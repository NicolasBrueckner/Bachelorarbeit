using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MenuScreen
{
	private Button _startButton;
	private Button _quitButton;

	protected override void SetDefaultsInternal( UIScreenTypes type, UIScreenController controller )
	{
		base.SetDefaultsInternal( type, controller );
	}

	protected override void OnActivationInternal()
	{
		base.OnActivationInternal();
	}

	protected override void OnDeactivationInternal()
	{
		base.OnDeactivationInternal();
	}

	protected override void GetElements()
	{
		base.GetElements();

		Debug.Log( $"entered GetElements() in {Type} screen" );

		_startButton = Root.Q<Button>( "Start" );
		_quitButton = Root.Q<Button>( "Quit" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		Debug.Log( $"entered BindElements() in {Type} screen" );

		_startButton.clicked += OnStartButtonClicked;
		_quitButton.clicked += OnQuitButtonClicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();
	}

	private void OnStartButtonClicked()
	{
		Debug.Log( $"entered OnStartButtonCicked()" );
		EventManager.Instance.StartGame();
		uiScreenController.ToggleScreen( UIScreenTypes.HUD );
	}

	private void OnQuitButtonClicked()
	{
		Application.Quit();
	}
}
