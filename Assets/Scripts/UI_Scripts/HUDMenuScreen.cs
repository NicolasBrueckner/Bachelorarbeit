using UnityEngine.UIElements;

public class HUDMenuScreen : MenuScreen
{
	private Button _pauseButton;
	private VisualElement _healthbar;

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

		_pauseButton = _root_.Q<Button>( "PauseButton" );
		_healthbar = _root_.Q<VisualElement>( "Healthbar" );
	}

	protected override void BindElements()
	{
		base.BindElements();

		_pauseButton.clicked += OnPauseButtonClicked;
	}

	protected override void BindEvents()
	{
		base.BindEvents();

		EventManager.Instance.OnHealthChanged += OnHealthChanged;
	}

	private void OnPauseButtonClicked()
	{
		uiScreenController.ShowScreen( UIScreenTypes.Pause );
	}

	private void OnHealthChanged( float healthPercentage )
	{
		_healthbar.style.width = healthPercentage;
	}
}
