using UnityEngine;

[CreateAssetMenu( menuName = "BaseStats/Weapons" )]
public class WeaponBaseStats : ScriptableObject
{
	public float atk;
	public float atk_spd;
	public float spd;
	public float duration;
	public float size;
	public int pierce;
}
