using UnityEngine.UIElements;

public class HUDMenuScreen : MenuScreen
{
	private Button _pauseButton;
	private VisualElement _healthbar;

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

		_pauseButton = Root.Q<Button>( "PauseButton" );
		_healthbar = Root.Q<VisualElement>( "Healthbar" );
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
		uiScreenController.ToggleScreen( UIScreenTypes.Pause );
	}

	private void OnHealthChanged( float healthPercentage )
	{
		_healthbar.style.width = healthPercentage;
	}
}
