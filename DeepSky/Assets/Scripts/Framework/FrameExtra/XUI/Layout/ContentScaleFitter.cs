using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

[AddComponentMenu ("Layout/Content Scale Fitter", 140)]
[ExecuteInEditMode]
[RequireComponent (typeof(RectTransform))]
public class ContentScaleFitter : UIBehaviour, ILayoutSelfController
{
	public enum ScaleMode
	{
		ScaleWithHeight,
		ScaleWithWidth

	}

	private bool m_Dirty = true;

	[Tooltip ("If the screen resolution is smaller, the UI will be scaled down.")]
	[RangeAttribute(1,3000)]
	[SerializeField] protected int m_ReferenceResolution = 1136;
	public int ReferenceResolution {
		get { return m_ReferenceResolution; }
		set {
			if (m_ReferenceResolution == value)
				return;
			m_ReferenceResolution = value;
			SetDirty ();
		}
	}

	[SerializeField] private ScaleMode m_ScaleMode = ScaleMode.ScaleWithWidth;

	public ScaleMode scaleMode {
		get { return m_ScaleMode; }
		set {
			if (m_ScaleMode.Equals (value))
				return;
			m_ScaleMode = value;
			SetDirty (); 
		}
	}

	[SerializeField] private Vector3 m_Scale = new Vector3 (1f, 1f, 1f);

	public Vector3 scale {
		get { return m_Scale; }
		set {
			if (m_Scale.Equals (value))
				return;
			m_Scale = value;
			SetDirty ();
		}
	}

	[System.NonSerialized]
	private RectTransform m_Rect;

	private RectTransform rectTransform {
		get {
			if (m_Rect == null)
				m_Rect = GetComponent<RectTransform> ();
			return m_Rect;
		}
	}

	[System.NonSerialized]
	private RectTransform m_CanvasRect;

	private RectTransform canvasRect {
		get {
			if (m_CanvasRect == null)
			{
					foreach(Canvas c  in GetComponentsInParent<Canvas>())
					{
						if(c.isRootCanvas)
						{
							m_CanvasRect =  c.gameObject.GetComponent<RectTransform>();
							break;
						}
					}
			}
			return m_CanvasRect;
		}
	}


	private DrivenRectTransformTracker m_Tracker;

	protected ContentScaleFitter ()
	{
	}

	#region Unity Lifetime calls

	protected override void OnEnable ()
	{
		base.OnEnable ();
		SetDirty ();
	}

	protected override void OnDisable ()
	{
		m_Tracker.Clear ();
		LayoutRebuilder.MarkLayoutForRebuild (rectTransform);
		base.OnDisable ();
		rectTransform.localScale = m_Scale;
	}

	#endregion

	protected override void OnCanvasHierarchyChanged ()
	{
		m_CanvasRect = null;
		base.OnCanvasHierarchyChanged();
	}

	protected override void OnRectTransformDimensionsChange ()
	{
		SetDirty();
	}

	private void UpdateRect ()
	{
		if (!m_Dirty)
			return;

		if (!IsActive ())
			return;
		
		if (canvasRect == null)
			return;

		m_Dirty = false;
		
		Vector2 screenSize = canvasRect.sizeDelta;
		m_Tracker.Clear ();
		m_Tracker.Add (this, rectTransform, DrivenTransformProperties.Scale);
		float ratio = 1f;
		switch (m_ScaleMode) {
		case ScaleMode.ScaleWithWidth:
			{
				if(screenSize.x < m_ReferenceResolution)
				{
					ratio = screenSize.x/m_ReferenceResolution;
				}
				break;
			}
		case ScaleMode.ScaleWithHeight:
			{
				if(screenSize.y< m_ReferenceResolution)
				{
					ratio = (float)screenSize.y/m_ReferenceResolution;
				}
				break;
			}
		}
		rectTransform.localScale = m_Scale*ratio;
	}

	public virtual void Update()
	{
		UpdateRect ();
	}

	public virtual void SetLayoutHorizontal ()
	{
		UpdateRect ();
	}

	public virtual void SetLayoutVertical ()
	{
		UpdateRect ();
	}

	protected void SetDirty ()
	{
		m_Dirty = true;

		if (!IsActive ())
			return;
		
		LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
	}


	#if UNITY_EDITOR
	protected override void OnValidate()
	{
		SetDirty();
	}

	#endif
}
