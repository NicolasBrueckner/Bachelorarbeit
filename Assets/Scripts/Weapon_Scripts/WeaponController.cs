using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	Bubble,
	Fist,
	Scream,
	Swipe,
}

public class WeaponController : MonoBehaviour
{
	[ReadOnly]
	public Stats currentStats;

	public WeaponType type;
	public BaseStats baseStats;
	public GameObject weaponObject;
	public int weaponQueueSize;
	public bool startsActive;

	public PlayerCharacterController playerCharacterController { get; private set; }
	public UpgradeController upgradeController { get; private set; }
	public float Frequency => currentStats[ StatType.atk_spd ] * playerCharacterController.currentStats[ StatType.atk_spd ];
	public Vector2 Direction => playerCharacterController.AimDirection;

	private bool _isActive;
	private Queue<GameObject> _weaponObjectQueue;

	private void Awake()
	{
		currentStats = new( baseStats );
		_weaponObjectQueue = new Queue<GameObject>();

		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnDependenciesInjected -= OnDependenciesInjected;
	}

	private void OnDependenciesInjected()
	{
		GetDependencies();

		InitializeWeapon();
		ToggleWeapon( startsActive );
	}

	private void GetDependencies()
	{
		playerCharacterController = RuntimeManager.Instance.playerCharacterController;
		upgradeController = RuntimeManager.Instance.upgradeController;
	}

	private IEnumerator WeaponAttackCoroutine()
	{
		while ( _isActive )
		{
			Attack();

			yield return new WaitForSeconds( Frequency );
		}
	}

	private void Attack()
	{
		if ( _weaponObjectQueue.Count > 0 )
		{
			GameObject dequeuedWeaponObject = _weaponObjectQueue.Dequeue();

			dequeuedWeaponObject.SetActive( _isActive );
			dequeuedWeaponObject.GetComponent<Weapon>().StartAttack();
		}
	}

	public void EnqueueWeaponObject( GameObject weaponObject )
	{
		weaponObject.SetActive( false );
		weaponObject.transform.position = transform.position;
		_weaponObjectQueue.Enqueue( weaponObject );
	}

	private void InitializeWeapon()
	{
		for ( int i = 0; i < weaponQueueSize; i++ )
		{
			GameObject weaponObjectCopy = Instantiate( weaponObject, transform );
			Weapon weapon = weaponObjectCopy.GetComponent<Weapon>();

			InitializeWeaponInstance( weapon );
			EnqueueWeaponObject( weaponObjectCopy );
		}

		upgradeController.AddPlayerStats( currentStats, startsActive );
	}

	private void InitializeWeaponInstance( Weapon weapon )
	{
		weapon.weaponController = this;
		weapon.currentStats = currentStats;
		weapon.SetDefaults();
	}

	[ContextMenu( "Activate Weapon" )]
	public void DebugToggleWeapon()
	{
		ToggleWeapon( null );
	}

	public void ToggleWeapon( bool? isActive )
	{
		_isActive = isActive ?? !_isActive;

		if ( _isActive )
		{
			StartCoroutine( WeaponAttackCoroutine() );
		}
		else
		{
			StopCoroutine( WeaponAttackCoroutine() );
			foreach ( GameObject weaponObject in _weaponObjectQueue )
				weaponObject.SetActive( _isActive );
		}

		EventManager.Instance.WeaponToggled( this, _isActive );
	}
}
