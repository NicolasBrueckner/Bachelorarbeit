using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
	public WeaponType type;
	public WeaponController weaponController { get; private set; }

	private void Awake()
	{
		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;
	}

	private void OnDependenciesInjected()
	{
		GetDependencies();
	}

	private void GetDependencies()
	{
		weaponController = RuntimeManager.Instance.weaponControllers[ type ];
	}
}
