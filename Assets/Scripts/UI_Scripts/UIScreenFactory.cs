using System;

public static class UIScreenFactory
{
	public static MenuScreen CreateScreen( UIScreenType types )
	{
		return types switch
		{
			UIScreenType.Main => new MainMenuScreen(),
			UIScreenType.HUD => new HUDMenuScreen(),
			UIScreenType.Pause => new PauseMenuScreen(),
			UIScreenType.GameOver => new GameOverMenuScreen(),
			UIScreenType.LevelUp => new LevelUpMenuScreen(),
			_ => throw new ArgumentException( "not a screen type" )
		};
	}
}
