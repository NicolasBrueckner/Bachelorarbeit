using UnityEngine;


public class CameraController : MonoBehaviour
{
	public Camera thisCamera;
	public GameObject target;

	private Vector3 _offset = new( 0, 0, -10 );

	private void Start()
	{
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
		target = RuntimeManager.Instance.playerCharacterController.gameObject;
	}

	private void OnApplicationFocus( bool focus )
	{
		Cursor.lockState = focus ? CursorLockMode.Confined : CursorLockMode.None;
	}

	private void Update()
	{
		if ( target != null )
			FollowTarget();
	}

	private void FollowTarget()
	{
		transform.position = target.transform.position + _offset;
	}

}
