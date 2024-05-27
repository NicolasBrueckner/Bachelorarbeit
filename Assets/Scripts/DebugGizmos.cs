using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum FlowFieldDisplayType
{
	None,
	All,
	Cost,
	Integration,
	Destination,
	RealDirection,
}

public class DebugGizmos : MonoBehaviour
{
	public bool showGrid;
	public FlowFieldDisplayType displayType;

	private Dictionary<Direction, string> _directionToIcon;
	private FlowField _currentGrid;
	private float3 _gridOrigin;
	private int2 _gridSize;

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

		string label = cell.cost switch
		{
			0 => "0",
			byte.MaxValue => "\u221E",
			_ => _directionToIcon.TryGetValue( cell.Direction as Direction, out label ) ? label : "NA",
		};

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

		GUIStyle style = new( GUI.skin.label )
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
			case FlowFieldDisplayType.RealDirection:
				foreach ( Cell cell in _currentGrid.Cells )
					Handles.Label( cell.position, cell.realDirection.ToString(), style );
				break;
			default:
				break;
		}
	}

	private void InitializeDirectionToIcon()
	{
		string[] icons = { "\u2191", "\u2197", "\u2192", "\u2198", "\u2193", "\u2199", "\u2190", "\u2196" };

		_directionToIcon = new Dictionary<Direction, string>
		{
			{Direction.N, icons[0] },
			{Direction.NE, icons[1] },
			{Direction.E, icons[2] },
			{Direction.SE, icons[3] },
			{Direction.S, icons[4] },
			{Direction.SW, icons[5] },
			{Direction.W, icons[6] },
			{Direction.NW, icons[7] }
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
				Vector3 center = new( posX, posY, gridOrigin.z );
				Gizmos.DrawWireCube( center, size );
			}
		}
	}
}
