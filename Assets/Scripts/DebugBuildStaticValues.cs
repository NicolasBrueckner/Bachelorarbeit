using System.Collections.Generic;

public static class DebugBuildStaticValues
{
	public static int enemyAmount;
	public static float spawnFrequency;
	public static bool isInvincible = false;

	public static Dictionary<int, (float, int)> granularityByValue = new()
	{
		{1, (0.5f, 1) },
		{2, (0.25f, 2) },
		{3, (0.125f, 4) },
		{4, (0.0625f, 8) },
	};
}
