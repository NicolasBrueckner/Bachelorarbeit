using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public static class SectorStats
{
	private static int2 _baseGridSize = new( 15, 11 );

	public static float cellRadius = 0.5f;
	public static int gridScalar = 1;

	public static float cellDiameter = cellRadius * 2;

	public static int2 gridSize = _baseGridSize * gridScalar;

	public static float2 sectorSize = new( gridSize.x * cellDiameter, gridSize.y * cellDiameter );


}
