using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public enum FlowFieldDisplayType { None, All, Cost, Integration, Destination }

public class DebugGizmos : MonoBehaviour
{
	public GridController gridController;
	public bool showGrid;
	public FlowFieldDisplayType displayType;

	private FlowField _currentGrid;
	private int2 _gridOrigin;
	private int2 _gridSize;
	private float _cellHalfSize;
	private Sprite[] icons;

	private void Start()
	{
		icons = Resources.LoadAll<Sprite>( "Debug" );
	}

	public void SetFlowField( FlowField grid )
	{
		_currentGrid = grid;
		_gridOrigin = grid.gridOrigin;
		_gridSize = grid.gridSize;
		_cellHalfSize = grid.cellHalfSize;
	}


	public void DrawIcon( Cell cell )
	{
		GameObject iconGO = new GameObject();
		SpriteRenderer iconSR = iconGO.AddComponent<SpriteRenderer>();
		iconGO.transform.parent = transform;
		iconGO.transform.position = cell.position;

		if ( cell.cost == 0 )
			iconSR.sprite = icons[ 9 ];
		else if ( cell.cost == byte.MaxValue )
			iconSR.sprite = icons[ 10 ];
		else if ( cell.flowDirection == CellDirection.N )
			iconSR.sprite = icons[ 1 ];
		else if ( cell.flowDirection == CellDirection.E )
			iconSR.sprite = icons[ 0 ];
		else if ( cell.flowDirection == CellDirection.S )
			iconSR.sprite = icons[ 4 ];
		else if ( cell.flowDirection == CellDirection.W )
			iconSR.sprite = icons[ 7 ];
		else if ( cell.flowDirection == CellDirection.NE )
			iconSR.sprite = icons[ 2 ];
		else if ( cell.flowDirection == CellDirection.SE )
			iconSR.sprite = icons[ 5 ];
		else if ( cell.flowDirection == CellDirection.SW )
			iconSR.sprite = icons[ 6 ];
		else if ( cell.flowDirection == CellDirection.NW )
			iconSR.sprite = icons[ 3 ];
		else
			iconSR.sprite = icons[ 8 ];
	}

	private void OnDrawGizmos()
	{
		if ( showGrid )
		{
			if ( _currentGrid == null )
				DrawGizmoGrid( gridController.gridOrigin, gridController.testData.size, Color.red, gridController.cellHalfSize );
			else
				DrawGizmoGrid( _gridOrigin, _gridSize, Color.green, _cellHalfSize );
		}

		if ( _currentGrid == null )
			return;

		GUIStyle style = new GUIStyle( GUI.skin.label );
		style.alignment = TextAnchor.MiddleCenter;

		switch ( displayType )
		{
			case FlowFieldDisplayType.Cost:
				foreach ( Cell cell in _currentGrid.grid )
					Handles.Label( cell.position, cell.cost.ToString(), style );
				break;
			case FlowFieldDisplayType.Integration:
				foreach ( Cell cell in _currentGrid.grid )
					Handles.Label( cell.position, cell.integrationCost.ToString(), style );
				break;
			case FlowFieldDisplayType.Destination:
				{
					foreach ( Transform t in transform )
						GameObject.Destroy( t.gameObject );
					foreach ( Cell cell in _currentGrid.grid )
						DrawIcon( cell );
				}
				break;

			default:
				break;
		}
	}

	private void DrawGizmoGrid( int2 gridOrigin, int2 gridSize, Color32 gridColor, float cellHalfSize )
	{
		Gizmos.color = gridColor;
		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
			{
				Vector3 center = new Vector3( gridOrigin.x + ( cellHalfSize * 2 * x ) + cellHalfSize, gridOrigin.y + ( cellHalfSize * 2 * y ) + cellHalfSize, 0 );
				Vector3 size = Vector3.one * cellHalfSize * 2;
				Gizmos.DrawWireCube( center, size );
			}
		}
	}
}
