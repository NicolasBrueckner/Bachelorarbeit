using EditorAttributes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterController : MonoBehaviour
{
	[ReadOnly]
	public Stats currentStats;

	public BaseStats baseStats;

	public Vector2 AimDirection { get; private set; } = new Vector2( 0f, 1f );

	private int _enemyLayer;
	private int _interactableLayer;
	private Vector2 _moveDirection;
	private Rigidbody2D _rb2D;
	private InputActions _actions;
	private InputAction _moveAction;
	private InputAction _aimAction;

	private void Awake()
	{
		currentStats = new( baseStats );
		_enemyLayer = LayerMask.NameToLayer( "Enemy" );
		_interactableLayer = LayerMask.NameToLayer( "Interactable" );
		_rb2D = GetComponent<Rigidbody2D>();
		_actions = new();
	}

	private void OnEnable()
	{
		_moveAction = _actions.Player.Move;
		_aimAction = _actions.Player.Aim;
		_moveAction.Enable();
		_aimAction.Enable();

		_moveAction.performed += OnMoveAction;
		_moveAction.canceled += OnMoveAction;
		_aimAction.performed += OnAimAction;
	}

	private void OnDisable()
	{
		_moveAction.performed -= OnMoveAction;
		_moveAction.canceled -= OnMoveAction;
		_aimAction.performed -= OnAimAction;

		_moveAction.Disable();
		_aimAction.Disable();
	}

	private void FixedUpdate()
	{
		_rb2D.velocity = currentStats[ StatType.spd ] * Time.deltaTime * _moveDirection;
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.gameObject.layer == _enemyLayer )
			TakeDamage( collision.GetComponent<Enemy>().currentStats[ StatType.atk ] );
		else if ( collision.gameObject.layer == _interactableLayer )
			collision.GetComponent<WeaponPickup>().controller.ToggleWeapon( true );

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

	private void TakeDamage( float damage )
	{
		currentStats[ StatType.hp ] -= math.max( damage - currentStats[ StatType.def ], damage * 0.15f );
		Debug.Log( $"current hp: {currentStats[ StatType.hp ]}" );

		EventManager.Instance.HealthChanged( currentStats[ StatType.hp ] / currentStats[ StatType.max_hp ] );

		if ( currentStats[ StatType.hp ] <= 0 )
			EventManager.Instance.PlayerDied();
	}
}
