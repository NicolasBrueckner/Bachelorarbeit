using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public enum FlowFieldDisplayType { None, All, Cost, Integration, Destination }

public class DebugGizmos : MonoBehaviour
{
	public bool showGrid;
	public FlowFieldDisplayType displayType;

	private FlowField _currentGrid;
	private float3 _gridOrigin;
	private int2 _gridSize;
	private Sprite[] _icons;

	private void Start()
	{
		_icons = Resources.LoadAll<Sprite>( "Debug" );
	}

	public void SetFlowField( FlowField grid )
	{
		_currentGrid = grid;
		_gridOrigin = grid.GridOrigin;
		_gridSize = grid.GridSize;
	}


	public void DrawIcon( Cell cell )
	{
		GameObject iconGO = new GameObject();
		SpriteRenderer iconSR = iconGO.AddComponent<SpriteRenderer>();
		iconGO.transform.parent = transform;
		iconGO.transform.position = cell.position;

		if ( cell.cost == 0 )
			iconSR.sprite = _icons[ 9 ];
		else if ( cell.cost == byte.MaxValue )
			iconSR.sprite = _icons[ 10 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.N )
			iconSR.sprite = _icons[ 1 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.NE )
			iconSR.sprite = _icons[ 0 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.E )
			iconSR.sprite = _icons[ 4 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.SE )
			iconSR.sprite = _icons[ 7 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.S )
			iconSR.sprite = _icons[ 2 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.SW )
			iconSR.sprite = _icons[ 5 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.W )
			iconSR.sprite = _icons[ 6 ];
		else if ( Direction.GetDirection( ( int2 )cell.flowDirection ) == Direction.NW )
			iconSR.sprite = _icons[ 3 ];
		else
			iconSR.sprite = _icons[ 9 ];
	}

	private void OnDrawGizmos()
	{
		if ( showGrid )
		{
			if ( _currentGrid == null )
				return;
			else
				DrawGizmoGrid( _gridOrigin, _gridSize, Color.green, Sector.cellRadius );
		}

		if ( _currentGrid == null )
			return;

		GUIStyle style = new GUIStyle( GUI.skin.label );
		style.alignment = TextAnchor.MiddleCenter;

		switch ( displayType )
		{
			case FlowFieldDisplayType.Cost:
				foreach ( Transform t in transform )
					Destroy( t.gameObject );
				foreach ( Cell cell in _currentGrid.Cells )
					Handles.Label( cell.position, cell.cost.ToString(), style );
				break;
			case FlowFieldDisplayType.Integration:
				foreach ( Transform t in transform )
					Destroy( t.gameObject );
				foreach ( Cell cell in _currentGrid.Cells )
					Handles.Label( cell.position, cell.integrationCost.ToString(), style );
				break;
			case FlowFieldDisplayType.Destination:
				foreach ( Transform t in transform )
					Destroy( t.gameObject );
				foreach ( Cell cell in _currentGrid.Cells )
					DrawIcon( cell );
				break;
			default:
				break;
		}
	}

	private void DrawGizmoGrid( float3 gridOrigin, int2 gridSize, Color32 gridColor, float cellHalfSize )
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

	private bool Float2Compare( float2 a, float2 b )
	{
		return math.all( math.abs( a - b ) < 0.0001f );
	}

	private bool Float3Compare( float3 a, float3 b )
	{
		return math.all( math.abs( a - b ) < 0.0001f );
	}
}
