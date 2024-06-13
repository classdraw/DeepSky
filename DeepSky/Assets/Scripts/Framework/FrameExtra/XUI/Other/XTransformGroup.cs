using System;
using UnityEngine;
using XEngine;

namespace XEngine.UI
{
    public class XTransformGroup : XBaseComponent
    {
        private Transform mapTrans;
        [SerializeField]
        private Transform[] pointList;

        public Transform GetItemAt(int index)
        {
            return pointList[index];
        }

        public void SetItemAt(int index, Transform c)
        {
            pointList[index] = c;
        }
        public void SetSize(int size)
        {
            pointList = new Transform[size];
        }
        public int Size
        {
            get
            {
                return pointList.Length;
            }
        }

        public override void SetData(object _data)
        {
            throw new NotImplementedException();
        }


    }
}

