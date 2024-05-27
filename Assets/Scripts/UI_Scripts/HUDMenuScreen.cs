using UnityEngine;
using UnityEngine.UIElements;

public class HUDMenuScreen : MenuScreen
{
	private Button _pauseButton;
	private VisualElement _healthbar;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void GetElements()
	{
		base.GetElements();

		_pauseButton = _root_.Q<Button>( "PauseButton" );
		_healthbar = _root_.Q<VisualElement>( "Healthbar" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_pauseButton.clicked += OnPauseButtonClicked;
	}

	private void OnPauseButtonClicked()
	{
		UIScreenManager.Instance.ShowScreen( UIScreenTypes.Pause );
	}

	private void OnHealthChanged(float healthPercentage)
	{
		_healthbar.style.width = healthPercentage;
	}
}
