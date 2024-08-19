using UnityEngine;


public class CameraController : MonoBehaviour
{
	public float targetAspectRatio = 16f / 9f;
	public Camera thisCamera;
	public Transform targetTransform;

	private int _lastScreenWidth = 1920;
	private int _lastScreenHeight = 1080;
	private Vector3 _offset = new( 0, 0, -10 );

	private void Start()
	{
		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;

		thisCamera = GetComponent<Camera>();
		Screen.SetResolution( _lastScreenWidth, _lastScreenHeight, false );
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
		targetTransform = RuntimeManager.Instance.playerCharacterController.gameObject.transform;
	}

	private void OnApplicationFocus( bool focus )
	{
		Cursor.lockState = focus ? CursorLockMode.Confined : CursorLockMode.None;
	}

	private void Update()
	{
		if ( targetTransform != null )
			FollowTarget();

		if ( Screen.width != _lastScreenWidth || Screen.height != _lastScreenHeight )
		{
			_lastScreenWidth = Screen.width;
			_lastScreenHeight = Screen.height;

			EnsureAspectRatio();
		}
	}

	private void FollowTarget()
	{
		transform.position = targetTransform.position + _offset;
	}

	private void EnsureAspectRatio()
	{
		float windowAspectRatio = Screen.width / ( float )Screen.height;
		float scaleHeight = windowAspectRatio / targetAspectRatio;

		if ( scaleHeight < 1.0f )
		{
			Rect rect = thisCamera.rect;
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = ( 1.0f - scaleHeight ) / 2.0f;
			thisCamera.rect = rect;
		}
		else
		{
			float scaleWidth = 1.0f / scaleHeight;
			Rect rect = thisCamera.rect;
			rect.width = scaleWidth;
			rect.height = 1.0f;
			rect.x = ( 1.0f - scaleWidth ) / 2.0f;
			rect.y = 0;
			thisCamera.rect = rect;
		}
	}

}
