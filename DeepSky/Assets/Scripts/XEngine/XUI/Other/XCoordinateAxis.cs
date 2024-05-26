using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine;

[RequireComponent(typeof(UILineRenderer))]
public class XCoordinateAxis:XBaseComponent
{
    [SerializeField]
    private UILineRenderer m_LineRenderer;

    [SerializeField]
    private XList m_PointXList;
    [SerializeField]
    private XList m_XAxisDesXList;
    [SerializeField]
    private XList m_YAxisDesXList;

    private Vector2[] m_Points;

    private void Awake()
    {
        InitComponent();
    }
    protected override void OnInitComponent()
    {
        m_LineRenderer.relativeSize = true;
    }

    public override void SetData(object _data)
    {
        InitComponent();
    }

    public void SetPointList(XUIDataList ud)
    {
        List<Vector2> list = new List<Vector2>();
        for (int i=0; i<ud.Size; i++)
        {
            list.Add(ud.GetItemAt<Vector2>(i));
        }
        Vector2[] points = list.ToArray();
        SetPointList(points);
    }
    public void SetPointList(Vector2[] points)
    {
        m_Points = points;
        m_LineRenderer.Points = m_Points;
        updatePointCircle();
    }
    private void updatePointCircle()
    {
        int pointCount = m_Points.Length;
        XUIDataList ud = new XUIDataList();
        for (int i = 0; i < pointCount; i++)
        {
            ud.Add(XUISpec.Visible);
        }
        m_PointXList.SetData(ud);
        //
        float sizeX = GetRectTransform().rect.width;
        float sizeY = GetRectTransform().rect.height;

        for (int i = 0; i < pointCount; i++)
        {
            XBaseComponent item = m_PointXList.GetItemAt(i);
            Vector2 pos = m_Points[i];

            item.GetRectTransform().anchoredPosition = new Vector2(pos.x * sizeX, pos.y * sizeY);
        }
    }
    public void SetXAxisDesData(XUIDataList ud)
    {
        m_XAxisDesXList.SetData(ud);
    }
    public void SetYAxisDesData(XUIDataList ud)
    {
        m_YAxisDesXList.SetData(ud);
    }

}
