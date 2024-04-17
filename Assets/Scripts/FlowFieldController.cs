using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlowFieldController : MonoBehaviour
{
	public FlowField activeFlowField;
	public DebugGizmos debugGizmos;
	public Sector[,] sectors;

	private InputActions _actions;
	private InputAction _mouseRightAction;
	private InputAction _mousePositionAction;

	private void Awake()
	{
		_actions = new InputActions();
	}

	private void OnEnable()
	{
		_mouseRightAction = _actions.Debug.MouseRight;
		_mousePositionAction = _actions.Debug.MousePosition;
		_mouseRightAction.Enable();
		_mousePositionAction.Enable();

		_mouseRightAction.performed += OnMouseRight;
	}

	private void OnDisable()
	{
		_mouseRightAction.performed -= OnMouseRight;

		_actions.Debug.MouseRight.Disable();
		_actions.Debug.MousePosition.Disable();
	}

	private void OnMouseRight( InputAction.CallbackContext context )
	{
		activeFlowField = new FlowField( sectors );

		activeFlowField.CreateGrid();
		debugGizmos.SetFlowField( activeFlowField );
		activeFlowField.CreateCostField( sectors[ 0, 0 ].costs );

		Vector2 mousePosition = _mousePositionAction.ReadValue<Vector2>();
		float3 position = Camera.main.ScreenToWorldPoint( new float3( mousePosition.x, mousePosition.y, 0f ) );

		//activeFlowField.CreateIntegrationField( activeFlowField.GetCellFromPosition( position ) );
		activeFlowField.CreateFlowField();
	}

	private void GetActiveSectors()
	{


	}
}
