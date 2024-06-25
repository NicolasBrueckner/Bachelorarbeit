using EditorAttributes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterController : MonoBehaviour
{
	[ReadOnly]
	public CharacterStats currenStats;

	public BaseCharacterStats baseStats;

	public Vector2 AimDirection { get; private set; } = new Vector2( 0f, 1f );

	private Rigidbody2D _rb2D;
	private Vector2 _moveDirection;
	private InputActions _actions;
	private InputAction _moveAction;
	private InputAction _aimAction;

	private void Awake()
	{
		currenStats = new( baseStats );
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
		_rb2D.velocity = currenStats.spd * Time.deltaTime * _moveDirection;
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
}
