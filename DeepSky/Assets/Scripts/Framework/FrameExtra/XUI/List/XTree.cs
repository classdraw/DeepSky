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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace XEngine.UI
{

    public class XTree : XTreeNode
    {
        [SerializeField]
        private ScrollRect scrollRect;
        [SerializeField]
        private GameObject[] mTemplateList;
        public int cellSpace = 0;

        private int mLastTemplateIndex;
        private XTreeNode mSelectTreeNode;
                
        private Func<XToggleParam, bool> mToggleCallback;
        public void SetToggleCallback(Func<XToggleParam, bool> callback)
        {
            mToggleCallback = callback;
        }

        private bool autoPerfectPosition = false;

        protected override void OnInitComponent()
        {
            base.OnInitComponent();

            mLastTemplateIndex = mTemplateList.Length - 1;
            root = this;
            nodeLevel = -1;
            mRectTransform = GetComponent<RectTransform>();

            for (int i = 0, len = mTemplateList.Length; i < len; i++)
            {
                mTemplateList[i].SetActive(false);
            }
        }
        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            mToggleCallback = null;
            m_ContentSizeFunc = null;
        }

        public override void SetData(object _data)
        {
            selectNode = null;
            base.SetData(_data);
        }

        protected override void updateDisplay()
        {
            base.updateDisplay();

            XTreeNode newNode = selectNode;
            while (newNode != null)
            {
                newNode.SetSelect(true);
                newNode = newNode.parent;
            }
        }

        public override XTreeNode SetSelectNode(TargetHandler TargetHandler)
        {
            XTreeNode node = base.SetSelectNode(TargetHandler);
            if (!node) return node;
            setNewSelectNode(node, selectNode);
            selectNode = node;
            return null;
        }
        
        public void TryMoveUpdate(XTreeNode node)
        {
            updateDisplay();
            if (scrollRect != null && autoPerfectPosition)
            {
                RectTransform scrollTransform = (RectTransform)scrollRect.transform;
                float scrollRangeMaxY = -mRectTransform.anchoredPosition.y - scrollTransform.rect.height;

                float nodeY = GetRelativePos(node);
                float nodeMaxY = nodeY - node.Size.y;
                if (nodeMaxY < scrollRangeMaxY)
                {
                    Vector2 pos = mRectTransform.anchoredPosition;
                    pos.y = -(nodeMaxY + scrollTransform.rect.height);
                    mRectTransform.anchoredPosition = pos;
                }
            }
        }

        override protected XTreeNode createItem(int nodeLevel)
        {
            GameObject template = nodeLevel < mTemplateList.Length
                ? mTemplateList[nodeLevel]
                : mTemplateList[mLastTemplateIndex];

            GameObject item = GameObject.Instantiate(template);
            item.SetActive(true);
            XTreeNode node = item.GetComponent<XTreeNode>();
            node.InitComponent();
            node.SetContentSizeFunc(this.m_ContentSizeFunc);
            return node;
        }

        public XTreeNode selectNode
        {
            get { return mSelectTreeNode; }
            set
            {
                mSelectTreeNode = value;
            }
        }

        public object GetSelectData()
        {
            if (mSelectTreeNode != null)
            {
                XTreeProvider provider = mSelectTreeNode.GetData();
                return provider != null && provider.GetTitleData() != null ? provider.GetTitleData().uiParam : null;
            }
            return null;
        }

        public override bool isExpand { get { return true; } }

        public float GetRelativePos(XTreeNode node)
        {
            float pos = 0;
            while (node != null && !node.isRoot)
            {
                pos += node.GetPos();
                node = node.parent;
            }
            return pos;
        }

        public void MoveToItem(float pos)
        {
            Vector3 localPos = mRectTransform.anchoredPosition;
            localPos.y = pos;
            mRectTransform.anchoredPosition = localPos;
        }

        protected override bool OnNodeToggleClick(XTreeNode node)
        {
            XTreeNode newSelectNode = node;
            if (node.isTrunk)
            {
                node.isExpand = !node.isExpand;
                TryMoveUpdate(node);
                //newSelectNode = node.getInnerFirstChildNode();
            }
            if (newSelectNode == null) return true;

            if (this.selectNode == newSelectNode) return false;

            XTreeNode backup = this.selectNode;
            this.selectNode = newSelectNode;
            if (mToggleCallback != null)
            {
                object toggleParam = newSelectNode ? newSelectNode.GetToggleParam() : null;
                if (!mToggleCallback(new XToggleParam(-1, toggleParam)))
                {
                    this.selectNode = backup;
                    return false;
                }
            }
            setNewSelectNode(newSelectNode, backup);

            return true;
        }

        public void HideUnselected(){
            if(this.selectNode!=null){
                for(int i=0;i<mNodeList.Count;i++){
                    var node=mNodeList[i];
                    if(node!=this.selectNode){
                        node.isExpand=false;
                        TryMoveUpdate(node);
                    }
                }
                TryMoveUpdate(this.selectNode);
            }

        }

    }

}
