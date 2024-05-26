using System;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.UI
{
    public class RList : XBaseToggleGroup
    {
        [SerializeField]
        private GameObject mItemTemplate;
        
        private Queue<IXScrollItem> mCacheItemQueue = new Queue<IXScrollItem>();

        protected override void OnInitComponent()
        {
            IXScrollItem item = mItemTemplate.GetComponent<IXScrollItem>();
            mCacheItemQueue.Enqueue(item);
        }

        private GameObject GetTemplate()
        {
            return mItemTemplate;
        }

        private IXScrollItem GetItem()
        {
            IXScrollItem scrollItem = null;
            RLuaComponent component = null;
            foreach (var VARIABLE in mCacheItemQueue)
            {
                scrollItem = VARIABLE;
                component = scrollItem as RLuaComponent;
                if (component != null && component.canUse) break;
            }

            if (component == null || !component.canUse)
                scrollItem = createItem();
            if (!scrollItem.GetGameObject().activeSelf)
                scrollItem.GetGameObject().SetActive(true);
            return scrollItem;
        }
        
        private IXScrollItem createItem()
        {
            GameObject go = Instantiate(GetTemplate(), transform, false);
            if (go == null) return null;
            go.SetActive(true);
            IXScrollItem item = go.GetComponent<IXScrollItem>();
            mCacheItemQueue.Enqueue(item);
            item.InitComponent();
            return item;
        }
        
        override protected void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            foreach (var item in mCacheItemQueue)
            {
                item.DestroyComponent();
            }
        }

        public override void SetData(object _data)
        {
            IXScrollItem item = GetItem();
            SetUIValueInner((MonoBehaviour) item.GetXComponent(), _data);
        }

        public override void SetSelectIndex(int index)
        {
            throw new NotImplementedException();
        }

        public override int GetSelectIndex()
        {
            throw new NotImplementedException();
        }

        public override void SetToggleCallback(Func<XToggleParam, bool> callback)
        {
            throw new NotImplementedException();
        }
    }
}