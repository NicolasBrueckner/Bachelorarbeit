using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
	[Header( "Current Character Stats" )]
	public float hp;
	public float atk;
	public float atk_spd;
	public float def;
	public float spd;

	public CharacterStats( BaseCharacterStats baseStats )
	{
		hp = baseStats.baseHP;
		atk = baseStats.baseATK;
		atk_spd = baseStats.baseATK_SPD;
		def = baseStats.baseDEF;
		spd = baseStats.baseSPD;
	}
}
