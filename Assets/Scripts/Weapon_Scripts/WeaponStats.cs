using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponStats
{
	public float atk;
	public float atk_spd;
	public float spd;
	public float duration;
	public int pierce;

	public WeaponStats( WeaponBaseStats baseStats )
	{
		atk = baseStats.atk;
		atk_spd = baseStats.atk_spd;
		spd = baseStats.spd;
		duration = baseStats.duration;
		pierce = baseStats.pierce;
	}
}
