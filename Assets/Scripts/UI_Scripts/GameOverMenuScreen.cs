using UnityEngine;
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
