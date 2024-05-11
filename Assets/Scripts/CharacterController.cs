using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
	public BaseCharacterStats baseStats;
	public CharacterStats stats;
	public float2 AimDirection { get; private set; }

	private Rigidbody2D _rb2D;
	private float2 _moveDirection;
	private InputActions _actions;
	private InputAction _moveAction;
	private InputAction _aimAction;

	private void Awake()
	{
		stats = new CharacterStats( baseStats );
		_rb2D = GetComponent<Rigidbody2D>();
		_actions = new InputActions();
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
		Debug.Log( $"spd: {stats.spd}" );
		_rb2D.velocity = _moveDirection * stats.spd * Time.deltaTime;
	}

	private void OnMoveAction( InputAction.CallbackContext context )
	{
		_moveDirection = ( float2 )context.ReadValue<Vector2>();
	}

	private void OnAimAction( InputAction.CallbackContext context )
	{
		float2 center = new float2( Screen.width / 2, Screen.height / 2 );
		AimDirection = math.normalize( ( float2 )context.ReadValue<Vector2>() - center );
		Debug.Log( $"aim direction: {AimDirection}" );
	}
}
