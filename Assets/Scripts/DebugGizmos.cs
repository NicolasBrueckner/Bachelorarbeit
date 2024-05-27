using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum FlowFieldDisplayType { None, All, Cost, Integration, Destination }

public class DebugGizmos : MonoBehaviour
{
	public bool showGrid;
	public FlowFieldDisplayType displayType;

	private Dictionary<int2, string> _directionToIcon;
	private FlowField _currentGrid;
	private float3 _gridOrigin;
	private int2 _gridSize;
	private Sprite[] _icons;

	private void Start()
	{
		InitializeDirectionToIcon();
	}

	public void SetFlowField( FlowField grid )
	{
		_currentGrid = grid;
		_gridOrigin = grid.GridOrigin;
		_gridSize = grid.GridSize;
	}


	public void DrawIcon( Cell cell )
	{
		Vector3 position = cell.position;
		int2 flowDirection = ( int2 )cell.flowDirection;

		string label = "";

		if ( cell.cost == 0 )
			label = "0";
		else if ( cell.cost == byte.MaxValue )
			label = "\u221E";
		else
			label = _directionToIcon.TryGetValue( flowDirection, out label ) ? label : "NA";

		Handles.Label( position, label );
	}

	private void OnDrawGizmos()
	{
		if ( _currentGrid == null )
			return;

		if ( showGrid || displayType == FlowFieldDisplayType.Destination )
		{
			foreach ( Transform t in transform )
				DestroyImmediate( t.gameObject );
		}

		if ( showGrid )
			DrawGizmoGrid( _gridOrigin, _gridSize, Color.green, Sector.cellRadius );

		GUIStyle style = new GUIStyle( GUI.skin.label )
		{
			alignment = TextAnchor.MiddleCenter,
			fontSize = 10
		};

		switch ( displayType )
		{
			case FlowFieldDisplayType.Cost:
				foreach ( Cell cell in _currentGrid.Cells )
					Handles.Label( cell.position, cell.cost.ToString(), style );
				break;
			case FlowFieldDisplayType.Integration:
				foreach ( Cell cell in _currentGrid.Cells )
					Handles.Label( cell.position, cell.integrationCost.ToString(), style );
				break;
			case FlowFieldDisplayType.Destination:
				foreach ( Cell cell in _currentGrid.Cells )
					DrawIcon( cell );
				break;
			default:
				break;
		}
	}

	private void InitializeDirectionToIcon()
	{
		string[] icons = { "\u2191", "\u2197", "\u2192", "\u2198", "\u2193", "\u2199", "\u2190", "\u2196" };

		_directionToIcon = new Dictionary<int2, string>
		{
			{Direction.N.direction, icons[0] },
			{Direction.NE.direction, icons[1] },
			{Direction.E.direction, icons[2] },
			{Direction.SE.direction, icons[3] },
			{Direction.S.direction, icons[4] },
			{Direction.SW.direction, icons[5] },
			{Direction.W.direction, icons[6] },
			{Direction.NW.direction, icons[7] }
		};
	}

	private void DrawGizmoGrid( float3 gridOrigin, int2 gridSize, Color32 gridColor, float cellHalfSize )
	{
		Gizmos.color = gridColor;
		float cellSize = cellHalfSize * 2;
		Vector3 size = Vector3.one * cellSize;

		float startX = gridOrigin.x + cellHalfSize;
		float startY = gridOrigin.y + cellHalfSize;

		for ( int x = 0; x < gridSize.x; x++ )
		{
			float posX = startX + ( cellSize * x );
			for ( int y = 0; y < gridSize.y; y++ )
			{
				float posY = startY + ( cellSize * y );
				Vector3 center = new Vector3( posX, posY, gridOrigin.z );
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
