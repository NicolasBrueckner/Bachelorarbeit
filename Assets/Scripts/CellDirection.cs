using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class CellDirection
{
	public readonly int2 direction;

	private CellDirection(int x, int y)
	{
		direction = new int2( x, y );
	}

	public static implicit operator int2(CellDirection cellDirection)
	{
		return cellDirection.direction;
	}

	public static CellDirection GetDirection(int2 direction)
	{
		return trueDirections.FirstOrDefault( cellDirection => math.all( cellDirection.direction == direction ) ) ?? None;
	}

	public static readonly CellDirection None = new CellDirection( 0, 0 );
	public static readonly CellDirection N = new CellDirection( 0, 1 );
	public static readonly CellDirection E = new CellDirection( 1, 0 );
	public static readonly CellDirection S = new CellDirection( 0, -1 );
	public static readonly CellDirection W = new CellDirection( -1, 0 );
	public static readonly CellDirection NE = new CellDirection( 1, 1 );
	public static readonly CellDirection SE = new CellDirection( 1, -1 );
	public static readonly CellDirection SW = new CellDirection( -1, -1 );
	public static readonly CellDirection NW = new CellDirection( -1, 1 );

	public static readonly List<CellDirection> cardinals = new List<CellDirection>
	{
		N,
		E,
		S,
		W,
	};

	public static readonly List<CellDirection> trueDirections = new List<CellDirection>
	{
		N,
		E,
		S,
		W,
		NE,
		SE,
		SW,
		NW,
	};

	public static readonly List<CellDirection> allDirections = new List<CellDirection>
	{
		None,
		N,
		E,
		S,
		W,
		NE,
		SE,
		SW,
		NW,
	};
}
