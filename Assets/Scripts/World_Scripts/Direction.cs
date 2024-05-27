using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Direction
{
	public readonly int2 direction;

	public Direction( int x, int y )
	{
		direction = new int2( x, y );
	}

	public static implicit operator int2( Direction cellDirection )
	{
		return cellDirection.direction;
	}

	public static Direction GetDirection( int2 direction )
	{
		return trueDirections.FirstOrDefault( cellDirection => math.all( cellDirection.direction == direction ) ) ?? None;
	}

	public static readonly Direction None = new Direction( 0, 0 );
	public static readonly Direction N = new Direction( 0, 1 );
	public static readonly Direction NE = new Direction( 1, 1 );
	public static readonly Direction E = new Direction( 1, 0 );
	public static readonly Direction SE = new Direction( 1, -1 );
	public static readonly Direction S = new Direction( 0, -1 );
	public static readonly Direction SW = new Direction( -1, -1 );
	public static readonly Direction W = new Direction( -1, 0 );
	public static readonly Direction NW = new Direction( -1, 1 );

	public static readonly List<Direction> cardinals = new List<Direction>
	{
		N,
		E,
		S,
		W,
	};

	public static readonly List<Direction> trueDirections = new List<Direction>
	{
		N,
		NE,
		E,
		SE,
		S,
		SW,
		W,
		NW,
	};

	public static readonly List<Direction> allDirections = new List<Direction>
	{
		None,
		N,
		NE,
		E,
		SE,
		S,
		SW,
		W,
		NW,
	};
}
