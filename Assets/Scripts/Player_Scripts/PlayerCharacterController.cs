using EditorAttributes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterController : MonoBehaviour
{
	[ReadOnly]
	public CharacterStats currentStats;

	public CharacterBaseStats baseStats;

	public Vector2 AimDirection { get; private set; } = new Vector2( 0f, 1f );

	private Vector2 _moveDirection;
	private Rigidbody2D _rb2D;
	private InputActions _actions;
	private InputAction _moveAction;
	private InputAction _aimAction;
	private ExperienceSytem _experienceSytem;

	private void Awake()
	{
		currentStats = new( baseStats );
		_rb2D = GetComponent<Rigidbody2D>();
		_actions = new();
		_experienceSytem = new( 0.3f, 0, 0 );
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
		_rb2D.velocity = currentStats.spd * Time.deltaTime * _moveDirection;
		Debug.Log( $"player hp:{currentStats.hp}" );
		_experienceSytem.AddExperience( 1 );
		Debug.Log( $"current level: {_experienceSytem.CurrentLevel}, current exp: {_experienceSytem.CurrentExperience}" );
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		Enemy enemy = collision.GetComponent<Enemy>();

		if ( enemy )
			TakeDamage( enemy.currentStats.atk );
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
		currentStats.hp -= math.max( damage - currentStats.def, damage * 0.15f );

		EventManager.Instance.HealthChanged( currentStats.hp / currentStats.max_hp );

		if ( currentStats.hp <= 0 )
			EventManager.Instance.PlayerDied();
	}
}
