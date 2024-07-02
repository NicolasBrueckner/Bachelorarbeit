using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : MenuScreen
{
	private Button _resumeButton;
	private Button _quitButton;

	protected override void SetDefaultsInternal( UIScreenTypes type, UIScreenController controller )
	{
		base.SetDefaultsInternal( type, controller );
	}

	protected override void OnActivationInternal()
	{
		base.OnActivationInternal();

		Time.timeScale = 0;
	}

	protected override void OnDeactivationInternal()
	{
		base.OnDeactivationInternal();

		Time.timeScale = 1;
	}

	protected override void GetElements()
	{
		base.GetElements();

		_resumeButton = Root.Q<Button>( "Resume" );
		_quitButton = Root.Q<Button>( "Quit" );
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
