using UnityEngine;

public struct DATA_InitLocalPlayer{
    public PlayerCtrl m_kLocalPlayer;
}
public struct DATA_ServerMovePos{
    public ulong clientId;
    public Vector2Int oldPos;
    public Vector2Int newPos;
}
public enum MessageManager_Enum:int{
    InitLocalPlayer=0,
    PlayerMovePos=1
}