using System;

namespace XEngine.UI
{

    public class XTreeProvider : XUIDataList
    {
        public XTreeProvider(bool _isTrunk, bool _selected = false, bool _expand = false) : base(null)
        {
            this.select = _selected;
            this.expand = _expand;
            this.isTrunk = _isTrunk;
        }
        public XTreeProvider() : base()
        {

        }
        private bool mSelect = false;
        private bool mExpand = false;
        public bool select
        {
            get { return mSelect; }
            set
            {
                mSelect = value;
                if (value)
                    XLogger.Log("SelectNode:" + this.ToString());
            }
        }
        public bool expand
        {
            get { return mExpand; }
            set
            {
                mExpand = value;
                if (value)
                    XLogger.Log("ExpandNode:" + this.ToString());
            }
        }

        public bool isTrunk = true;

        protected XUIDataList m_TitleData;

        public XUIDataList GetTitleData()
        {
            return m_TitleData;
        }
        public void SetTitleData(XUIDataList titleData)
        {
            m_TitleData = titleData;
        }

        protected XTreeProvider mParent;

        public void SetParent(XTreeProvider parent)
        {
            this.mParent = parent;
        }

        public XTreeProvider GetParent()
        {
            return mParent;
        }


        public bool FindNode(Func<XTreeProvider, bool> action)
        {
            if (this.Size == 0)
            {
                if (action(this))
                {
                    return true;
                }
            }
            else
            {
                for (int i = 0; i < this.Size; i++)
                {
                    XTreeProvider childProvider = (XTreeProvider)mList[i];
                    if (childProvider.FindNode(action))
                    {
                        return true;
                    }
                }
                //
            }
            return false;
        }


        public override void Add(object item)
        {
            XTreeProvider node = (XTreeProvider)item;
            node.SetParent(this);
            base.Add(item);
        }

        public override void AddAt(int index, object item)
        {
            XTreeProvider node = (XTreeProvider)item;
            node.SetParent(this);
            base.AddAt(index, item);
        }

        public override void SetItemAt(int i, object item)
        {
            XTreeProvider node = (XTreeProvider)item;
            node.SetParent(this);
            base.SetItemAt(i, item);
        }

        public void SelectItem(XTreeProvider data)
        {
            //  BetterList<int> indexList = new BetterList<int>();

            data.select = true;
            data.expand = true;

            data = data.GetParent();
            while (data != null)
            {
                data.expand = true;
                data = data.GetParent();
            }
        }


        public void SelectFirst()
        {
            SelectItem(0, 0, 0, 0);
        }
        public void SelectItem(params int[] itemIndex)
        {
            XTreeProvider currentMenu = this;

            XTreeProvider lastNode = null;
            for (int i = 0; i < itemIndex.Length; i++)
            {
                int index = itemIndex[i];
                currentMenu = (XTreeProvider)currentMenu.GetItemAt(index);
                if (currentMenu != null)
                {
                    lastNode = currentMenu;
                    lastNode.expand = true;
                }
                else
                {
                    break;
                }
            }
            if (lastNode != null)
                lastNode.select = true;
        }
    }
}
