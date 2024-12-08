using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Common.Define{
    
    [System.Serializable]
    public enum Player_State_Enum:int{
            None=-1,
            Idle=0,
            Move=1,
    }
}
