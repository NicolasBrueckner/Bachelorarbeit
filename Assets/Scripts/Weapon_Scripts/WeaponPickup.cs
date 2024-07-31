using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
	public WeaponType type;
	public WeaponController weaponController { get; private set; }

	private int _playerLayer;

	private void Awake()
	{
		_playerLayer = LayerMask.NameToLayer( "Player" );

		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnDependenciesInjected -= OnDependenciesInjected;
	}

	private void OnDependenciesInjected()
	{
		GetDependencies();
	}

	private void GetDependencies()
	{
		weaponController = RuntimeManager.Instance.weaponControllers[ type ];
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.layer == _playerLayer )
		{
			weaponController.ToggleWeapon( true );
			Destroy( gameObject );
		}
	}
}
