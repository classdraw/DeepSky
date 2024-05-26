using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XLua;
using XEngine;
using XEngine.Pool;

[LuaCallCSharp]
[ExecuteInEditMode]
public class UIView : LuaBehaviour
{
	protected DLayer _layer;
	private DModal _modal;
	public Color _color = new Color (0, 0, 0, 0);
	private GameObject _subViewLayer;
	public ResHandle m_ResHandle;

    public bool CanRespondBackKey{ get; set;}

    public bool IsModal()
	{
		return _modal != null;
	}

    protected virtual void Awake()
	{
		CanRespondBackKey = true;

		string subViewName = "SubView";
		var subViewTransform = transform.Find (subViewName);
		if (subViewTransform != null) {
			_subViewLayer = subViewTransform.gameObject;
		}else{
			_subViewLayer = new GameObject (subViewName);
			RectTransform rectTransform =  _subViewLayer.AddComponent <RectTransform>();
			rectTransform.SetParent (transform,false);
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.pivot = new Vector2 (0.5f, 0.5f);
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
		}

		_subViewLayer.hideFlags = HideFlags.DontSave;
	
		UpdateSublayerState ();
	}


	protected override void OnEnable()
	{
		base.OnEnable ();
	}


	public void SetModal(bool b)
	{
		if (b) {
			if (_modal == null) {
				var go = new GameObject ("DModal");
				_modal = go.AddComponent<DModal> ();
				_modal.color = _color;
				_modal.enabled = true;
				go.transform.SetParent (transform, false);
			}
			_modal.transform.SetSiblingIndex (0);
		} else {
			if (_modal != null) {
				GameObject.Destroy (_modal.gameObject);
				_modal = null;
			}
		}
	}

	protected virtual void Start()
	{
		
	}

	public virtual void Show(DLayer layer)
	{
		layer.AddChild (this.transform);
		this._layer = layer;
	}

	public virtual void ShowSubView(UIView subView)
	{
		subView.transform.SetParent (_subViewLayer.transform, false);
		UpdateSublayerState ();
	}

	public virtual void RemoveSubView(UIView subView)
	{
		subView.Hide ();
		UpdateSublayerState ();
	}

	public virtual bool Hide()
	{
		UIView parentView = null;
		if (transform.parent != null) {
			parentView = transform.parent.gameObject.GetComponent <UIView>();
		}

		while (_subViewLayer.transform.childCount > 0) {
			GameObject child = _subViewLayer.transform.GetChild (0).gameObject;
			UIView subView = child.GetComponent <UIView>();
			if (subView != null) {
				subView.Hide ();
			}else{
				UnityEngine.Object.Destroy(child);
			}
		}

		destroySelf ();

		if (parentView) {
			parentView.UpdateSublayerState ();
		}
		return true;
	}

	protected virtual void destroySelf()
	{
		if (_layer == null) {
			transform.SetParent (null, false);
        }else{
	        _layer.RemoveChild(this.transform);
	        _layer = null;
        }
		if(m_ResHandle!=null){
			m_ResHandle.Dispose();
			m_ResHandle=null;
		}
		
		//jyytest
		// XEngine.Pool.PoolManager.GetInstance().DestroyGameObject(this.gameObject);
	}

	void UpdateSublayerState()
	{
		_subViewLayer.transform.SetAsLastSibling ();
		_subViewLayer.SetActive (_subViewLayer.transform.HasActiveChild());

		if (transform.parent != null) {
			UIView parentView = transform.parent.gameObject.GetComponent <UIView>();
			if (parentView) {
				parentView.UpdateSublayerState ();
			}
		}
	}


	//return whether handled back key.
	public virtual bool OnBackKey(bool canRemoveSelf = true)
	{
		if (CanRespondBackKey == false) {
			return false;
		}

		if (_subViewLayer.activeInHierarchy) {
			for (int i = _subViewLayer.transform.childCount - 1; i >= 0; i--) {
				UIView subView = _subViewLayer.transform.GetChild (i).gameObject.GetComponent <UIView> ();
				if (subView != null && subView.gameObject.activeInHierarchy) {
					bool handled = subView.OnBackKey ();
					if (handled) {
						return true;
					}
				}
			}

		}

		if (isActiveAndEnabled && canRemoveSelf) {
			Hide ();
			return true;
		}

		return false;
	}
}