using UnityEngine;
using UpdateCommon.Role;
using Unity.Netcode;
public struct DATA_InitLocalPlayer{
    public PlayerCtrl m_kLocalPlayer;
}
public struct DATA_AOIUpdatePlayerPos{
    public NetworkBehaviour m_kNetObject;
    public Vector2Int m_kOldPos;
    public Vector2Int m_kNewPos;
}
public struct DATA_AOIAddPlayer{
    public NetworkBehaviour m_kNetObject;
    public Vector2Int m_kPos;
}

public struct DATA_AOIRemovePlayer{
    public NetworkBehaviour m_kNetObject;
    public Vector2Int m_kPos;
}


public enum MessageManager_Enum:int{
    InitLocalPlayer=0,
    AOIUpdatePlayerPos=1,
    AOIAddPlayer=2,
    AOIRemovePlayer=3
}