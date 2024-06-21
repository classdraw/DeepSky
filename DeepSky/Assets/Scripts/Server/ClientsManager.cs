#if UNITY_SERVER || SERVER_EDITOR_TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Utilities;

public class ClientsManager : MonoSingleton<ClientsManager>
{
    public GameObject m_PlayerPrefab;
}
#endif