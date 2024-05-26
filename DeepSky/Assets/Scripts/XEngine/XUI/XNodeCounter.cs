using UnityEngine;

namespace XEngine.UI
{
    public class XNodeCounter:MonoBehaviour
    {
        [ContextMenu("PrintNodeCount")]
        private void PrintNodeCount()
        {
            //XTransformUtil.AddComponent
            Transform[] transList = gameObject.GetComponentsInChildren<Transform>(true);
            XLogger.LogTest("Transform Count:" + transList.Length);

            Component[] componentList = gameObject.GetComponentsInChildren<Component>(true);
            XLogger.LogTest("Component Count:" + componentList.Length);
        }
    }
}
