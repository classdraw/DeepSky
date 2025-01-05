using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using XEngine.Net;

public class TestData:INetworkSerializable{
    public string m_sName;
    public int m_iLv;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref m_sName);
        serializer.SerializeValue(ref m_iLv);
    }
}
public class NetMessageManager :MonoBehaviour
{
    private CustomMessagingManager m_kCustomMessagingManager=>NetManager.GetInstance().CustomMessagingManager;
    public void Init(){
        m_kCustomMessagingManager.OnUnnamedMessage+=OnReceiveMessage;
    }

    private void OnReceiveMessage(ulong clientId, FastBufferReader reader)
    {
        reader.ReadValueSafe<TestData>(out TestData td);
        
        // reader.ReadValueSafe(out string s);
        // reader.ReadValueSafe(out int i1);
        // reader.ReadValueSafe(out int i2);
    }

    private void SendMessageToServer(){
        FastBufferWriter writer=new FastBufferWriter(1024,Allocator.Temp);
        // writer.WriteValueSafe("你好");
        // writer.WriteValue(0);
        // writer.WriteValue(0);
        writer.WriteValueSafe(new TestData(){
            m_sName="hello",
            m_iLv=123
        });
        m_kCustomMessagingManager.SendUnnamedMessage(NetManager.ServerClientId,writer);
    }

    private void SendMessageToAllClient(){
        FastBufferWriter writer=new FastBufferWriter(1024,Allocator.Temp);
        // writer.WriteValueSafe("你好");
        // writer.WriteValue(0);
        // writer.WriteValue(0);
        writer.WriteValueSafe(new TestData(){
            m_sName="hello",
            m_iLv=123
        });
        m_kCustomMessagingManager.SendUnnamedMessageToAll(writer);
    }
}
