using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace XEngine.Utilities{
    public class FrameUtils
    {
        /// <summary>
        /// 打印游戏物体自界面名称开始的路径 并拷贝到粘贴板
        /// </summary>
        /// <param name="obj"></param>
        public static void PrintObjPathAndCopyToPasteBoard(GameObject obj)
        {
            StringBuilder sb = new StringBuilder();

            FindPath(sb, obj.transform);

            string text = sb.ToString();
            XLogger.LogTest(string.Format("<color=#ffa500ff>{0}</color>", text));
            //TextEditor te = new TextEditor();
            //te.text = text;
            //te.OnFocus();
            //te.Copy();
            sb.Remove(0, sb.Length);
        }

        public static void FindPath(System.Text.StringBuilder sb, Transform tr)
        {
            sb.Insert(0, tr.name); sb.Insert(0, "/");
            Transform objParant = tr.parent;
            if (objParant == null || (objParant.name.Contains("Layer_") && objParant.parent.name == "Root"))
                return;
            FindPath(sb, objParant);
        }
    }

}
