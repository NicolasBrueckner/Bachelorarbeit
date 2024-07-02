using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum StatType
{
	level,
	experience,
	max_hp,
	hp,
	atk,
	atk_spd,
	spd,
	def,
	size,
	duration,
	pierce,
}

[CreateAssetMenu( menuName = "BaseStats" )]
public class BaseStats : ScriptableObject
{
	public SerializedDictionary<StatType, float> valuesByStatType;
}
