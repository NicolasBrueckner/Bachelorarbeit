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

	public static implicit operator int2( Direction cellDirection ) =>
		cellDirection.direction;

	public static Direction GetDirection( int2 direction ) =>
		trueDirections.FirstOrDefault( cellDirection => math.all( cellDirection.direction == direction ) ) ?? None;

	public static readonly Direction None = new( 0, 0 );
	public static readonly Direction N = new( 0, 1 );
	public static readonly Direction NE = new( 1, 1 );
	public static readonly Direction E = new( 1, 0 );
	public static readonly Direction SE = new( 1, -1 );
	public static readonly Direction S = new( 0, -1 );
	public static readonly Direction SW = new( -1, -1 );
	public static readonly Direction W = new( -1, 0 );
	public static readonly Direction NW = new( -1, 1 );

	public static readonly List<Direction> cardinals = new()
	{
		N,
		E,
		S,
		W,
	};

	public static readonly List<Direction> trueDirections = new()
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

	public static readonly List<Direction> allDirections = new()
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
