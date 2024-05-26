using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using XLua;
using DG.Tweening;
using System.Collections;

namespace XEngine.UI
{
    public class XMoreHierarchyButton: XBaseComponent
    {
        List<BtnInfo> allBtn = new List<BtnInfo>();
        class BtnInfo
        {
            public Transform trans = null;
            public Transform parent = null;
            public BtnInfo parentBtn = null;
            public Transform subTrans = null;

            public bool isOpen = false;
            private Image maskImage;
            private Image backImage;
            private DOTweenAnimation anim;
            private Button btn;

            public void OnInit()
            {
                btn = trans.GetComponent<Button>();
                maskImage = trans.Find("CheckMark").GetComponent<Image>();
                maskImage.gameObject.SetActive(false);
                backImage = trans.Find("BackGround").GetComponent<Image>();
                backImage.gameObject.SetActive(true);
                Transform animTran = trans.Find("Anim");
                if(animTran != null)
                {
                    anim = animTran.GetComponent<DOTweenAnimation>();
                }
            }

            public void SetOpenState()
            {
                isOpen = !isOpen;
                if (anim != null)
                {
                    if (isOpen)
                    {
                        anim.DOPlayForward();
                    }
                    else
                    {
                        anim.DOPlayBackwards();
                    }
                }
            }

            private bool _isSelect = false;
            public bool IsSelect
            {
                get
                {
                    return _isSelect;
                }
                set
                {
                    if(_isSelect != value)
                    {
                        _isSelect = value;
                        if(_isSelect)
                        {
                            maskImage.gameObject.SetActive(true);
                            backImage.gameObject.SetActive(false);
                            btn.targetGraphic = maskImage;
                        }
                        else
                        {
                            maskImage.gameObject.SetActive(false);
                            backImage.gameObject.SetActive(true);
                            btn.targetGraphic = backImage;
                        }
                    }
                }
            }
        }

        public ScrollRect scrollRect;
        public RectTransform posPivot;
        protected Action<XClickParam> OnClick;
        public override void SetClickCallback(Action<XClickParam> callback)
        {
            OnClick = callback;
        }

        void Awake()
        {
            InitComponent();
        }

        protected override void OnInitComponent()
        {
            OnSetClickEvent(transform,null);
        }

        void OnSetClickEvent(Transform trans,Transform parent)
        {
            BtnInfo parentBtn = GetBtnInfo(parent);
            for (int i = 0; i < trans.childCount; i++)
            {
                Button button = trans.GetChild(i).GetComponent<Button>();
                if (button != null)
                {
                    XUIEventListener.Get(button.gameObject).onClick = OnUIClick;
                    BtnInfo btnInfo = new BtnInfo();
                    btnInfo.trans = button.transform;
                    btnInfo.parentBtn = parentBtn;
                    btnInfo.parent = parent;
                    btnInfo.OnInit();
                    allBtn.Add(btnInfo);
                    XSubBtnParent subBtnParent = button.gameObject.GetComponent<XSubBtnParent>();
                    if (subBtnParent != null && subBtnParent.subBtnParent != null)
                    {
                        subBtnParent.subBtnParent.gameObject.SetActive(false);
                        btnInfo.subTrans = subBtnParent.subBtnParent;
                        OnSetClickEvent(subBtnParent.subBtnParent, button.transform);
                    }
                }
            }
        }

        BtnInfo GetBtnInfo(Transform trans)
        {
            for(int i = 0;i < allBtn.Count;i++)
            {
                if (trans == allBtn[i].trans)
                    return allBtn[i];
            }
            return null;
        }

        protected virtual void OnUIClick(GameObject go)
        {
            if (OnClick != null)
            {
                OnClick(new XClickParam(go));
                OnClickUpdateAllBtn(go.transform);
                StartCoroutine(OnSetPositionT(go));
            }
        }

        private RectTransform sRect = null;  
        IEnumerator OnSetPositionT(GameObject go)
        {
            yield return new WaitForSeconds(0.2f);
            OnSetPosition(go);
        }

        void OnSetPosition(GameObject go)
        {
            if (scrollRect == null || posPivot == null)return;
            if (sRect == null) sRect = transform.gameObject.GetComponent<RectTransform>();
            BtnInfo btnInfo = GetBtnInfo(go.transform);      
            if (btnInfo != null && btnInfo.isOpen)
            {
                RectTransform btnRect = go.GetComponent<RectTransform>();

                if (btnRect.position.y < posPivot.position.y)
                {
                    float moveDis = posPivot.position.y - btnRect.position.y;
                    if (sRect.sizeDelta.y > 0)
                        scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition -  moveDis / sRect.sizeDelta.y;
                }
            }
        }

        void OnClickUpdateAllBtn(Transform trans)
        {
            BtnInfo btnInfo = GetBtnInfo(trans);

            if(btnInfo != null)
            {
                if(btnInfo.subTrans!= null)
                    btnInfo.SetOpenState();
 
                for (int i = 0; i < allBtn.Count; i++)
                {
                    allBtn[i].IsSelect = false;
                }

       
                SetDisplay(btnInfo);

                if (btnInfo.isOpen)
                {
                    if (btnInfo.subTrans != null)
                        btnInfo.subTrans.gameObject.SetActive(true);
                }
                else
                {
                    if (btnInfo.subTrans != null)
                        btnInfo.subTrans.gameObject.SetActive(false);
                }
            }
        }

        public void ResetAllBtn()
        {
            for (int i = 0; i < allBtn.Count; i++)
            {
                allBtn[i].IsSelect = false;
                if(allBtn[i].isOpen)
                {
                    if (allBtn[i].subTrans != null)
                    {
                        allBtn[i].subTrans.gameObject.SetActive(false);
                        allBtn[i].SetOpenState();
                    }
                }
            }
            if (scrollRect != null)
                scrollRect.verticalNormalizedPosition = 1;
        }

        public void OnSetStateBaseTrans(Transform trans)
        {
            OnClickUpdateAllBtn(trans);
        }

        public void OnSetStateBaseTrans(string objName)
        {
            BtnInfo btnInfo = null;
            for(int i = 0; i <allBtn.Count;i++)
            {
                if (objName == allBtn[i].trans.gameObject.name)
                    btnInfo = allBtn[i];
            }
            if(btnInfo != null)
            {
                OnSetParentState(btnInfo);
                OnClickUpdateAllBtn(btnInfo.trans);
            }
        }

        private void OnSetParentState(BtnInfo info)
        {
            if(info.parentBtn != null)
            {
                if(!info.parentBtn.isOpen)
                {
                    info.parentBtn.subTrans.gameObject.SetActive(true);
                    info.parentBtn.SetOpenState();
                }
            }
        }

        private void SetDisplay(BtnInfo btnInfo)
        {
            btnInfo.IsSelect = true;
            if (btnInfo.parentBtn != null)
            {
                SetDisplay(btnInfo.parentBtn);
            }
        }

        public void SetButtonActive(string nameStr,bool show)
        {
            for (int i = 0; i < allBtn.Count; i++)
            {
                BtnInfo btn = allBtn[i];
                if (btn.trans != null && string.Equals(btn.trans.gameObject.name,nameStr))
                {
                    btn.trans.gameObject.SetActive(show);
                }
            }
        }

        public override void SetData(object _data)
        {
            //donothing
        }
        public override object GetData()
        {
            return null;
        }
    }
}
