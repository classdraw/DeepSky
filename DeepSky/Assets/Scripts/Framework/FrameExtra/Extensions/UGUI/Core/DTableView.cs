//
//  DTableView.cs
//	列表组件
//
//  Created by heven on 2016/6/13 11:55:1.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[XLua.CSharpCallLua]
public delegate DTableViewCell LDataSourceAdapter (DTableView table, int index);

[XLua.CSharpCallLua]
public delegate float LDataSourceCellSize (DTableView table, int index);

[XLua.CSharpCallLua]
public delegate void FreeCell (int index);
[XLua.CSharpCallLua]
public delegate void ScrollEnd ();

public enum TableViewSelectionMode{
	None = 0,
	Single,
	Multiple,
}

public struct TableViewCellInfo
{
	public float positionX;
	public float positionY;
	public bool isSelected;
}

[AddComponentMenu ("UI/Extensions/DTableView")]
[XLua.LuaCallCSharp]
public class DTableView : ScrollRect
{
	public static int INVALID_INDEX = -1;

	public enum ScrollDirection
	{
		HORIZONTAL = 1,
		VERTICAL = 2
	}
			
	public TableViewSelectionMode selectionMode = TableViewSelectionMode.Single;
	public bool allowCellSwitchOff = false;

	protected int cellsCount;
	public int CellsCount
	{
		get{return cellsCount;}
		set{
			if (cellsCount > value) {
				cellsInfo.RemoveRange (value, cellsCount - value);
			}else if (cellsCount < value) {
				for (int i = 0; i <= value - cellsCount; i++) { //多加一条
					cellsInfo.Add (new TableViewCellInfo());
				}
			}

			cellsCount = value;
		}
	}

	public Vector2 cellsSize;
	public int cellOffsetX;
	public int cellOffsetY;
	public int firstCellOffset;
	protected List<DTableViewCell> cellsUsed;
	protected Dictionary<string,List<DTableViewCell>> cellsFreed;
	protected List<TableViewCellInfo> cellsInfo;
	protected int lastSelectedIndex = INVALID_INDEX;
	protected HashSet<int> indices;
	//	public DTableViewCell cellTemplate;
	public LDataSourceAdapter onDataSourceAdapterHandler;
	public LDataSourceCellSize onDataSourceCellSizeHandler;
	public FreeCell freeCell;
	public ScrollEnd scrollEnd;
	private float loadMoreDistance = 10.0f;

	private bool m_HasRebuiltLayout = false;

	public float LoadMoreDistance{ get { return loadMoreDistance; } set { loadMoreDistance = value; } }

	public ScrollDirection direction = ScrollDirection.VERTICAL;

	float bottomOffset = 0.0f;

	public float BottomOffset { get { return bottomOffset; } set { bottomOffset = value; } }

	[SerializeField]
	bool m_VOrderingTopDown = false;
	public bool VOrderingTopDown {
		get { return m_VOrderingTopDown; } 
		set {
			if (m_VOrderingTopDown == value)
				return; 
				
			m_VOrderingTopDown = value;
			reloadData (false); 
		} 
	}

	public bool scrollable = true;

	public DTableView ()
	{
		CellsCount = 0;
		cellsSize = Vector2.zero;
		cellsUsed = new List<DTableViewCell> ();
		cellsFreed = new Dictionary<string, List<DTableViewCell>> ();
		cellsInfo = new List<TableViewCellInfo> ();
		indices = new HashSet<int> ();
	}

	protected override void Awake ()
	{
		base.Awake ();
		horizontal = direction == ScrollDirection.HORIZONTAL;
		vertical = direction == ScrollDirection.VERTICAL;
	}

	public override void Rebuild(CanvasUpdate executing)
	{
		base.Rebuild (executing);
		if (executing == CanvasUpdate.PostLayout)
		{
			m_HasRebuiltLayout = true;
		}
	}

	protected override void OnDisable()
	{
		m_HasRebuiltLayout = false;
		base.OnDisable();
	}


	public int GetSelectedCell()
	{
		return lastSelectedIndex;
	}

	public List<int> GetSelectedCells()
	{
		var len = Mathf.Min(CellsCount,cellsInfo.Count);
		var indexes = new List<int> (len);

		for (int i = 0; i < len; i++) {
			if (cellsInfo [i].isSelected)
				indexes.Add (i);
		}
		return indexes;
	}

	public void SetSelectedCell(int index)
	{
		UnSelectAllCells ();
		switch (selectionMode) {
			case TableViewSelectionMode.None:
				return;
			case TableViewSelectionMode.Single:
				OnCellSelectStateChanged (index);
				break;
			case TableViewSelectionMode.Multiple:
				OnCellSelectStateChanged (index);
				break;
			default:
				break;
		}
	}

	public void SetSelectedCells(List<int> indexes)
	{
		if (indexes == null || indexes.Count == 0) {
			return;
		}

		UnSelectAllCells ();
		switch (selectionMode) {
			case TableViewSelectionMode.None:
				return;
			case TableViewSelectionMode.Single:
					OnCellSelectStateChanged (indexes [0]);
				break;
			case TableViewSelectionMode.Multiple:
				for (int i = 0; i < indexes.Count; i++) {
					OnCellSelectStateChanged (indexes[i]);
				}
				break;
			default:
				break;
		}
	}

	public void SelectAllCells()
	{
		if (selectionMode != TableViewSelectionMode.Multiple) {
			return;
		}

		for (int i = 0; i < CellsCount; i++) {
			if (cellsInfo[i].isSelected == false) {
				OnCellSelectStateChanged (i);
			}
		}
	}

	public void UnSelectAllCells()
	{
		for (int i = 0; i < CellsCount; i++) {
			if (cellsInfo[i].isSelected) {
				OnCellSelectStateChanged (i);
			}
		}
	}

	private void OnCellSelectStateChanged(int idx)
	{
		if (selectionMode == TableViewSelectionMode.None) {
			return;
		}

		if (idx < 0 || idx >= CellsCount) {
			return;
		}

		bool selected = true;
		if (allowCellSwitchOff) {
			selected = !(cellsInfo [idx].isSelected);
		}

		TableViewCellInfo info;

		if (selectionMode == TableViewSelectionMode.Single && lastSelectedIndex != INVALID_INDEX) {
			if (idx != lastSelectedIndex && selected) {
				if (cellsInfo.Count > lastSelectedIndex) {
					info = cellsInfo [lastSelectedIndex];
					info.isSelected = false;
					cellsInfo [lastSelectedIndex] = info;
					foreach(var cell in cellsUsed)
					{
						if (cell.idx == lastSelectedIndex) {
							cell.SetSelecetState (false);
						}
					}
				}
			}
		}

		if (selected) {
			lastSelectedIndex = idx;
		}else{
			lastSelectedIndex = INVALID_INDEX;
		}

		info = cellsInfo [idx];
		info.isSelected = selected;
		cellsInfo [idx] = info;

		DTableViewCell _cell = GetCellFormCellsUsed(idx);
		if (_cell != null) {
			_cell.SetSelecetState (selected);
		}
	}

	public DTableViewCell GetCellFormCellsUsed(int idx)
	{
		foreach(var cell in cellsUsed)
		{
			if (cell.idx == idx) {
				return cell;
			}
		}

		return null;
	}

	private List<DTableViewCell> GetOrAddFreedList (string Identifier)
	{
		if (cellsFreed.ContainsKey (Identifier)) {
			return cellsFreed [Identifier];
		}
		var list = new List<DTableViewCell> ();
		cellsFreed.Add (Identifier, list);
		return list;
	}

	public void AddToFreedList (DTableViewCell cell)
	{
		var list = GetOrAddFreedList (cell.Identifier);
		list.Add (cell);
		cell.gameObject.transform.SetParent (null);
		cell.gameObject.SetActive (false);
		if (freeCell != null) {
			freeCell (cell.idx);
		}
		cell.reset ();
	}

	public virtual void reloadData (bool resetOffset = true)
	{
		for (int i = 0; i < cellsUsed.Count; i++) {
			DTableViewCell cell = cellsUsed [i];
			AddToFreedList (cell);
		}

		cellsUsed.Clear ();
		indices.Clear ();
		resetCellsInfo ();

		for (int i = 0; i < content.childCount; ++i) {
			GameObject.Destroy (content.GetChild (i).gameObject);
		}

		updatePositions ();
		updateContentSize ();
		if(resetOffset){
			setContentOffsetToTop ();
		}
		onScrolling ();
		CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild (this);
		CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild (this);
	}

	public void resetCellsInfo()
	{
		for (int i = 0; i < cellsInfo.Count; i++) {
			TableViewCellInfo info = cellsInfo [i];
			info.isSelected = false;
			info.positionX = 0;
			info.positionY = 0;
			cellsInfo [i] = info;
		}
	}

	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange ();
		updateContentSize ();
	}

	public void setContentOffsetToTop ()
	{
		if (direction == ScrollDirection.VERTICAL) {
			verticalNormalizedPosition = 1;
		} else {
			SetContentAnchoredPosition (Vector2.zero);
		}
	}

	public void setContentOffsetToBottom ()
	{
		if (direction == ScrollDirection.VERTICAL) {
/*            Vector2 np = normalizedPosition;
            np.y = 0;
            normalizedPosition = np;*/
			verticalNormalizedPosition = 0;
		}
	}

	public void ScrollToCell(int idx)
	{
		if (idx < 0 || idx > cellsCount) {
			return;
		}

		var contentSize = this.content.rect.size;

		if (direction == ScrollDirection.VERTICAL) {
			var offsetY = Mathf.Abs (cellPositionFromIndex (idx).y);
			if (idx > 0) {
				offsetY += idx * cellOffsetY;
			}

			verticalNormalizedPosition = (contentSize.y - offsetY ) / contentSize.y;
		} else {
			//TODO 待测试
			var offsetX = Mathf.Abs (cellPositionFromIndex (idx).x);
			if (idx > 0) {
				offsetX += idx * cellOffsetX;
			}

			horizontalNormalizedPosition = offsetX / contentSize.x;
		}

		onScrolling ();
	}

	public void removeAllFromUsed ()
	{
		foreach (DTableViewCell cell in cellsUsed) {
#if UNITY_EDITOR
            DestroyImmediate(cell.gameObject);
#else
                    Destroy(cell.gameObject);
#endif
        }
        cellsUsed.Clear ();
	}

	public void removeAllFromFreed ()
	{
		foreach (var list in cellsFreed) {
			foreach (var cell in list.Value) {
				if (cell != null && cell.gameObject != null) {
#if UNITY_EDITOR
                    DestroyImmediate(cell.gameObject);
#else
                    Destroy(cell.gameObject);
#endif

                }
            }
			list.Value.Clear ();
		}
		cellsFreed.Clear ();
	}

	#region IPooledComponent implementation

	public void Get ()
	{
		
	}

	public void Release ()
	{
		OnDestroy ();
	}

	#endregion

	protected override void LateUpdate ()
	{
		base.LateUpdate ();

		if (!IsActive())
			return;
		
		EnsureLayoutHasRebuilt ();
		onScrolling ();
	}

	protected override void OnDestroy ()
	{
		onDataSourceAdapterHandler = null;
		onDataSourceCellSizeHandler = null;
		removeAllFromUsed ();
		removeAllFromFreed ();
		base.OnDestroy ();
	}

	protected void onScrolling ()
	{
		if (onDataSourceAdapterHandler == null)
			return;
		
		if (CellsCount == 0) {
			return;
		}

		int beginIdx = 0, endIdx = 0;

		Vector2 offset = getContentOffset ();
		if (!VOrderingTopDown) {
				offset *= -1;
		}

		beginIdx = IndexFromOffset (offset);
		if (beginIdx == INVALID_INDEX) {
			beginIdx = CellsCount - 1;
		}

		if (VOrderingTopDown) {
			offset.y += viewRect.rect.height;
		} else {
			offset.y -= viewRect.rect.height;
		}

		offset.x += viewRect.rect.width;

		endIdx = IndexFromOffset (offset);
		if (endIdx == INVALID_INDEX) {
			endIdx = CellsCount - 1;
		}

		while (cellsUsed.Count > 0) {
			DTableViewCell cell = cellsUsed [0];
			int idx = cell.idx;

			if (idx < beginIdx) {
				indices.Remove (idx);
				cellsUsed.Remove (cell);
				AddToFreedList (cell);
			} else {
				break;
			}
		}

		while (cellsUsed.Count > 0) {
			DTableViewCell cell = cellsUsed [cellsUsed.Count - 1];
			int idx = cell.idx;

			if (idx > endIdx && idx < CellsCount) {
				indices.Remove (idx);
				cellsUsed.RemoveAt (cellsUsed.Count - 1);
				AddToFreedList (cell);
			} else {
				break;
			}
		}

		for (int idx = beginIdx; idx <= endIdx && idx < CellsCount; ++idx) {
			if (indices.Contains (idx)) {
				continue;
			}
			updateCellAtIndex (idx);
		}


	}

	private void EnsureLayoutHasRebuilt()
	{
		if (!m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			Canvas.ForceUpdateCanvases();
	}


	protected void setContainerSize (Vector2 size)
	{
		//重设content anchor , pivot,防止prefab中的错误设置导致显示出错
		if (direction == ScrollDirection.HORIZONTAL) {
			content.anchorMin = Vector2.zero;
			content.anchorMax = new Vector2 (0f, 1f);
			content.pivot = new Vector2 (0f, 1f);
		} else {
			content.anchorMin = new Vector2 (0f, 1f);
			content.anchorMax = Vector2.one;
			if (VOrderingTopDown) {
				content.pivot = Vector2.one;
			} else {
				content.pivot = new Vector2 (1f, 0f);
			}
		}

		Vector2 cs = viewRect.rect.size;//safe
        if (direction == ScrollDirection.HORIZONTAL)
        {
            float width = Mathf.Max(cs.x, size.x);
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
        else
        {
            float height = Mathf.Max(cs.y, size.y);
			//float preHeight = content.sizeDelta.y;
			//float prePosY = content.anchoredPosition.y;
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            //the reason of adding the below code are :
            //first we should know that  if we want to modify the right ,bottom ,left ,top of the RectTransfrom,we should to modify the offsetMin (left,bottom),offsetMax(right,top)
            //second we should know that the content's RectTransform's some properties should be equal the viewPoint(eg:width).In the tableView the widths of content and viewpoint are equals so the left-padding should be zero
            // for the positionY of the content , the positionY is the bottom of the content to the top of the viewPoint, the content should be not moved when we 
			//change the size of the content.........(you can think that we want the distance of the top of the content to  the top of the viewPoint should not change.I think pivot should be (i,1) i is a number,so if you change the
			// content size ,we should not re-calculate the positionY of the content)
			//content.sizeDelta = new Vector2(content.sizeDelta.x,height);
			//EnsureLayoutHasRebuilt ();
			//float currentPosY = prePosY - (content.sizeDelta.y - preHeight);
			//content.anchoredPosition = new Vector2 (content.anchoredPosition.x,currentPosY);
			//Debug.LogError ("we set the position y " + currentPosY + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			content.offsetMin = new Vector2(0,content.offsetMin.y);
        }
		//content.SetSizeWithCurrentAnchors(new Vector2(width, height));//Extension Method we should only set width or height for Horizontal or Vertical

	}

	protected Vector2 GetContentSize ()
	{
		return content.rect.size;
	}

	protected void updateContentSize ()
	{

//		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform());
		Vector2 size = Vector2.zero;

		if (CellsCount > 0) {
			TableViewCellInfo info = cellsInfo [CellsCount];
			float maxPosition = (direction == ScrollDirection.HORIZONTAL ? info.positionX  : info.positionY) + bottomOffset;

			switch (direction) {
			case ScrollDirection.HORIZONTAL:
				size.Set (maxPosition, cellsSize.y);
				break;
			default:
				size.Set (cellsSize.x, maxPosition);
				break;
			}
		}
		setContainerSize (size);
	}

	protected void updatePositions ()
	{
		float currentPos = firstCellOffset;
		Vector2 cSize = Vector2.zero;

		for (int i = 0; i < cellsInfo.Count; i++) {
			
			TableViewCellInfo info = cellsInfo [i];
			if (i < cellsCount) {
				cSize = GetCellSize (i);
			} else {
				cSize = Vector2.zero;
			}

			switch (direction) {
			case ScrollDirection.HORIZONTAL:
				info.positionX = currentPos;
				info.positionY = cellOffsetY;
				currentPos += cSize.x + cellOffsetX;
				break;
			default:
				info.positionX = cellOffsetX;
				info.positionY = currentPos;
				currentPos += cSize.y + cellOffsetY;
				break;
			}

			cellsInfo [i] = info;
		}
	}

	public DTableViewCell DequeueReusableCellWithIdentifier (string Identifier)
	{
		var list = GetOrAddFreedList (Identifier);
		return dequeueCell (list);
	}

	protected DTableViewCell dequeueCell (List<DTableViewCell> list)
	{
		DTableViewCell cell = null;
		if (list.Count == 0) {
			return null;
		} else {
			cell = list [list.Count - 1];
			list.Remove (cell);
			if (cell == null) //防止go被销毁
				return null;
		}
		return cell;
	}

	protected int IndexFromOffset (Vector2 offset)
	{
		int index = 0;
		int maxIdx = CellsCount - 1;

		if (!VOrderingTopDown) {
			offset.y = GetContentSize ().y - offset.y;
		}

		index = __indexFromOffset (offset);
		if (index != INVALID_INDEX) {
			index = Mathf.Max (0, index);
			if (index > maxIdx) {
				index = INVALID_INDEX;
			}
		}

		return index;
	}

	int __indexFromOffset (Vector2 offset)
	{
		if (CellsCount == 0) {
			return 0;
		}

		int low = 0;
		int high = CellsCount - 1;
		float search;
		switch (direction) {
		case ScrollDirection.HORIZONTAL:
			search = offset.x;
			break;
		default:
			search = offset.y;
			break;
		}

		while (high >= low) {
			int index = low + (high - low) / 2;
			float cellStart = direction == ScrollDirection.HORIZONTAL ? cellsInfo [index].positionX : cellsInfo [index].positionY;
			float cellEnd = direction == ScrollDirection.HORIZONTAL ? cellsInfo [index + 1].positionX : cellsInfo [index + 1].positionY;

			if (search >= cellStart && search <= cellEnd) {
				return index;
			} else if (search < cellStart) {
				high = index - 1;
			} else {
				low = index + 1;
			}
		}

		if (low <= 0) {
			return 0;
		}

		return INVALID_INDEX;
	}

	protected Vector2 GetCellSize (int index)
	{
		var c = cellsSize;
		if (onDataSourceCellSizeHandler == null)
			return c;
		
		switch (direction) {
		case ScrollDirection.HORIZONTAL:
			{
				c.x = onDataSourceCellSizeHandler (this, index);
				break;
			}
		default:
			{
				c.y = onDataSourceCellSizeHandler (this, index);
				break;
			}
		}
		return c;
	}

	protected Vector2 cellPositionFromIndex (int idx)
	{
		Vector2 offset = Vector2.zero;

		if (idx == DTableView.INVALID_INDEX) {
			return offset;
		}

//		Vector2 cellSize = GetCellSize (idx);
	
		offset.x = cellsInfo [idx].positionX;	
		offset.y = cellsInfo [idx].positionY;

//		offset.y = GetContentSize ().y - offset.y - cellSize.y;
//		offset.y = GetContentSize ().y - offset.y - cellOffsetY;

//		offset.y = GetContentSize ().y - offset.y - cellSize.y;

		offset.y *= -1;

		return offset;
	}

	protected void insertSortableCell (DTableViewCell cell, int idx)
	{
		if (cellsUsed.Count == 0) {
			cellsUsed.Add (cell);
		} else {
			for (int i = 0; i < cellsUsed.Count; i++) {
				if (cellsUsed [i].idx > idx) {
					cellsUsed.Insert (i, cell);
					return;
				}
			}
			cellsUsed.Add (cell);
		}
	}

	public DTableViewCell cellAtIndex (int idx)
	{
		if (!indices.Contains (idx)) {
			return null;
		}
		for (int i = 0; i < cellsUsed.Count; i++) {
			if (cellsUsed [i].idx == idx) {
				return cellsUsed [i];
			}
		}
		return null;
	}

	public virtual void updateCellAtIndex (int idx)
	{
		DTableViewCell cell = onDataSourceAdapterHandler.Invoke (this, idx);
		if (cell == null) {
			Debug.LogError ("cell can not be NULL");
		}
		cell.gameObject.SetActive (true);
		cell.idx = idx;
		RectTransform rtran = cell.gameObject.GetComponent<RectTransform> ();
		cell.ChangeTableSelectedIndex = OnCellSelectStateChanged;
		cell.SetSelecetState (cellsInfo[idx].isSelected);

		rtran.pivot = new Vector2 (0, 1);
		rtran.anchorMax = new Vector2 (0, 1);
		rtran.anchorMin = new Vector2 (0, 1);

        //rtran.sizeDelta = cellsSize;
        rtran.SetParent (content);
        rtran.localScale = Vector2.one;
//		cell.gameObject.transform.localPosition = cellPositionFromIndex(idx);
		//
		Vector2 v2 = cellPositionFromIndex (idx);
		Vector3 v3 = new Vector3 (v2.x, v2.y, 0);
		rtran.anchoredPosition3D = v3;
		rtran.anchoredPosition = v2;
		rtran.rotation = Quaternion.identity;
		insertSortableCell (cell, idx);
		indices.Add (idx);
	}

	public Vector2 getContentOffset ()
	{
		return content.anchoredPosition;
	}

	//	public void setContentOffsetInDuration(Vector2 offset, float duration)
	//	{
	//		if (bounceable)
	//		{
	//			validateOffset(ref offset);
	//		}
	//		setContentOffsetInDurationWithoutCheck(offset, duration);
	//	}

	public override void OnBeginDrag (PointerEventData eventData)
	{
		if (!scrollable) {
			return;
		}
		base.OnBeginDrag (eventData);
	}

	public override void OnDrag (PointerEventData eventData)
	{
		if (!scrollable) {
			return;
		}
		base.OnDrag (eventData);

		if (direction == ScrollDirection.VERTICAL && scrollEnd != null) {
			if (content.sizeDelta.y <= viewRect.rect.height) {
				return;
			}

			if ( content.anchoredPosition.y  >= loadMoreDistance) {
				scrollEnd ();
			}
		}
	}

	public void ReCalculateContent ()
	{
		resetCellsInfo ();
		updatePositions ();
		updateContentSize ();
		for (int i = 0; i < cellsUsed.Count; i++) {
			DTableViewCell cell = cellsUsed [i];
			cell.transform.localScale = Vector2.one;
			Vector2 v2 = cellPositionFromIndex (cell.idx);
			Vector3 v3 = new Vector3 (v2.x, v2.y, 0);
			var rect = cell.gameObject.GetComponent<RectTransform> ();
			rect.anchoredPosition3D = v3;
			rect.anchoredPosition = v2;
			rect.rotation = Quaternion.identity;
		}
		onScrolling ();
	}
	public void RefreshCellAtIndex(int index)
	{
		if (onDataSourceAdapterHandler == null)
			return;

		if (CellsCount == 0 ) {
			return;
		}
		while (cellsUsed.Count > 0) {
			DTableViewCell cell = cellsUsed [0];
			int idx = cell.idx;

			if (idx == index) {
				LuaDTableViewCell luaDTableViewCell =	cell.GetComponent<LuaDTableViewCell> ();
				if (luaDTableViewCell == null) {
					return;
				}
				luaDTableViewCell.Refresh ();
				return;
			} 
		}
	}
	/*************************************************************************************************************
	//if the index of the cell that you want to remove is not in the viewPoint. you should do nothing?
	//of course ,it is not right. because before and after deleting, the i ( i = index + 1,2,3.......) may be change.....
	//but the cell at the bottom of the viewPoint , you should do noting....
	//so if the cell is in the viewPoint and the idX is not lower than the removeIndex, we should re-create or refresh it
	// so we delete it from cellsUsed and then call onScrolling to refresh content 
	**************************************************************************************************************/
	public void RemoveCellAtIndex(int index)
	{
		if (onDataSourceAdapterHandler == null)
			return;

		if (CellsCount == 0 ) {
			reloadData ();
			return;
		}

		resetCellsInfo ();
		updatePositions ();
		updateContentSize ();
		while (cellsUsed.Count > 0) {
			DTableViewCell cell = cellsUsed [cellsUsed.Count - 1];
			int idx = cell.idx;

			if (idx >= index) {
				indices.Remove (idx);
				cellsUsed.RemoveAt (cellsUsed.Count - 1);
				AddToFreedList (cell);
			} else {
				break;
			}
		}
		onScrolling ();
	}
}