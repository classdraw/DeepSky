// Copyright (C) 2016 freeyouth
//
// Author: freeyouth <343800563@qq.com>
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
using XEngine;
using XEngine.Utilities;

namespace XEngine.UI
{
    public class XUIDataList:XDataList
    {
        //private static readonly ObjectPool<XUIDataList> s_ListPool = new ObjectPool<XUIDataList>(null, null);
        //public static XUIDataList Create(object param = null)
        //{
        //    XUIDataList uiDataList = s_ListPool.Get();
        //    uiDataList.uiParam = param;
        //    return uiDataList;
        //}
        //static public void RecycleAll(XUIDataList list)
        //{
        //    recycleInner(list);
        //}
        //private static void Recycle(XUIDataList toRelease)
        //{
        //    s_ListPool.Release(toRelease);
        //    toRelease.uiParam = null;
        //    toRelease.SetDataSource(null);
        //    toRelease.Clear();
        //}
        //static private void recycleInner(XUIDataList list)
        //{
        //    if ( !list.IsLazy)
        //    {
        //        for (int i = 0; i < list.Size; i++)
        //        {
        //            object uiDataList = list.GetItemAt(i);
        //            if (uiDataList is XUIDataList)
        //            {
        //                recycleInner((XUIDataList)uiDataList);
        //                Recycle((XUIDataList)uiDataList);
        //            }
        //        }
        //    }
        //    Recycle(list);
        //}
        //static public void TracePoolInfo()
        //{
        //    XLogger.Log("all:" + s_ListPool.countAll + " , active:" + s_ListPool.countActive);
        //}

        public XUIDataList()
        {
        }
        public XUIDataList(object _SelfData)
        {
            this.uiParam = _SelfData;
        }

        protected object mUiParam;

        public object uiParam
        {
            get { return mUiParam; }
            set { mUiParam = value; }
        }

        private bool mIsLazy = false;
        private int mLazySize = 0;
        public bool IsLazy
        {
            get { return mIsLazy; }
        }

        public void SetDataSource(Func<int, object> dataSource)
        {
            if(dataSource != null)
            {
                mIsLazy = true;
                this.mDataSource = dataSource;
            }
            else
            {
                mIsLazy = false;
                this.mDataSource = null;
            }
        }
        public void SetDataSourceSize(int size)
        {
            mLazySize = size;
        }
        public override void SetSize(int size)
        {
            if (mIsLazy)
            {
                XLogger.LogError("lazy XUIDataList cannot SetSize");
                return;
            }
            base.SetSize(size);
        }

        public override int Size
        {
            get
            {
                if (mIsLazy) return mLazySize;
                return base.Size;
            }
        }

        public override void Add(object item)
        {
            if (mIsLazy)
            {
                XLogger.LogError("lazy XUIDataList cannot Add");
                return;
            }
            base.Add(item);
        }
        public override void Add(params object[] param)
        {
            if (mIsLazy)
            {
                XLogger.LogError("lazy XUIDataList cannot Add");
                return;
            }
            base.Add(param);
        }

        private Func<int, object> mDataSource;

        public override object GetItemAt(int i)
        {
            if (mIsLazy)
            {
                return mDataSource.Invoke(i);
            }
            return base.GetItemAt(i);
        }

        public override BetterList<object> GetAllItem()
        {
            return base.GetAllItem();
        }

        public override void AddStr(string str)
        {
            this.Add(str);
        }
        public override void AddInt(int it)
        {
            this.Add(it);
        }
        public override void Clear()
        {
            mUiParam = null;
            base.Clear();
        }

    }

}