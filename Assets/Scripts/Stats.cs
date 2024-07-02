using AYellowpaper.SerializedCollections;
using EditorAttributes;

public class Stats
{
	[ReadOnly]
	public SerializedDictionary<StatType, float> valuesByStatType;

	public Stats( BaseStats baseStats )
	{
		valuesByStatType = baseStats.valuesByStatType;
	}

	public float this[ StatType type ]
	{
		get => valuesByStatType[ type ];
		set => valuesByStatType[ type ] = value;
	}

	public void SetStat( StatType type, float value )
	{
		valuesByStatType[ type ] = value;
	}

	public void IncreaseStat( StatType type, float value )
	{
		valuesByStatType[ type ] *= value;
	}
}
