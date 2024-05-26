using UnityEngine;
using System.Collections.Generic;

public class XUIPathConfig
{
    private Dictionary<string, string> m_UIPrefabDict = new Dictionary<string, string>();

    public void AddUIPrefabPath(string ui,string path)
    {
        m_UIPrefabDict.Add(ui, path);
    }

    public string GetUIPrefabPath(string ui)
    {
        return m_UIPrefabDict[ui];
    }

}