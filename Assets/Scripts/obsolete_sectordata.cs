using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu( menuName = "FlowField/obsolete/sectordata" )]
public class obsolete_sectordata : ScriptableObject
{
	public Sprite background;
	public Vector2Int editorSize;
	public byte[][] costs;

	internal int2 size;

	public void InitializeCostField()
	{
		size = new int2( editorSize.x, editorSize.y );

		if ( costs == null || costs.Length != size.x )
			costs = new byte[ size.x ][];

		for ( int x = 0; x < size.x; x++ )
		{
			if ( costs[ x ] == null || costs[ x ].Length != size.y )
				costs[ x ] = new byte[ size.y ];
		}
	}
}
