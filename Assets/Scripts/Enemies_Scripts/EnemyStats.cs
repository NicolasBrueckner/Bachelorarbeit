using UnityEngine;

public struct EnemyStats
{
	[Header( "Current Enemy Stats" )]
	public float hp;
	public float atk;
	public float atk_spd;
	public float def;
	public float spd;
	public float size;

	public EnemyStats( EnemyBaseStats baseStats )
	{
		hp = baseStats.hp;
		atk = baseStats.atk;
		atk_spd = baseStats.atk_spd;
		def = baseStats.def;
		spd = baseStats.spd;
		size = baseStats.size;
	}
}
