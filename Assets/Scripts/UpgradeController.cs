using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
	public float a;
	public float b;
	public float c;
	public EnemyPoolController enemyPoolController;
	public PlayerCharacterController playerCharacterController;
	public WeaponController bubbleController;
	public WeaponController fistController;
	public WeaponController screamController;
	public WeaponController swipeController;

	public int CurrentLevel { get; private set; } = 0;
	public int CurrentExperience { get; private set; } = 0;

	private float _experienceForNextLevel;
	private System.Random _random = new();
	private Dictionary<Stats, bool> _activeStateByStats;

	private Stats _PlayerCharacterStats => playerCharacterController.currentStats;
	private Stats _BubbleStats => bubbleController.currentStats;
	private Stats _FistStats => fistController.currentStats;
	private Stats _ScreamStats => screamController.currentStats;
	private Stats _SwipeStats => swipeController.currentStats;

	private void Awake()
	{
		GetExperienceForNextLevel();
		InitializeDictionaries();

		EventManager.Instance.OnWeaponToggled += ChangeStatActiveState;
		EventManager.Instance.OnUpgradePicked += UpgradeStat;
	}

	private void Start()
	{
		StartCoroutine( UpgradeEnemiesCoroutine() );

	}

	private void InitializeDictionaries()
	{
		_activeStateByStats = new()
		{
			{_PlayerCharacterStats, true},
			{_BubbleStats, false },
			{_FistStats, false },
			{_ScreamStats, false },
			{_SwipeStats, false },
		};
	}

	public void AddExperience( int experience )
	{
		CurrentExperience += experience;
		if ( CurrentExperience >= _experienceForNextLevel )
		{
			CurrentLevel++;
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
		foreach ( EnemyPool pool in enemyPoolController.serializedEnemies )
		{
			Stats stats = pool.currentStats;
			foreach ( StatType type in stats.valuesByStatType.Keys.ToList() )
				stats[ type ] += stats.baseStats[ type ] * 0.1f;
		}
	}

	public void SendUpgradeOptions()
	{
		Stats pickedInstance = PickRandomInstanceToUpgrade();
		List<StatType> pickedStats = PickRandomStatsToUpgrade( pickedInstance, 3 );
		List<float> pickedValues = pickedStats.Select( type => PickRandomUpgradeValue( type ) ).ToList();

		EventManager.Instance.LevelUp( pickedInstance, pickedStats, pickedValues );
	}

	private Stats PickRandomInstanceToUpgrade()
	{
		List<Stats> activeInstances = _activeStateByStats.Where( kvp => kvp.Value ).Select( kvp => kvp.Key ).ToList();
		return activeInstances[ _random.Next( activeInstances.Count ) ];
	}

	private List<StatType> PickRandomStatsToUpgrade( Stats stats, int amount )
	{
		List<StatType> types = stats.valuesByStatType.Keys.ToList();
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
		if ( controller == null )
			Debug.Log( "controller is null" );
		_activeStateByStats[ controller.currentStats ] = isActive;
	}
}
