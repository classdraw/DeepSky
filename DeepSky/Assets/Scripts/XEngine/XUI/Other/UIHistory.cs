using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHistory
{
    private List<HistoryNode> m_Nodes=new List<HistoryNode>();
    private HistoryNode m_DefaultNode;
    public UIHistory(){
        m_DefaultNode=new HistoryNode();
        m_DefaultNode.m_WindowList.Add("UI_Main");
        m_DefaultNode.m_ParamList.Add(null);
    }
    public void AddNode(HistoryNode node){
        m_Nodes.Add(node);
    }

    public HistoryNode Pop(string uiName){
        if(m_Nodes.Count>0){
            int lastIndex = m_Nodes.Count - 1;
            HistoryNode node = m_Nodes[lastIndex];
            if (string.IsNullOrEmpty(uiName))
            {
                m_Nodes.RemoveAt(lastIndex);
                return node;
            }
            if (node.m_ToWindow == uiName)
            {
                node = PopLastNode();
                return node;
            }else
            {
                //Logger.LogWarn("history node not match,there must be some logic mistake:" + uiName);
            }
        }
        return null;
    }

    private HistoryNode PopLastNode()
    {
        HistoryNode node = null;
        while(node == null)
        {
            int lastIndex = m_Nodes.Count - 1;
            if (lastIndex >= 0)
            {
                node = m_Nodes[lastIndex];
                m_Nodes.RemoveAt(lastIndex);
                if (node.m_WindowList.Count == 0)
                    node = null;
            }
            else
            {
                node = this.m_DefaultNode;
            }
        }
        return node;
    }
    public void RemoveWindow(string uiName){
        if(string.IsNullOrEmpty(uiName)){
            return;
        }
        int nCount=m_Nodes.Count;
        for(int i=0;i<nCount;i++){
            var node=m_Nodes[i];
            int cCount=node.m_WindowList.Count;
            for(int j=0;j<cCount;j++){
                if(node.m_WindowList[j].Equals(uiName)){
                    node.m_WindowList.RemoveAt(j);
                    node.m_ParamList.RemoveAt(j);
                    j--;
                    cCount--;
                }
            }
            if(cCount==0){
                m_Nodes.RemoveAt(i);
                i--;
                nCount--;
            }
        }
    }

        public void Clear(){
            m_Nodes.Clear();
        }
}

//历史ui节点
public class HistoryNode{
    public string m_ToWindow;
    public List<string> m_WindowList=new List<string>();
    public List<object> m_ParamList=new List<object>();
}