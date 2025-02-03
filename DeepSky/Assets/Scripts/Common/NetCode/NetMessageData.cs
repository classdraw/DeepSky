using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum NetMessageType:byte{
    Test,
    C_S_Register,
    C_S_Login
}
public enum Client_State_Enum{
    Connected,
    Login,
    Game
}
public class ClientData{
    public ulong m_lClientId;
    public Client_State_Enum m_kClientState;
}
public struct C_S_Register : INetworkSerializable
{
    public AccountInfo m_kAccountInfo;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        m_kAccountInfo.NetworkSerialize(serializer);
    }
}
public struct C_S_Login : INetworkSerializable
{
    public AccountInfo m_kAccountInfo;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        m_kAccountInfo.NetworkSerialize(serializer);
    }
}

public struct AccountInfo : INetworkSerializable
{
    public string m_sName;
    public string m_sPwd;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref m_sName);
        serializer.SerializeValue(ref m_sPwd);
    }
}