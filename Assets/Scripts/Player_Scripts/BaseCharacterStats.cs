using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu( menuName = "Character" )]
public class BaseCharacterStats : ScriptableObject
{
	public float baseHP;
	public float baseATK;
	public float baseATK_SPD;
	public float baseDEF;
	public float baseSPD;
}
