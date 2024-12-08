using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using Common.Define;
using XEngine.Net;

namespace UpdateCommon.Role{
        //公共
        public partial class PlayerCtrl : NetworkBehaviour
        {
                //当前角色状态机状态
                private NetVaribale<Player_State_Enum> m_eCurrentState=new NetVaribale<Player_State_Enum>(Player_State_Enum.None);
                public override void OnNetworkSpawn()
                {
                        base.OnNetworkSpawn();
                        // Debug.LogError(">>>>>>>>>>>>>>>>>>>>>"+this.NetworkObjectId);
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
                [ServerRpc(RequireOwnership =false)]//是否需要验证宿主、、
                private void SendInputMoveServerRpc(Vector3 inputDir){//结尾必须ServerRpc
                #if UNITY_SERVER || UNITY_EDITOR
                        this.Server_ReceiveMovement(inputDir);
                #endif
                        // var oldIntPos=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
                        // var dir=0.02f*moveSpeed*(inputDir.normalized);
                        // transform.position=transform.position+dir;
                        // var newIntPos=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
                        // //aoi相关
                        // if(newIntPos!=oldIntPos){
                        //     MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.PlayerMovePos,new DATA_ServerMovePos(){
                        //         clientId=OwnerClientId,
                        //         oldPos=oldIntPos,
                        //         newPos=newIntPos
                        //     });
                        // }
                        
                }


        }



}

