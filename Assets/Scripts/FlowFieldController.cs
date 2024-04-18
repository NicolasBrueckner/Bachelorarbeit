using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlowFieldController : MonoBehaviour
{
	public FlowField flowField;
	//public DebugGizmos debugGizmos;
	public Sector[,] sectors;
	public int2 worldSize;

	private Sector _mainSector;
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
		TestMethod();
		//Vector2 mousePosition = _mousePositionAction.ReadValue<Vector2>();
		//float3 position = Camera.main.ScreenToWorldPoint( new float3( mousePosition.x, mousePosition.y, 0f ) );
		//flowField = new FlowField( sectors );

		//flowField.CreateGrid();
		//debugGizmos.SetFlowField( flowField );
		//flowField.CreateCostField( sectors[ 0, 0 ].costs );



		//List<Sector> activeSectors = GetActiveSectors( position );

		//flowField.CreateIntegrationField( flowField.GetCellFromPosition( position ) );
		//flowField.CreateFlowField();
	}

	private Sector[,] GetActiveSectors( int2 mainIndex )
	{
		Sector[,] activeSectors = new Sector[ 3, 3 ];
		int2 mainIndex_temp = new int2( 1, 1 );

		foreach ( Direction direction in Direction.allDirections )
		{
			int2 currentIndex = mainIndex + ( int2 )direction.direction;
			int2 currentIndex_temp = mainIndex_temp + ( int2 )direction.direction;

			if ( GridUtility.ValidateIndex( currentIndex, worldSize ) )
				activeSectors[ currentIndex_temp.x, currentIndex_temp.y ] = sectors[ currentIndex.x, currentIndex.y ];
		}

		return activeSectors;
	}

	private void TestMethod()
	{
		int2 sectorDimensions = new int2( sectors.GetLength( 0 ), sectors.GetLength( 1 ) );
		int sectorLength = sectors.Length;

		int2 gridSize = new int2();

		gridSize = Sector.gridSize * sectorDimensions;
		Debug.Log( $"*int2: {gridSize}" );
		gridSize = Sector.gridSize * sectorLength;
		Debug.Log( $"*int: {gridSize}" );
	}
}
