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
			resultIndexes.Add( index + currentDirection );

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

	//public Cell GetCellFromPosition( float3 position )
	//{
	//	float2 adjustedPosition = new float2( position.x - gridOrigin.x, position.y - gridOrigin.y );
	//	float2 normalizedPosition = new float2(
	//		math.clamp( adjustedPosition.x / ( gridSize.x * _cellSize ), 0f, 1f ),
	//		math.clamp( adjustedPosition.y / ( gridSize.y * _cellSize ), 0f, 1f ) );

	//	int2 gridPosition = new int2(
	//		math.clamp( ( int )math.floor( normalizedPosition.x * gridSize.x ), 0, gridSize.x - 1 ),
	//		math.clamp( ( int )math.floor( normalizedPosition.y * gridSize.y ), 0, gridSize.y - 1 ) );

	//	return grid[ gridPosition.x, gridPosition.y ];
	//}
}
