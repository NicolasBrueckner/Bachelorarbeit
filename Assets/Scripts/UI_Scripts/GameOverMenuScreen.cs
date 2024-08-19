using UnityEngine.UIElements;

public class GameOverMenuScreen : MenuScreen
{
	private Button _restartButton;
	private Button _returnButton;

	protected override void SetDefaultsInternal( UIScreenTypes type, UIScreenController controller )
	{
		base.SetDefaultsInternal( type, controller );
	}

	protected override void OnActivationInternal()
	{
		base.OnActivationInternal();

		EventManager.Instance.EndGame();
	}

	protected override void OnDeactivationInternal()
	{
		base.OnDeactivationInternal();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_restartButton = Root.Q<Button>( "Restart" );
		_returnButton = Root.Q<Button>( "Quit" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_restartButton.clicked += OnRestartButtonClicked;
		_returnButton.clicked += OnReturnButtonCLicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		EventManager.Instance.OnPlayerDied += OnPlayerDied;
	}

	private void OnRestartButtonClicked()
	{
		EventManager.Instance.StartGame();
		uiScreenController.ToggleScreen( UIScreenTypes.HUD );
	}

	private void OnReturnButtonCLicked()
	{
		uiScreenController.ToggleScreen( UIScreenTypes.Main );
	}

	private void OnPlayerDied()
	{
		uiScreenController.ToggleScreen( Type );
	}
}
