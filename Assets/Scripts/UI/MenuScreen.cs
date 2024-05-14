using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class MenuScreen : MonoBehaviour
{
	public UIScreenTypes type;
	public VisualTreeAsset screen;

	protected VisualElement _root_;

	protected virtual void OnEnable()
	{
		_root_ = screen.CloneTree();

		GetElements();
		BindElements();
	}

	protected virtual void GetElements()
	{
	}

	protected virtual void BindElements()
	{
	}
}
