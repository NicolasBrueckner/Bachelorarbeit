using System;

public static class UIScreenFactory
{
	public static MenuScreen CreateScreen( UIScreenTypes types )
	{
		return types switch
		{
			UIScreenTypes.Main => new MainMenuScreen(),
			UIScreenTypes.HUD => new HUDMenuScreen(),
			UIScreenTypes.Pause => new PauseMenuScreen(),
			UIScreenTypes.GameOver => new GameOverMenuScreen(),
			UIScreenTypes.LevelUp => new LevelUpMenuScreen(),
			_ => throw new ArgumentException( "not a screen type" )
		};
	}
}
