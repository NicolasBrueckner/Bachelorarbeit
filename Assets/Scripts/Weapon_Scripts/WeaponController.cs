using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
	[ReadOnly]
	public Stats currentStats;

	public BaseStats baseStats;
	public GameObject weaponObject;
	public int weaponQueueSize;
	public bool startsActive;

	public float Frequency => currentStats[ StatType.atk_spd ] * _characterController.currentStats[ StatType.atk_spd ];
	public Vector2 Direction => _characterController.AimDirection;

	private bool _isActive;
	private Queue<GameObject> _weaponObjectQueue;
	private PlayerCharacterController _characterController;

	private void Awake()
	{
		currentStats = new( baseStats );
		_weaponObjectQueue = new Queue<GameObject>();
		_characterController = GetComponentInParent<PlayerCharacterController>();

		InitializeWeapon();
	}

	private void Start()
	{
		ToggleWeapon( startsActive );
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
		Debug.Log( $"weapon queue count: {_weaponObjectQueue.Count}" );
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
