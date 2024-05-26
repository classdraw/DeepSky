using UnityEngine;
using System.Collections;

public static class RectTransformExtensions
{
    public static void SetSizeWithCurrentAnchors(this RectTransform t, Vector2 size)
	{
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
	}

    public static Vector2 GetRectSize(this RectTransform t)
    {
        return t.rect.size;
    }
}
