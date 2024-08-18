using EditorAttributes;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterController : MonoBehaviour
{
	[ReadOnly]
	public Stats currentStats;

	public SpriteRenderer playerSprite;
	public BaseStats baseStats;
	public Rigidbody2D rb2D;
	public Collider2D physicsCollider;

	public Vector2 AimDirection { get; private set; } = new Vector2( 0f, 1f );

	private bool _isDashing;
	private float _speedMultiplier;
	private Vector2 _moveDirection;
	private Coroutine _dashCoroutine;
	private InputActions _actions;
	private InputAction _moveAction;
	private InputAction _aimAction;
	private InputAction _dashAction;
	private UpgradeController _upgradeController;

	private void Awake()
	{
		currentStats = new( baseStats );
		_actions = new();

		EventManager.Instance.OnDependenciesInjected += OnDependenciesInjected;
		EventManager.Instance.OnUpgradePicked += OnUpgradePicked;
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnUpgradePicked -= OnUpgradePicked;
		EventManager.Instance.OnDependenciesInjected -= OnDependenciesInjected;
	}

	private void OnDependenciesInjected()
	{
		GetDependencies();

		_upgradeController.AddPlayerStats( currentStats, true );
	}

	private void GetDependencies()
	{
		_upgradeController = RuntimeManager.Instance.upgradeController;
	}

	private void OnEnable()
	{
		_moveAction = _actions.Player.Move;
		_aimAction = _actions.Player.Aim;
		_dashAction = _actions.Player.Dash;
		_moveAction.Enable();
		_aimAction.Enable();
		_dashAction.Enable();

		_moveAction.performed += OnMoveAction;
		_moveAction.canceled += OnMoveAction;
		_aimAction.performed += OnAimAction;
		_dashAction.performed += OnDashAction;
	}

	private void OnDisable()
	{
		_moveAction.performed -= OnMoveAction;
		_moveAction.canceled -= OnMoveAction;
		_aimAction.performed -= OnAimAction;
		_dashAction.performed -= OnDashAction;

		_moveAction.Disable();
		_aimAction.Disable();
		_dashAction.Disable();
	}

	private void FixedUpdate()
	{
		_speedMultiplier = _isDashing ? 5 : 1;
		rb2D.velocity = currentStats[ StatType.spd ] * _speedMultiplier * Time.deltaTime * _moveDirection;
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		Enemy enemy = collision.GetComponent<Enemy>();

		if ( enemy && !DebugBuildStaticValues.isInvincible )
			TakeDamage( enemy.currentStats[ StatType.atk ] );
	}

	private void OnMoveAction( InputAction.CallbackContext context )
	{
		_moveDirection = context.ReadValue<Vector2>();
	}

	private void OnAimAction( InputAction.CallbackContext context )
	{
		Vector2 center = new( Screen.width / 2, Screen.height / 2 );
		AimDirection = math.normalize( context.ReadValue<Vector2>() - center );
	}

	private void OnDashAction( InputAction.CallbackContext context )
	{
		_dashCoroutine ??= StartCoroutine( DashCoroutine() );
	}

	private void OnUpgradePicked( Stats stats, StatType type, float value )
	{
		if ( stats == currentStats && type == StatType.size )
			RescaleObject();
	}

	private IEnumerator DashCoroutine()
	{
		float dashTimer = 0.3f;

		_isDashing = true;
		physicsCollider.enabled = false;

		SetSpriteAlpha( 0.5f );

		while ( dashTimer > 0f )
		{
			dashTimer -= Time.deltaTime;

			yield return null;
		}

		SetSpriteAlpha( 1 );

		physicsCollider.enabled = true;
		_isDashing = false;
		_dashCoroutine = null;
	}

	private void RescaleObject()
	{
		transform.localScale = Vector3.one * currentStats[ StatType.size ];
	}

	private void TakeDamage( float damage )
	{
		Debug.Log( $"taking: {damage} damage" );
		currentStats[ StatType.hp ] -= math.max( damage - currentStats[ StatType.def ], damage * 0.15f );

		EventManager.Instance.HealthChanged( currentStats[ StatType.hp ] / currentStats[ StatType.max_hp ] * 100 );

		if ( currentStats[ StatType.hp ] <= 0 )
			EventManager.Instance.PlayerDied();
	}

	private void SetSpriteAlpha( float alpha )
	{
		Color color = playerSprite.color;
		color.a = alpha;
		playerSprite.color = color;
	}
}
