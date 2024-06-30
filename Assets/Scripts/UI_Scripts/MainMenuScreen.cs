using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MenuScreen
{
	private Button _startButton;
	private Button _quitButton;

	protected override void SetDefaults()
	{
		base.SetDefaults();
	}

	protected override void OnActivation()
	{
		base.OnActivation();
	}

	protected override void OnDeactivation()
	{
		base.OnDeactivation();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_startButton = _root_.Q<Button>( "Start" );
		_quitButton = _root_.Q<Button>( "Quit" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_startButton.clicked += OnStartButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();
	}

	private void OnStartButtonClicked()
	{
		uiScreenController.ShowScreen( UIScreenTypes.HUD );
	}

	private void OnQuitButtonClicked()
	{
		Application.Quit();
	}
}
