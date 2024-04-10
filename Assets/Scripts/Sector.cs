using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sector : MonoBehaviour
{
	public Sprite background;
	public CostType costIdentifier;
	public byte[,] cost;

	public void SetCosts()
	{
		cost = CostData.Instance.costs[ costIdentifier ];
	}
}
