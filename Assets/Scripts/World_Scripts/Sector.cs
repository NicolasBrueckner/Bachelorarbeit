using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TerrainUtils;
using stats = SectorStats;

[System.Serializable]
public class Sector
{
	public Vector3 Position { get; private set; }
	public int2 Index { get; private set; }
	public byte[,] Costs { get; private set; }

	public Sector( Vector3 position, int2 index )
	{
		Position = position;
		Index = index;

		Costs = new byte[ stats.gridSize.x, stats.gridSize.y ];

		SetCostField();
	}

	private void SetCostField()
	{
		float cellDiameter = stats.cellDiameter;
		float2 gridSize = stats.gridSize;

		int terrainMask = LayerMask.GetMask( "cost_1", "cost_2", "cost_3", "cost_255" );

		for ( int x = 0; x < gridSize.x; x++ )
		{
			for ( int y = 0; y < gridSize.y; y++ )
			{
				Vector3 position = Position + new Vector3( 0.5f, 0.5f, 0f ) + new Vector3( x * cellDiameter, y * cellDiameter, 0f );
				Collider2D[] terrain = Physics2D.OverlapPointAll( position, terrainMask );

				Costs[ x, y ] = SetCost( terrain );
			}
		}
	}

	private byte SetCost( Collider2D[] terrain )
	{
		int highestLayer = terrain.Length > 0 ? terrain.Max( collider => collider.gameObject.layer ) : -1;

		return highestLayer switch
		{
			9 => 1,
			10 => 2,
			11 => 3,
			_ => byte.MaxValue,
		};
	}
}
