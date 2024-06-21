using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using XEngine.UI;

namespace XEngine.Editor
{
    //[CustomEditor(typeof(XRingItemGroup))]
    //public class XCircleItemGroupEditor : XUIGroupEditor
    //{
    //}

    [CustomEditor(typeof(XToggleUIGroup))]
    public class XToggleUIGroupEditor : XUIGroupEditor
    {
    }

    //using UnityEditor;
    [CustomEditor(typeof(XTreeNode))]
    public class XTreeNodeEditor : XToggleUIGroupEditor
    {
    }


    [CustomEditor(typeof(XUIGroup))]
    public class XUIGroupEditor : UnityEditor.Editor
    {
        private int mTextCount;
        private XUIGroup mUIGroup;

        private string mSearchPattern = "Btn_";

        private BetterList<MonoBehaviour> mUIList = new BetterList<MonoBehaviour>();

		protected virtual void OnEnable()
        {
            mUIGroup = target as XUIGroup;
            //
            MonoBehaviour[] mbList = mUIGroup.GetUIList();

            for (int i = 0, len = mbList != null ? mbList.Length : 0; i < len; i++)
            {
                mUIList.Add(mbList[i]);
            }
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ///////
            EditorGUILayout.BeginHorizontal();

            mSearchPattern = EditorGUILayout.TextField(mSearchPattern);
            if (GUILayout.Button("搜索"))
            {
                MonoBehaviour[] mbArr = mUIGroup.gameObject.GetComponentsInChildren<MonoBehaviour>(true);
                Dictionary<GameObject, MonoBehaviour> dict = new Dictionary<GameObject, MonoBehaviour>();
                //List<MonoBehaviour> resultList = new List<MonoBehaviour>();
                mUIList.Clear();
                for (int i = 0; i < mbArr.Length; i++)
                {
                    MonoBehaviour mb = mbArr[i];

                    if (!XUIEditorUtil.IsSupportedUI(mb)) continue;
                    if (dict.ContainsKey(mb.gameObject)) continue;
                    string mbName = mbArr[i].gameObject.name;

                    if (string.IsNullOrEmpty(mSearchPattern)
                        || Regex.IsMatch(mbName,mSearchPattern)) 
                    {
                        mUIList.Add(mb);
                        dict.Add(mb.gameObject, mb);
                    }
                }
            }
            if (GUILayout.Button("GetAll"))
            {
                MonoBehaviour[] mbArr = mUIGroup.gameObject.GetComponentsInChildren<MonoBehaviour>(true);
                Dictionary<GameObject, MonoBehaviour> dict = new Dictionary<GameObject, MonoBehaviour>();
                mUIList.Clear();
                for (int i = 0; i < mbArr.Length; i++)
                {
                    MonoBehaviour mb = mbArr[i];
                    if (mb.transform.parent != mUIGroup.transform) continue;

                    if (!XUIEditorUtil.IsSupportedUI(mb)) continue;
                    if (dict.ContainsKey(mb.gameObject)) continue;

                    mUIList.Add(mb);
                    dict.Add(mb.gameObject, mb);
                }
            }
            if (GUILayout.Button("Sort"))
            {
                for (int i = mUIList.size-1; i >=0 ; i--)
                {
                    if (mUIList[i] == null)
                    {
                        mUIList.RemoveAt(i);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            mTextCount = mUIList.size;
            mTextCount = EditorGUILayout.IntField(mTextCount);
            if (GUILayout.Button("Add")) mTextCount += 1;
            if (GUILayout.Button("Del")) mTextCount -= 1;
            mUIList.SetSize(mTextCount);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < mTextCount; i++)
            {
                EditorGUILayout.BeginHorizontal();

                MonoBehaviour go = (MonoBehaviour)EditorGUILayout.ObjectField(mUIList[i], typeof(MonoBehaviour), true);

                MonoBehaviour mb = XUIEditorUtil.GetSupportedUI(go != null ? go.gameObject : null);

                mUIList[i] = mb;

                EditorGUILayout.EndHorizontal();
            }

            mUIGroup.SetUIList(mUIList.ToArray());



            //if (EditorGUI.EndChangeCheck())
            //{
            //    SerializedObject so = new SerializedObject(mUIGroup);
            //    so.ApplyModifiedProperties();
            //}
        }
    }
}
