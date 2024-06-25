using UnityEngine;

[System.Serializable]
public class WeaponStats
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
		Debug.Log( $"pierce before initialization: {baseStats.pierce}" );
		atk = baseStats.atk;
		atk_spd = baseStats.atk_spd;
		spd = baseStats.spd;
		duration = baseStats.duration;
		size = baseStats.size;
		pierce = baseStats.pierce;
		Debug.Log( $"pierce after initialization: {baseStats.pierce}" );
	}
}
