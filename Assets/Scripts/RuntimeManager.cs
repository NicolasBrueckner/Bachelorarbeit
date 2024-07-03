using UnityEngine;

public class RuntimeManager : MonoBehaviour
{
	public static RuntimeManager Instance { get; private set; }

	public GameObject runtimeObject;
	public CameraController mainCameraController;

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
		EventManager.Instance.OnResetGame += ResetRuntimeObject;
		EventManager.Instance.OnStartGame += InstantiateRuntimeObject;
		EventManager.Instance.OnEndGame += DestroyRuntimeObject;
	}

	public void ResetRuntimeObject()
	{
		DestroyRuntimeObject();
		InstantiateRuntimeObject();
	}

	public void InstantiateRuntimeObject()
	{
		_runtimeObjectCopy = Instantiate( runtimeObject );
		mainCameraController.target = _runtimeObjectCopy.GetComponentInChildren<PlayerCharacterController>().gameObject;
	}

	public void DestroyRuntimeObject()
	{
		Destroy( _runtimeObjectCopy );
	}
}
