using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sector : MonoBehaviour
{
	public Sprite background;
	public int2 size;
	public CostType costIdentifier;

	internal byte[,] cost;

	public void SetCosts( byte[,] costs )
	{
		cost = CostData.Instance.costs[ costIdentifier ];
	}
}
