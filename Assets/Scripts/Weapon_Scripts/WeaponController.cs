using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
	[ReadOnly]
	public WeaponStats currentStats;

	public GameObject weaponObject;
	public WeaponBaseStats baseStats;
	public int weaponQueueSize;
	public bool startsActive;

	public float Frequency => currentStats.atk_spd * _characterController.currenStats.atk_spd;
	public Vector2 Direction => _characterController.AimDirection;

	private bool _isActive;
	private float _currentCooldown;
	private Coroutine _weaponAttackCoroutine;
	private Queue<GameObject> _weaponObjectQueue;
	private PlayerCharacterController _characterController;

	private void Awake()
	{
		currentStats = new( baseStats );
		_currentCooldown = 0f;
		_weaponObjectQueue = new Queue<GameObject>();
		_characterController = GetComponentInParent<PlayerCharacterController>();

		InitializeWeapon();
		ToggleWeapon( startsActive );
	}

	private IEnumerator WeaponAttackCoroutine()
	{
		while ( _isActive )
		{
			_currentCooldown += Time.deltaTime;
			if ( _currentCooldown >= Frequency )
				Attack();

			yield return null;
		}
	}

	private void Attack()
	{
		if ( _weaponObjectQueue.Count > 0 )
		{
			_currentCooldown = 0f;
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

			InitializeWeaponObject( weaponObjectCopy, weapon );
			_weaponObjectQueue.Enqueue( weaponObjectCopy );
		}
	}

	private void InitializeWeaponObject( GameObject weaponObjectCopy, Weapon weapon )
	{
		weapon.controller = this;
		weapon.currentStats = currentStats;
		weapon.SetDefaults();
		weaponObjectCopy.SetActive( _isActive );
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

	}
}
