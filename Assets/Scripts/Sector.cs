using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Sector : MonoBehaviour
{
	public GameObject sectorObject;
	public Sprite background;
	public CostMap costIdentifier;
	public byte[,] cost;

	public static readonly float cellRadius = 0.5f;
	public static readonly int2 gridSize = new int2( 10, 10 );
	public static readonly float2 sectorSize = new float2( 10f, 10f );

	public void SetCosts()
	{
		cost = CostData.Instance.costs[ costIdentifier ];
	}
}
