using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace XEngine.UI
{
    [RequireComponent(typeof(XRingList))]
    public class XRingListCellCountFitter : XSizeFitter
    {
        private XRingList m_XRingList;

        private int m_OriginCellItemCount;
        private int m_OriginCellItemSpace;
        private float m_OriginCellWidth;

        private float m_OriginWidth;

        public int suggestCellItemCount;

        protected override void OnInitComponent()
        {
            m_XRingList = GetComponent<XRingList>();

            m_OriginWidth = m_ThisTransform.sizeDelta.x;

            m_OriginCellWidth = m_XRingList.getTemplateScrollItem().Width;
            m_OriginCellItemCount = m_XRingList.cellItemCount;
            m_OriginCellItemSpace = m_XRingList.cellItemSpace;
        }

        protected override void OnResizeContent()
        {
            float fitSize = m_ContentMoreSize * fitRatio;

            float cellWidthWithSpace = m_OriginCellWidth + m_OriginCellItemSpace;
            int moreFitItemCount = Mathf.FloorToInt(fitSize / cellWidthWithSpace);
            suggestCellItemCount = m_OriginCellItemCount + moreFitItemCount;

            float leftSize = fitSize - moreFitItemCount * cellWidthWithSpace;
            int suggestCellItemSpace = leftSize > 0 ? m_OriginCellItemSpace + Mathf.FloorToInt(leftSize / suggestCellItemCount) : m_OriginCellItemSpace;
            if (suggestCellItemCount != m_XRingList.cellItemCount
                || suggestCellItemSpace != m_XRingList.cellItemSpace )
            {
                m_ThisTransform.sizeDelta = new Vector2(m_OriginWidth + fitSize, m_ThisTransform.sizeDelta.y);

                m_XRingList.cellItemCount = suggestCellItemCount;
                m_XRingList.cellItemSpace = suggestCellItemSpace;
                m_XRingList.ResetLayout();
            }
        }

    }
}
