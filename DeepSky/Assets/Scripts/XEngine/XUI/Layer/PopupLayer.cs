using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using XEngine;

[RequireComponent(typeof(DLayer))]
public class PopupLayer : DLayer
{
    private Queue<DPopup> _popupQueue=new Queue<DPopup>();
    private DPopup _currentPopup = null;

	public int Instant = 0;

    public DPopup ShowPopup(DPopup popup)
	{
		if (_currentPopup == null) {
			DoShowPopup(popup);
			_currentPopup = popup;
		}else{
			_popupQueue.Enqueue (popup);
		}
			
		return popup;
	}

	private void DoShowPopup(DPopup popup)
	{
		popup.ShowPopup(this, ClosePopup);
	}

	public bool IsEmpty()
	{
		return _currentPopup == null && _popupQueue.Count == 0;
	}

	public bool HideNormalLayer {
		get {
			return _currentPopup != null && _currentPopup.HideNormalLayer;
		}
	}

	public bool IsFullScreen()
	{
		return _currentPopup != null && _currentPopup.IsFullScreen;
	}

	protected void ClosePopup()
	{
		_currentPopup = null;
		if (_popupQueue.Count > 0)
		{
			_currentPopup = _popupQueue.Dequeue();
			DoShowPopup (_currentPopup);
		}
		//jyy
		// Game.UI.UIManager.GetInstance().UpdatePopupState ();
	}

	public bool OnBackKey()
	{
		if (_currentPopup != null) {
			_currentPopup.Hide ();
			return true;
		}	

		return false;
	}

	public void Hide()
	{
		destroySelf ();
	}

	protected void destroySelf()
	{
		//jyytest
		// TSingletonComponent<GameObjectPool>.GetInstance().ReleaseGameObject (this.gameObject);
		// XEngine.Pool.PoolManager.GetInstance().DestroyGameObject(this.gameObject);
	}
}