using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace XEngine.UI
{
    public class RLuaComponent : XLuaComponent,IXScrollItem
    {
        public int scrollDataIndex { get; set; }
        public void SetPointXY(float x, float y)
        {
            throw new NotImplementedException();
        }

        public Vector2 GetPointXY()
        {
            throw new NotImplementedException();
        }

        public float Width { get; private set; }
        public float Height { get; private set; }


        public bool canUse { get { return mCanUse; } }

        private bool mCanUse = true;

        protected override void OnInitComponent()
        {
            base.OnInitComponent();
            mCanUse = true;
        }

        public override void SetData(object _data)
        {
            base.SetData(_data);
            mCanUse = false;
        }

        public void OnRecycle()
        {
            transform.localPosition = new Vector3(10000, 10000, 0);
            mCanUse = true;
        }

        public void SetSelectCallback(Action<IXScrollItem> callback)
        {
            throw new NotImplementedException();
        }

        public IXComponent GetXComponent()
        {
            return this;
        }
    }
}