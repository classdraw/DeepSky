using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using Common.Define;
using XEngine.Net;
using XEngine.Utilities;

namespace UpdateCommon.Role{
        //公共
        public partial class PlayerCtrl : NetworkBehaviour
        {
                //当前角色状态机状态
                private NetVaribale<Player_State_Enum> m_eCurrentState=new NetVaribale<Player_State_Enum>(Player_State_Enum.None);
                public override void OnNetworkSpawn()
                {
                        this.name="PLAYER_"+this.OwnerClientId;
                        // XLogger.LogError("OnNetworkSpawn>>>>>>>>>>"+this.OwnerClientId);
                        base.OnNetworkSpawn();
                        if(IsClient){
                #if !UNITY_SERVER || UNITY_EDITOR
                        Client_OnNetworkSpawn();
                #endif
                        }else if(IsServer){
                #if UNITY_SERVER || UNITY_EDITOR
                        Server_OnNetworkSpawn();
                #endif
                        }
                }
                

                public override void OnNetworkDespawn()
                {
                        base.OnNetworkDespawn();
                        if(IsClient){
                #if !UNITY_SERVER || UNITY_EDITOR
                        Client_OnNetworkDespawn();
                #endif
                        }else if(IsServer){
                #if UNITY_SERVER || UNITY_EDITOR
                        Server_OnNetworkDespawn();
                #endif
                        }
                }

                //呼叫服务器自身的netobject
                [ServerRpc(RequireOwnership =false)]//是否需要验证宿主
                private void SendInputMoveServerRpc(Vector3 moveDir){//结尾必须ServerRpc
                        if(IsServer){
                #if UNITY_SERVER || UNITY_EDITOR
                        this.Server_ReceiveMovement(moveDir);
                #endif
                        }
                }

                private void Update(){
                        if(IsClient){
                #if !UNITY_SERVER || UNITY_EDITOR
                        Client_Update();
                #endif
                        }else if(IsServer){
                #if UNITY_SERVER || UNITY_EDITOR
                        Server_Update();
                #endif
                        }

                }


        }



}

