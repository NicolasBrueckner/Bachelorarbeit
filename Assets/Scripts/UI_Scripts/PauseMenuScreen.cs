using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : MenuScreen
{
	private Button _resumeButton;
	private Button _quitButton;

	protected override void SetDefaults()
	{
		base.SetDefaults();
	}

	protected override void OnActivation()
	{
		base.OnActivation();

		Time.timeScale = 0;
	}

	protected override void OnDeactivation()
	{
		base.OnDeactivation();

		Time.timeScale = 1;
	}

	protected override void GetElements()
	{
		base.GetElements();

		_resumeButton = _root_.Q<Button>( "Resume" );
		_quitButton = _root_.Q<Button>( "Quit" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_resumeButton.clicked += OnResumeButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();
	}

	private void OnResumeButtonClicked()
	{
		uiScreenController.ShowScreen( UIScreenTypes.HUD );
	}

	private void OnQuitButtonClicked()
	{
		uiScreenController.ShowScreen( UIScreenTypes.GameOver );
	}
}
