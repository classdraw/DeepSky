using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEditor.VersionControl;
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
    private Dictionary<NetMessageType,Action<ulong,INetworkSerializable>> m_kMessageActions=new Dictionary<NetMessageType, Action<ulong,INetworkSerializable>>();
    //视频是+= 不过角色还是list靠谱点
    public void Register(NetMessageType netMessageType,Action<ulong,INetworkSerializable>action){
        if(m_kMessageActions.ContainsKey(netMessageType)){
            return;
        }
        m_kMessageActions.Add(netMessageType,action);
    }
    public void UnRegister(NetMessageType netMessageType){
        if(!m_kMessageActions.ContainsKey(netMessageType)){
            return;
        }
        m_kMessageActions.Remove(netMessageType);
    }

    public void Trigger(NetMessageType netMessageType,ulong clientId,INetworkSerializable data){
        if(m_kMessageActions.ContainsKey(netMessageType)){
            m_kMessageActions[netMessageType]?.Invoke(clientId,data);
        }
    }
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

    private FastBufferWriter WriteData<T>(NetMessageType messageType,T t)where T :INetworkSerializable{
        //不足时 内部会自动拓展
        FastBufferWriter writer=new FastBufferWriter(1024,Allocator.Temp);
        using(writer){
            writer.WriteValueSafe(messageType);
            writer.WriteValueSafe(t);
        }
        return writer;
    }
    private void SendMessageToServerT<T>(NetMessageType messageType,T t)where T :INetworkSerializable{

        m_kCustomMessagingManager.SendUnnamedMessage(NetManager.ServerClientId,WriteData<T>(messageType,t));
    }

    private void SendMessageToClientT<T>(NetMessageType messageType,T t,ulong clientId)where T :INetworkSerializable{

        m_kCustomMessagingManager.SendUnnamedMessage(clientId,WriteData<T>(messageType,t));
    }

    private void SendMessageToClientListT<T>(NetMessageType messageType,T t,IReadOnlyList<ulong> clientIds)where T :INetworkSerializable{

        m_kCustomMessagingManager.SendUnnamedMessage(clientIds,WriteData<T>(messageType,t));
    }

    private void OnReceiveMessageT(ulong clientId, FastBufferReader reader)
    {
        // reader.ReadValueSafe<TestData>(out TestData td);
        
        reader.ReadValueSafe(out NetMessageType messageType);
        switch(messageType){
            case NetMessageType.Test:
                reader.ReadValueSafe(out TestData td);
                Trigger(messageType,clientId,td);
            break;
        }
        
        // reader.ReadValueSafe(out int i1);
        // reader.ReadValueSafe(out int i2);
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
