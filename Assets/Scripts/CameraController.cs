using UnityEngine;


public class CameraController : MonoBehaviour
{
	public GameObject target;
	public Vector3 offset = new( 0, 0, -10 );

	private void Awake()
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
		transform.position = target.transform.position + offset;
	}

}
