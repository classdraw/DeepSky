using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XEngine.UI;
using XEngine.Utilities;

namespace XEngine.Editor
{

    [CustomEditor(typeof(XTransformGroup))]
    public class XTransformGroupEditor : UnityEditor.Editor
    {
        private XTransformGroup mUIGroup;

        private string mChangeIndex = "0";
        //private string mChangeType = "ComponentGroup";


        private string mSearchIndex = "0";
        private string mSearchType = "test";

        private BetterList<MonoBehaviour> mUIList = new BetterList<MonoBehaviour>();

        void OnEnable()
        {
            mUIGroup = target as XTransformGroup;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ///////
            EditorGUILayout.BeginHorizontal();

            mChangeIndex = EditorGUILayout.TextField(mChangeIndex);
            //mChangeType = EditorGUILayout.TextField(mChangeType);
            if (GUILayout.Button("修改组件"))
            {
                int index = int.Parse(mChangeIndex);
                Transform component = mUIGroup.GetItemAt(index);
                Transform newComponent = component;
                if (newComponent != null)
                {
                    mUIGroup.SetItemAt(index, newComponent);
                }
                else
                {
                    XLogger.LogError("can not find component");
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            mSearchIndex = EditorGUILayout.TextField(mSearchIndex);
            mSearchType = EditorGUILayout.TextField(mSearchType);
            if (GUILayout.Button("搜索组件"))
            {
                int index = int.Parse(mSearchIndex);
                GameObject go = GameObject.Find(mSearchType);
                Transform newComponent = go != null ? go.transform : null;
                if (newComponent != null)
                {
                    mUIGroup.SetItemAt(index, newComponent);
                }
                else
                {
                    XLogger.LogError("can not find component");
                }
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("添加所有子对象"))
            {
                mUIGroup.SetSize(mUIGroup.transform.childCount);
                for (int i = 0; i < mUIGroup.transform.childCount; i++)
                {
                    Transform trans = mUIGroup.transform.GetChild(i);
                    mUIGroup.SetItemAt(i, trans);
                }
            }
        }
    }
}
