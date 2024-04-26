using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
	public float hp;
	public float atk;
	public float atk_spd;
	public float def;
	public float spd;

	public CharacterStats( float hp = 100, float atk = 10, float atk_spd = 1, float def = 10, float spd = 50 )
	{
		this.hp = hp;
		this.atk = atk;
		this.atk_spd = atk_spd;
		this.def = def;
		this.spd = spd;
	}
}
