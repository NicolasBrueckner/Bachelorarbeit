using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
	none,
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

public static class StatNames
{
	public static Dictionary<StatType, string> statNames = new()
	{
		{StatType.level, "Level" },
		{StatType.experience, "Experience" },
		{StatType.max_hp, "Max Healthpoints" },
		{StatType.hp, "Healthpoints" },
		{StatType.atk, "Attack" },
		{StatType.atk_spd, "Attack Speed" },
		{StatType.spd, "Speed" },
		{StatType.def, "Defense" },
		{StatType.size, "Size" },
		{StatType.duration, "Duration" },
		{StatType.pierce, "Pierce" },
	};
}

[CreateAssetMenu( menuName = "BaseStats" )]
public class BaseStats : ScriptableObject
{
	public string statName;
	public SerializedDictionary<StatType, float> valuesByStatType;

	public float this[ StatType type ]
	{
		get => valuesByStatType[ type ];
		set => valuesByStatType[ type ] = value;
	}
}
