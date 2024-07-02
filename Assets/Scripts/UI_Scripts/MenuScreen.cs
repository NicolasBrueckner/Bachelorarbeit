using UnityEngine.UIElements;

[System.Serializable]
public class MenuScreen
{
	public VisualTreeAsset screen;

	public VisualElement Root { get; private set; }

	protected UIScreenTypes type;
	protected UIScreenController uiScreenController;

	public void SetDefaults( UIScreenTypes type, UIScreenController controller )
	{
		SetDefaultsInternal( type, controller );
	}

	protected virtual void SetDefaultsInternal( UIScreenTypes type, UIScreenController controller )
	{
		Root = screen.CloneTree();

		GetElements();
		BindElements();
		BindEvents();
	}

	public void OnActivation()
	{
		OnActivationInternal();
	}

	protected virtual void OnActivationInternal() { }

	public void OnDeactivation()
	{
		OnDeactivationInternal();
	}

	protected virtual void OnDeactivationInternal() { }

	protected virtual void GetElements() { }

	protected virtual void BindElements() { }

	protected virtual void BindEvents() { }
}
