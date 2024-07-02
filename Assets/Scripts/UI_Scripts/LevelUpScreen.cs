using System.Collections.Generic;
using UnityEngine.UIElements;

public class LevelUpScreen : MenuScreen
{
	private Button item1;
	private Button item2;
	private Button item3;
	private Dictionary<(string, string), VisualElement> _labelsByElement = new();

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
	}

	protected override void BindElements()
	{
		base.BindElements();
	}

	protected override void BindEvents()
	{
		base.BindEvents();
	}
}