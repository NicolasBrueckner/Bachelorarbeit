using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu( menuName = "FlowField/Sector" )]
public class SectorData : ScriptableObject
{
	public Sprite background;
	public Vector2Int editorSize;
	public byte[][] cost;

	internal int2 size;

	public void InitializeCostField()
	{
		size = new int2( editorSize.x, editorSize.y );

		if ( cost == null || cost.Length != size.x )
			cost = new byte[ size.x ][];

		for ( int x = 0; x < size.x; x++ )
		{
			if ( cost[ x ] == null || cost[ x ].Length != size.y )
				cost[ x ] = new byte[ size.y ];
		}
	}
}
