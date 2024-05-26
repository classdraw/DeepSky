// Copyright (C) 2016 freeyouth
//
// Author: freeyouth <343800563@qq.com>
// Date: 2016-12-15
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Code:

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace XEngine.UI
{

    public class XTreeNode : XToggleUIGroup
    {
        protected XTreeProvider m_TreeNodeProvider;

        protected List<XTreeNode> mNodeList = new List<XTreeNode>();

        [HideInInspector]
        public XTreeNode parent;
        [HideInInspector]
        public XTree root;

        [SerializeField]
        private GameObject mExpandGO;
        [SerializeField]
        private GameObject mUnExpandGO;

        [SerializeField]
        private GameObject mExpandAnimGO;
        [SerializeField]
        private float mAnimEulerAnglesExpand;
        [SerializeField]
        private float mAnimEulerAnglesUnExpand;

        [HideInInspector]
        public int nodeLevel = 0;
        [HideInInspector]
        public float totalHeight;
        [SerializeField]
        private float mTitleHeight;

        [SerializeField]
        protected XListTween mListEffect;

        [SerializeField]
        public Color foldColor;
        [SerializeField]
        public Color unFoldColor;
        [SerializeField]
        public Text mTitleGO;//标题，用于在展开/折叠时修改文本颜色 foldColor，unFoldColor
        [SerializeField]
        public bool isSetAtBottom = false; //是否需要置于最底层

        protected Func<IXComponent, object, float> m_ContentSizeFunc;
        public void SetContentSizeFunc(Func<IXComponent, object, float> func)
        {
            m_ContentSizeFunc = func;
        }

        protected override void OnInitComponent()
        {
            base.OnInitComponent();
            isExpand = false;
        }
        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            m_ContentSizeFunc = null;
        }
        public override void SetData(object _data)
        {
            if (mRectTransform == null) InitComponent();

            m_TreeNodeProvider = (XTreeProvider)_data;

            if (m_TreeNodeProvider != null)
            {
                if (m_TreeNodeProvider.select)
                {
                    m_TreeNodeProvider.select = false;
                    if (!isTrunk)
                        root.selectNode = this;
                }

                if (m_TreeNodeProvider.expand)
                {
                    m_TreeNodeProvider.expand = false;
                    this.isExpand = true;
                }

                this.isTrunk = m_TreeNodeProvider.isTrunk;

                if (!this.isTrunk)
                {
                    if (mExpandGO != null)
                        mExpandGO.SetActive(false);
                    if (mUnExpandGO != null)
                        mUnExpandGO.SetActive(false);
                }
            }

            base.SetData(m_TreeNodeProvider.GetTitleData());
        }

        public new XTreeProvider GetData()
        {
            return m_TreeNodeProvider;
        }

        new public Vector2 Size
        {
            get { return mRectTransform.sizeDelta; }
        }

        public float TitleHeight
        {
            get { return mTitleHeight; }
        }

        public float GetPos()
        {
            return mRectTransform.anchoredPosition.y;
        }

        override protected void updateDisplay()
        {
            base.updateDisplay();
            totalHeight = 0;

            if (!isRoot)
            {
                //title size;
                totalHeight += mTitleHeight + root.cellSpace;
            }
            this.SetSelect(false);

            if (isExpand)
            {
                if (isTrunk)
                {
                    if (mExpandGO != null && !mExpandGO.activeSelf)
                        mExpandGO.SetActive(true);
                    if (mUnExpandGO != null && mUnExpandGO.activeSelf)
                        mUnExpandGO.SetActive(false);
                    if (mExpandAnimGO != null)
                        mExpandAnimGO.transform.localEulerAngles = new Vector3(0, 0, mAnimEulerAnglesExpand);
                    if (mTitleGO != null)
                        mTitleGO.color = foldColor;
                }

                for (int i = 0, len = m_TreeNodeProvider.Size; i < len; i++)
                {                 
                    XTreeProvider data = (XTreeProvider)m_TreeNodeProvider.GetItemAt(i);
                    XTreeNode treeNode = getChildNode(i);
                    //treeNode.name = "Node" + i;
                    //
                    treeNode.SetData(data);
                    Vector3 localPos = treeNode.transform.localPosition;
                    localPos.y = -totalHeight;
                    treeNode.transform.localPosition = localPos;
                    float nodeHight = treeNode.Size.y;
                    if (m_ContentSizeFunc != null)
                    {
                        var back = m_ContentSizeFunc(treeNode, data);
                        if(back > 0)
                            nodeHight = back;
                    }
                    totalHeight += nodeHight + root.cellSpace;
                }
                for (int i = m_TreeNodeProvider.Size, len = mNodeList.Count; i < len; i++)
                {
                    XTreeNode node = mNodeList[i];
                    //node.transform.localPosition = Vector3.zero;
                    node.gameObject.SetActive(false);
                }
            }
            else
            {
                if (isTrunk)
                {
                    if (mExpandGO != null && mExpandGO.activeSelf)
                        mExpandGO.SetActive(false);
                    if (mUnExpandGO != null && !mUnExpandGO.activeSelf)
                        mUnExpandGO.SetActive(true);
                    if (mExpandAnimGO != null)
                        mExpandAnimGO.transform.localEulerAngles = new Vector3(0, 0, mAnimEulerAnglesUnExpand);
                    if (mTitleGO != null)
                        mTitleGO.color = unFoldColor;
                }

                for (int i = 0, len = mNodeList.Count; i < len; i++)
                {
                    XTreeNode node = mNodeList[i];
                    //node.transform.localPosition = Vector3.zero;
                    node.gameObject.SetActive(false);
                }
            }

            resetContentSize();
        }

        private void resetContentSize()
        {
            Vector2 size = mRectTransform.sizeDelta;
            size.y = totalHeight;
            mRectTransform.sizeDelta = size;
        }

        protected XTreeNode getChildNode(int index)
        {
            XTreeNode treeNode;
            if (mNodeList.Count > index)
            {
                treeNode = mNodeList[index];
            }
            else
            {
                treeNode = root.createItem(nodeLevel + 1);
                treeNode.SetClickCallback(this.OnChildGroupClick);
                treeNode.SetClickUpCallback(this.OnChildGroupUp);
                treeNode.SetLongPressCallback(this.OnChildLongPress);
                treeNode.transform.SetParent(this.transform, false);
                if (treeNode.isSetAtBottom)
                    treeNode.transform.SetAsFirstSibling();                         
                mNodeList.Add(treeNode);
            }
            treeNode.gameObject.SetActive(true);
            treeNode.parent = this;
            treeNode.root = root;
            treeNode.nodeLevel = nodeLevel + 1;
            return treeNode;
        }

        public XTreeNode GetChildNodeByIndex(int index)
        {
            XTreeNode treeNode = null;
            if (mNodeList.Count > index)
            {
                treeNode = mNodeList[index];
            }
            return treeNode;
        }

        public virtual bool isExpand
        {
            get; set;
        }

        public virtual bool isSelected
        {
            get { return root.selectNode == this; }
        }

        public virtual bool isTrunk
        {
            get; set;
        }

        protected override void OnToggleClick(GameObject go)
        {
            OnNodeToggleClick(this);
        }

        public object GetToggleParam()
        {
            object toggleParam = m_TreeNodeProvider.GetTitleData() != null ? m_TreeNodeProvider.GetTitleData().uiParam : null;
            return toggleParam;
        }
        protected virtual void setNewSelectNode(XTreeNode newNode, XTreeNode oldNode)
        {
            XTreeNode itrNode = oldNode;
            while (itrNode != null)
            {
                itrNode.SetSelect(false);
                itrNode = itrNode.parent;
                if (itrNode == newNode.parent) break;
            }

            itrNode = newNode;
            while (itrNode != null)
            {
                itrNode.SetSelect(true);
                itrNode = itrNode.parent;
            }
            //相同的父节点展示特效
            if (newNode.parent != null
                && oldNode != null
                && oldNode.parent == newNode.parent
                && newNode.parent.mListEffect != null)
            {
                newNode.parent.mListEffect.OnSelectNewToggle(newNode, oldNode);
            }
        }
        public virtual XTreeNode getInnerFirstChildNode()
        {
            if (this.isTrunk)
            {
                if (this.isExpand)
                {
                    if (mNodeList.Count > 0)
                    {
                        XTreeNode firstNode = mNodeList[0];
                        return firstNode.getInnerFirstChildNode();
                    }
                }
                return null;
            }
            else
            {
                return this;
            }
        }
        //protected HMTreeNode getInnerFirstNode()
        //{
        //    if (mNodeList.size == 0)
        //        return this;

        //    HMTreeNode node = mNodeList[0];
        //    return node.getInnerFirstNode();
        //}
        
        [XLua.CSharpCallLua]
        public delegate bool TargetHandler(object uiParam);

        public virtual XTreeNode SetSelectNode(TargetHandler TargetHandler)
        {
            XTreeNode select = null;
            for (int i = 0; i < mNodeList.Count; i++)
            {
                XTreeNode node = mNodeList[i];
                select = node.SetSelectNode(TargetHandler);
                if (!select)
                {
                    object uiParam = node.GetData().uiParam;
                    if (TargetHandler(uiParam))
                    {
                        select = node;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return select;
        }
        
        public bool isRoot
        {
            get { return this is XTree; }
        }

        protected virtual bool OnNodeToggleClick(XTreeNode node)
        {
            return root.OnNodeToggleClick(node);
        }
        protected virtual XTreeNode createItem(int nodeLevel)
        {
            return null;
        }
        protected override void OnChildGroupClick(XClickParam clickParam)
        {
            if (root.OnClick != null)
                root.OnClick(clickParam);
        }
        protected override void OnChildGroupUp(XClickParam clickParam)
        {
            if (root.OnClickUp != null)
                root.OnClickUp(clickParam);
        }
        protected override void OnChildLongPress(XClickParam clickParam)
        {
            if (root.OnLongPressClick != null)
                root.OnLongPressClick(clickParam);
        }
        override protected void OnUIClick(GameObject go)
        {
            if (root.OnClick != null)
                root.OnClick(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
        }
        override protected void OnUILongPressClick(GameObject go)
        {
            if (root.OnLongPressClick != null)
                root.OnLongPressClick(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
        }
        override protected void OnUiClickUp(GameObject go)
        {
            if (root.OnClickUp != null)
                root.OnClickUp(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
        }


    }

}
