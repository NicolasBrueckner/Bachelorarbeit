using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Weapons" )]
public class WeaponBaseStats : ScriptableObject
{
	public float atk;
	public float atk_spd;
	public float spd;
	public int pierce;
}
