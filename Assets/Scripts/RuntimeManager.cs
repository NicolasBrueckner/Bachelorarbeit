using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuntimeManager : MonoBehaviour
{
	public static RuntimeManager Instance { get; private set; }

	public GameObject runtimeObject;
	public CameraController mainCameraController;

	public PlayerCharacterController playerCharacterController;
	public Dictionary<WeaponType, WeaponController> weaponControllers;
	public EnemyPoolController enemyPoolController;
	public FlowFieldController flowFieldController;
	public WorldController worldController;
	public UpgradeController upgradeController;
#if UNITY_EDITOR
	public DebugController debugController;
#endif

	private GameObject _runtimeObjectCopy;

	private void Awake()
	{
		if ( Instance == null )
		{
			Instance = this;
			DontDestroyOnLoad( gameObject );
		}
		else
			Destroy( gameObject );
	}

	private void Start()
	{
		EventManager.Instance.OnStartGame += InstantiateRuntimeObject;
		EventManager.Instance.OnEndGame += DestroyRuntimeObject;
	}

	public void InstantiateRuntimeObject()
	{
		_runtimeObjectCopy = Instantiate( runtimeObject );
		InjectDependencies();
	}

	public void DestroyRuntimeObject()
	{
		Destroy( _runtimeObjectCopy );
		_runtimeObjectCopy = null;
	}

	private void InjectDependencies()
	{
		playerCharacterController = FindObjectOfType<PlayerCharacterController>();
		enemyPoolController = FindObjectOfType<EnemyPoolController>();
		flowFieldController = FindObjectOfType<FlowFieldController>();
		worldController = FindObjectOfType<WorldController>();
		upgradeController = FindObjectOfType<UpgradeController>();
#if UNITY_EDITOR
		debugController = FindObjectOfType<DebugController>();
#endif

		InjectWeaponControllers();

		EventManager.Instance.DependenciesInjected();
	}

	private void InjectWeaponControllers()
	{
		weaponControllers = new();

		foreach ( WeaponType weaponType in System.Enum.GetValues( typeof( WeaponType ) ) )
		{
			weaponControllers[ weaponType ] = FindObjectsOfType<WeaponController>().FirstOrDefault( controller => controller.type == weaponType );
		}
	}
}
