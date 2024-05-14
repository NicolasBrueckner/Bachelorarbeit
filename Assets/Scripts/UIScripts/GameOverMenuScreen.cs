using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenuScreen : MenuScreen
{
	private Button _restartButton;
	private Button _returnButton;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_restartButton = _root_.Q<Button>( "Restart" );
		_returnButton = _root_.Q<Button>( "Return" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_restartButton.clicked += OnRestartButtonClicked;
		_returnButton.clicked += OnReturnButtonCLicked;
	}

	private void OnRestartButtonClicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.HUD );
		Debug.Log( "ogbenroubg" );
	}

	private void OnReturnButtonCLicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.Main );
		Debug.Log( "ogbenroubg" );
	}
}
