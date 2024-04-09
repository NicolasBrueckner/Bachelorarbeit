using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu( menuName = "FlowField/Sector" )]
public class Sector : ScriptableObject
{
	public Sprite background;
	public CostType costIdentifier;
	public byte[,] cost;
}
