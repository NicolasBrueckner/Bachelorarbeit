using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class MenuScreen : MonoBehaviour
{
	public UIScreenController uiScreenController;
	public UIScreenTypes type;
	public VisualTreeAsset screen;

	protected VisualElement _root_;

	protected virtual void SetDefaults()
	{
		_root_ = screen.CloneTree();

		GetElements();
		BindElements();

		uiScreenController?.AddScreen( type, _root_ );
	}

	protected virtual void OnActivation() { }

	protected virtual void OnDeactivation() { }

	protected virtual void GetElements() { }

	protected virtual void BindElements() { }

	protected virtual void BindEvents() { }
}
