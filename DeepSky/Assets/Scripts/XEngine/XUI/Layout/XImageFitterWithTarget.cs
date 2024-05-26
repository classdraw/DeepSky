using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if  UNITY_EDITOR
using UnityEditor;
#endif
public class XImageFitterWithTarget : MonoBehaviour
{
    public Image image;
    private RectTransform imageRectTrans;
    public GameObject targets;
    private RectTransform targetRectTrans;
    protected Vector2 lastTextSize;
    public Vector2 sizeOffset = new Vector2(6, 6);

    // Use this for initialization
    void Start()
    {
        if (image == null)
        {
            image = transform.GetComponent<Image>();
        }
        if (image != null)
        {
            imageRectTrans = image.GetComponent<RectTransform>();
        }
        if (targets == null)
        {
            Debug.LogError("目标为空");
            //target = transform.GetComponent<GameObject>();
        }
        if (targets != null)
        {
            targetRectTrans = targets.GetComponent<RectTransform>();

            lastTextSize = new Vector2(targetRectTrans.sizeDelta.x, targetRectTrans.sizeDelta.y);
            Refresh();
        }

        if (image == null || targets == null)
        {
            Debug.LogErrorFormat("请检查，目标Text是否为null：{0},目标Image是否为null:{1}", targets, image);
        }
    }

    Vector2 GetTargetSize()
    {
        if (targets == null) return Vector2.zero;
        RectTransform rect = targets.GetComponent<RectTransform>();
        var size = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
        return size;
    }

    void UpdateImageSize(Vector2 size, Vector2 offset)
    {
        if (imageRectTrans != null)
        {
            imageRectTrans.sizeDelta = size + offset;
        }
    }

    public void Refresh()
    {
        UpdateImageSize(GetTargetSize(), sizeOffset);
        lastTextSize = GetTargetSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (targets != null && imageRectTrans != null)
        {
            if (lastTextSize != GetTargetSize())
            {
                Refresh();
            }
        }
    }
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(XImageFitterWithTarget))]
//[CanEditMultipleObjects]
//public class XImageFitterWithTargetEditor : Editor
//{
//    public XImageFitterWithTarget obj;
//    public override void OnInspectorGUI()
//    {
//        obj = target as XImageFitterWithTarget;
//        base.OnInspectorGUI();
//        EditorGUILayout.BeginHorizontal();
//        if (GUILayout.Button("执行"))
//        {
//            obj.Refresh();
//        }
//        EditorGUILayout.EndHorizontal();
//    }
//}
//#endif