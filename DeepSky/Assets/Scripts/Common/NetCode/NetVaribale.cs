using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace XEngine.Net{
    public class NetVaribale<T> : NetworkVariable<T>
    {
        public NetVaribale(T value = default(T), NetworkVariableReadPermission readPerm = NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission writePerm = NetworkVariableWritePermission.Server)
        : base(value,readPerm, writePerm)
        {

        }

        public override bool IsDirty()
        {
            //客户端没有修改网络变量的权限,所以直接过滤并且避免==null报错
            if(NetworkVariableSerialization<T>.AreEqual==null){
                return false;
            }
            return base.IsDirty();
        }
    }

}
