using UnityEngine;

[System.Serializable]
public class CharacterStats
{
	[Header( "Current Character Stats" )]
	public int level;
	public int experience;
	public float hp;
	public float atk;
	public float atk_spd;
	public float def;
	public float spd;
	public float size;

	public CharacterStats( CharacterBaseStats baseStats )
	{
		level = baseStats.level;
		experience = baseStats.experience;
		hp = baseStats.hp;
		atk = baseStats.atk;
		atk_spd = baseStats.atk_spd;
		def = baseStats.def;
		spd = baseStats.spd;
		size = baseStats.size;
	}
}
