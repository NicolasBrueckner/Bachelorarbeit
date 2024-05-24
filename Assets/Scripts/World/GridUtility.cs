using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

public static class GridUtility
{
	public static List<int2> IndexArray2DToList( int2[,] array2D )
	{
		return Enumerable.Range( 0, array2D.GetLength( 0 ) ).SelectMany(
			i => Enumerable.Range( 0, array2D.GetLength( 1 ) ).Select( j => array2D[ i, j ] ) ).ToList();
	}

	public static List<int2> Sort2DIndexes( List<int2> indexes )
	{
		indexes.Sort( ( a, b ) => ( a.x + a.y ).CompareTo( b.x + b.y ) );
		return indexes;
	}

	public static List<int2> GetUnsafeIndexes( int2 index, List<Direction> directions )
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

		if ( index.x >= 0 && index.x < gridSize.x && index.y >= 0 & index.y < gridSize.y )
		{
			Cell currentCell = grid[ index.x, index.y ];
			if ( currentCell.cost < byte.MaxValue )
				return true;
		}
		return false;
	}

	public static int2 GetIndexFromPosition( float3 position, float3 gridOrigin, int2 gridSize )
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

	public static int2 GetIndexFromPosition( float3 position, float3 gridOrigin, int2 gridSize, float2 cellSize )
	{
		float3 adjustedPosition = position - gridOrigin;
		float2 normalizedPosition = new float2(
			math.clamp( adjustedPosition.x / ( gridSize.x * cellSize.x ), 0f, 1f ),
			math.clamp( adjustedPosition.y / ( gridSize.y * cellSize.y ), 0f, 1f ) );

		int2 gridPosition = new int2(
			math.clamp( ( int )math.floor( normalizedPosition.x * gridSize.x ), 0, gridSize.x - 1 ),
			math.clamp( ( int )math.floor( normalizedPosition.y * gridSize.y ), 0, gridSize.y - 1 ) );

		return gridPosition;
	}
}
