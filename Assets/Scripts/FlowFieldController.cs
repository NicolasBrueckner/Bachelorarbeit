using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static GridUtility;

public class FlowFieldController : MonoBehaviour
{
	public FlowField flowField;
	public DebugGizmos debugGizmos;
	public Sector[,] sectors;
	public int2 worldGridSize;

	private int2 _mainIndex;
	private InputActions _actions;
	private InputAction _mouseRightAction;
	private InputAction _mousePositionAction;

	private void Awake()
	{
		_actions = new InputActions();
	}

	private void Start()
	{
		worldGridSize = new int2( sectors.GetLength( 0 ), sectors.GetLength( 1 ) );
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
		Vector2 mousePosition = _mousePositionAction.ReadValue<Vector2>();
		float3 position = Camera.main.ScreenToWorldPoint( new float3( mousePosition.x, mousePosition.y, 0f ) );
		int2 index = GetIndexFromPosition( position, transform.position, worldGridSize, Sector.sectorSize );

		if ( math.any( index != _mainIndex ) )
		{
			flowField = new FlowField( GetActiveSectors( index ) );

			flowField.InitializeFlowField();
			debugGizmos.SetFlowField( flowField );
			_mainIndex = index;
		}

		flowField.SetDestinationCell( position );
		flowField.CreateIntegrationField();
		flowField.CreateFlowField();
	}

	private Sector[,] GetActiveSectors( int2 mainIndex )
	{
		(Sector[,] activeSectors, int2 mainIndex_temp) = CreateActiveSectorArray( mainIndex );
		int2 activeSectorsDimensions = new int2( activeSectors.GetLength( 0 ), activeSectors.GetLength( 1 ) );

		foreach ( Direction direction in Direction.allDirections )
		{
			int2 currentIndex = mainIndex + direction;
			int2 currentIndex_temp = mainIndex_temp + direction;

			if ( ValidateIndex( currentIndex_temp, activeSectorsDimensions ) )
				activeSectors[ currentIndex_temp.x, currentIndex_temp.y ] = sectors[ currentIndex.x, currentIndex.y ];
		}

		return activeSectors;
	}

	private (Sector[,], int2 mainIndex) CreateActiveSectorArray( int2 mainIndex )
	{
		int2 currentDimensions = new int2( 3, 3 );
		int2 mainIndex_temp = new int2( 1, 1 );

		if ( !ValidateIndex( mainIndex + Direction.N, worldGridSize ) )
		{
			currentDimensions.y -= 1;
		}
		if ( !ValidateIndex( mainIndex + Direction.E, worldGridSize ) )
		{
			currentDimensions.x -= 1;
		}
		if ( !ValidateIndex( mainIndex + Direction.S, worldGridSize ) )
		{
			currentDimensions.y -= 1;
			mainIndex_temp.y -= 1;
		}
		if ( !ValidateIndex( mainIndex + Direction.W, worldGridSize ) )
		{
			currentDimensions.x -= 1;
			mainIndex_temp.x -= 1;
		}

		return (new Sector[ currentDimensions.x, currentDimensions.y ], mainIndex_temp);
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

		int2 i = new int2( 9, 2 );
		int2 S = new int2( 5, 3 );

		Debug.Log( $"i.x / S.x = {i.x / S.x}" );
		Debug.Log( $"i.y / S.y = {i.y / S.y}" );
		Debug.Log( $"i.x % S.x = {i.x % S.x}" );
		Debug.Log( $"i.y % S.y = {i.y % S.y}" );
	}
}
