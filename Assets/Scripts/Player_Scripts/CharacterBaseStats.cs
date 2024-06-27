using EditorAttributes;
using UnityEngine;

[CreateAssetMenu( menuName = "BaseStats/Character" )]
public class CharacterBaseStats : ScriptableObject
{
	[ReadOnly]
	public int level = 0;
	[ReadOnly]
	public int experience = 0;

	public float hp;
	public float atk;
	public float atk_spd;
	public float def;
	public float spd;
	public float size;
}
