using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenuScreen : MenuScreen
{
	private Button _restartButton;
	private Button _returnButton;

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

		_restartButton = _root_.Q<Button>( "Restart" );
		_returnButton = _root_.Q<Button>( "Quit" );
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
	}

	private void OnRestartButtonClicked()
	{
		uiScreenController.ShowScreen( UIScreenTypes.HUD );
		Debug.Log( "ogbenroubg" );
	}

	private void OnReturnButtonCLicked()
	{
		uiScreenController.ShowScreen( UIScreenTypes.Main );
		Debug.Log( "ogbenroubg" );
	}
}
