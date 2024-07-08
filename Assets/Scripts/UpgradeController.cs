using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
	public float a;
	public float b;
	public float c;

	public int CurrentLevel { get; private set; }
	public int CurrentExperience { get; private set; }
	public PlayerCharacterController PlayerCharacterController { get; private set; }

	private float _experienceForNextLevel;
	private System.Random _random;
	private Dictionary<Stats, bool> _activeStateByPlayerStats;
	private Dictionary<Stats, bool> _activeStateByEnemyStats;

	private void Awake()
	{
		_activeStateByPlayerStats = new();
		_activeStateByEnemyStats = new();
		_random = new();

		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;
		EventManager.Instance.OnWeaponToggled += ChangeStatActiveState;
		EventManager.Instance.OnUpgradePicked += UpgradeStat;
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnDependenciesInjected -= OnDependenciesInjected;
		EventManager.Instance.OnWeaponToggled -= ChangeStatActiveState;
		EventManager.Instance.OnUpgradePicked -= UpgradeStat;
	}

	private void OnDependenciesInjected()
	{
		GetDependencies();

		GetExperienceForNextLevel();
		StartCoroutine( UpgradeEnemiesCoroutine() );
	}

	private void GetDependencies()
	{
		PlayerCharacterController = RuntimeManager.Instance.playerCharacterController;
		CurrentLevel = ( int )PlayerCharacterController.currentStats[ StatType.level ];
		CurrentExperience = ( int )PlayerCharacterController.currentStats[ StatType.experience ];
	}

	public void AddPlayerStats( Stats stats, bool isActive )
	{
		_activeStateByPlayerStats[ stats ] = isActive;
	}

	public void AddEnemyStats( Stats stats, bool isActive )
	{
		_activeStateByEnemyStats[ stats ] = isActive;
	}

	public void AddExperience( int experience )
	{
		CurrentExperience += experience;
		if ( CurrentExperience >= _experienceForNextLevel )
		{
			CurrentLevel++;
			Debug.Log( $"Level up! level: {CurrentLevel}" );
			SendUpgradeOptions();
			_experienceForNextLevel = GetExperienceForNextLevel();
		}
	}

	public IEnumerator UpgradeEnemiesCoroutine()
	{
		while ( gameObject.activeInHierarchy )
		{
			yield return new WaitForSeconds( 10 );
			UpgradeEnemies();
		}
	}

	public void UpgradeEnemies()
	{
		foreach ( Stats stats in _activeStateByEnemyStats.Keys )
		{
			if ( _activeStateByEnemyStats[ stats ] )
			{
				foreach ( StatType type in stats.valuesByStatType.Keys.ToList() )
					stats[ type ] += stats.baseStats[ type ] * 0.1f;
			}
		}
	}

	public void SendUpgradeOptions()
	{
		Stats pickedInstance = PickRandomInstanceToUpgrade();
		List<StatType> pickedStats = PickRandomStatsToUpgrade( pickedInstance, 3 );
		List<float> pickedValues = pickedStats.Select( type => PickRandomUpgradeValue( type ) ).ToList();

		foreach ( StatType type in pickedStats )
			Debug.Log( $"picked stat: {type}" );
		foreach ( float value in pickedValues )
			Debug.Log( $"picked value: {value}" );

		EventManager.Instance.LevelUp( pickedInstance, pickedStats, pickedValues );
	}

	private Stats PickRandomInstanceToUpgrade()
	{
		List<Stats> activeInstances = _activeStateByPlayerStats.Where( kvp => kvp.Value ).Select( kvp => kvp.Key ).ToList();
		return activeInstances[ _random.Next( activeInstances.Count ) ];
	}

	private List<StatType> PickRandomStatsToUpgrade( Stats stats, int amount )
	{
		List<StatType> types = stats.valuesByStatType.Keys
			.Where( type => type != StatType.none && type != StatType.level && type != StatType.experience && type != StatType.hp )
			.ToList();

		return types.OrderBy( x => _random.Next() ).Take( amount ).Distinct().ToList();
	}

	private float PickRandomUpgradeValue( StatType type )
	{
		return type switch
		{
			StatType.max_hp => 0.2f * _random.Next( 1, 3 ),
			StatType.atk => 0.1f * _random.Next( 1, 3 ),
			StatType.atk_spd => -0.05f * _random.Next( 1, 3 ),
			StatType.spd => 0.05f * _random.Next( 1, 3 ),
			StatType.def => 0.1f * _random.Next( 1, 3 ),
			StatType.size => 0.1f * _random.Next( 1, 3 ),
			StatType.duration => 0.1f * _random.Next( 1, 3 ),
			StatType.pierce => _random.Next( 1, 2 ),
			_ => 0f,
		};
	}

	private void UpgradeStat( Stats stats, StatType type, float value )
	{
		stats[ type ] += stats.baseStats[ type ] * value;
	}

	private float GetExperienceForNextLevel()
	{
		return ( a * CurrentLevel * CurrentLevel ) + ( b * CurrentLevel ) + c;
	}

	private void ChangeStatActiveState( WeaponController controller, bool isActive )
	{
		_activeStateByPlayerStats[ controller.currentStats ] = isActive;
	}
}
