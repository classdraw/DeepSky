using System;
using Common.Define;
using Unity.Netcode;

namespace XEngine.Net{
    public static class NetworkVaribaleSerialization 
    {
        public static void Init(){
            BindUserNetworkVariableSerialization<Player_State_Enum>();
            
            // new Unity.Netcode.NetworkVariable<int>();

        }

        public static void BindUserNetworkVariableSerialization<T>()where T:unmanaged,Enum{
            UserNetworkVariableSerialization<T>.WriteValue=(FastBufferWriter writer,in T value)=>{
                writer.WriteValueSafe(value);

            };

            UserNetworkVariableSerialization<T>.ReadValue=(FastBufferReader reader,out T value)=>{
                reader.ReadValueSafe(out value);
            };

            UserNetworkVariableSerialization<T>.DuplicateValue=(in T value,ref T dupValue)=>{
                dupValue=value;
            };
        }
    }

}
