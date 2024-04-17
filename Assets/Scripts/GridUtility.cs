using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class GridUtility
{
	public static List<int2> GetUnsafeNeighborIndexes( int2 index, List<Direction> directions )
	{
		List<int2> resultIndexes = new List<int2>();

		foreach ( Direction currentDirection in directions )
			resultIndexes.Add( index + new int2( ( int2 )currentDirection.direction ) );

		return resultIndexes;
	}

	public static bool ValidateIndex( int2 index, int2 gridSize )
	{
		if ( index.x >= 0 && index.x < gridSize.x && index.y >= 0 & index.y < gridSize.y )
			return true;
		return false;
	}

	public static bool ValidateIndex( int2 index, Cell[,] grid )
	{
		int2 gridSize = new int2( grid.GetLength( 0 ), grid.GetLength( 1 ) );
		Cell currentCell = grid[ index.x, index.y ];

		if ( index.x >= 0 && index.x < gridSize.x && index.y >= 0 & index.y < gridSize.y && currentCell.cost < byte.MaxValue )
			return true;
		return false;
	}

	public static int2 GetCellFromPosition( float3 position, float3 gridOrigin, int2 gridSize )
	{
		float3 adjustedPosition = new float3( position.x - gridOrigin.x, position.y - gridOrigin.y, 0 );
		float2 normalizedPosition = new float2(
			math.clamp( adjustedPosition.x / ( gridSize.x * Sector.cellDiameter ), 0f, 1f ),
			math.clamp( adjustedPosition.y / ( gridSize.y * Sector.cellDiameter ), 0f, 1f ) );

		int2 gridPosition = new int2(
			math.clamp( ( int )math.floor( normalizedPosition.x * gridSize.x ), 0, gridSize.x - 1 ),
			math.clamp( ( int )math.floor( normalizedPosition.y * gridSize.y ), 0, gridSize.y - 1 ) );

		return gridPosition;
	}
}
