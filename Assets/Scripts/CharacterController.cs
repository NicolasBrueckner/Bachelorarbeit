using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
	public CharacterStats stats;

	private Rigidbody2D _rb2D;
	private Vector2 _moveDirection;
	private InputActions _actions;
	private InputAction _moveAction;

	private void Awake()
	{
		stats = new CharacterStats();
		_rb2D = GetComponent<Rigidbody2D>();
		_actions = new InputActions();
	}

	private void OnEnable()
	{
		_moveAction = _actions.Player.Move;
		_moveAction.Enable();

		_moveAction.performed += OnMoveAction;
		_moveAction.canceled += OnMoveAction;
	}

	private void OnDisable()
	{
		_moveAction.performed -= OnMoveAction;
		_moveAction.canceled -= OnMoveAction;

		_moveAction.Disable();
	}

	private void FixedUpdate()
	{
		_rb2D.velocity = _moveDirection * stats.spd * Time.deltaTime;
	}

	private void OnMoveAction( InputAction.CallbackContext context )
	{
		_moveDirection = context.ReadValue<Vector2>();
	}
}
