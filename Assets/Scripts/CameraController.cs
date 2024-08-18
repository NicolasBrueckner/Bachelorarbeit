using UnityEngine;


public class CameraController : MonoBehaviour
{
	public Camera thisCamera;
	private Transform _targetTransform;

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
		_targetTransform = RuntimeManager.Instance.playerCharacterController.transform;
	}

	private void OnApplicationFocus( bool focus )
	{
		Cursor.lockState = focus ? CursorLockMode.Confined : CursorLockMode.None;
	}

	private void Update()
	{
		if ( _targetTransform != null )
			FollowTarget();
	}

	private void FollowTarget()
	{
		transform.position = _targetTransform.position + _offset;
	}

}
