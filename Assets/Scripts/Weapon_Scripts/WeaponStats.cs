using UnityEngine;

[System.Serializable]
public struct WeaponStats
{
	[Header( "Current Weapon Stats" )]
	public float atk;
	public float atk_spd;
	public float spd;
	public float duration;
	public float size;
	public int pierce;

	public WeaponStats( WeaponBaseStats baseStats )
	{
		atk = baseStats.atk;
		atk_spd = baseStats.atk_spd;
		spd = baseStats.spd;
		duration = baseStats.duration;
		size = baseStats.size;
		pierce = baseStats.pierce;
	}
}
