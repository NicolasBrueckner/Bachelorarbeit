using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu( menuName = "FlowField/SectorStats" )]
public class SectorStats : ScriptableObject
{
	private static SectorStats _instance;

	public static SectorStats Instance
	{
		get
		{
			_instance ??= Resources.Load<SectorStats>( "SectorStats" );
			return _instance;
		}
	}

	public float cellRadius;
	public int2 gridSize;
}