using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
	public float hp;
	public float atk;
	public float atk_spd;
	public float def;
	public float spd;

	public CharacterStats( float hp, float atk, float atk_spd, float def, float spd )
	{
		this.hp = hp;
		this.atk = atk;
		this.atk_spd = atk_spd;
		this.def = def;
		this.spd = spd;
	}
}
