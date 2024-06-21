using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XEngine.Utilities;
using XEngine.Pool;
using XEngine.Loader;

namespace XEngine.Server{
    [AutoCreateInstance(true)]
    public class ServerGlobal : MonoSingleton<ServerGlobal>
    {
        ResHandle m_NetResHandle;
        protected override void Init(){
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj=m_NetResHandle.GetGameObject();
        }

    }

}
