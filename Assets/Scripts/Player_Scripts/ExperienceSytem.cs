using UnityEngine;

public class ExperienceSytem
{
	public int CurrentLevel { get; private set; } = 0;
	public int CurrentExperience { get; private set; } = 0;

	private float _experienceForNextLevel;
	private float _a;
	private float _b;
	private float _c;

	public ExperienceSytem( float a, float b, float c )
	{
		_a = a;
		_b = b;
		_c = c;
		_experienceForNextLevel = GetExperienceForNextLevel();
	}

	public void AddExperience( int experience )
	{
		CurrentExperience += experience;
		if ( CurrentExperience >= _experienceForNextLevel )
		{
			CurrentLevel++;
			_experienceForNextLevel = GetExperienceForNextLevel();
		}
	}

	private float GetExperienceForNextLevel()
	{
		Debug.Log( $"exp for next level: {_experienceForNextLevel}" );
		return ( _a * CurrentLevel * CurrentLevel ) + ( _b * CurrentLevel ) + _c;
	}
}
