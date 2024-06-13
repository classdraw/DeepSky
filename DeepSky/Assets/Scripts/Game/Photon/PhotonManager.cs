using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using ExitGames.Client.Photon;
using System;
using Utilities;
using XEngine.Utilities;

namespace Game.Photon{
    public class PhotonManager:MonoSingleton<PhotonManager>,IPhotonPeerListener
    {
        ///服务器状态
		public enum Server_State_Enum:int{
			None=0,
			On=1,
			Off=2
		}
        private int m_iStateEnum=0;
        public int GetState(){
            return m_iStateEnum;
        }

        private Action m_funcComplete;//连接成功回调
		private Action m_funcFail;//连接失败回调

        private PhotonPeer m_kPeer;//服务器连接对象
        ConnectionProtocol m_eProtoncol=ConnectionProtocol.Tcp;
    
        private string m_sServerName;
		private string m_sServerAddress;
		private bool m_IsConnecting=false;
        private bool _needReConnect=false;//是否需要断线重连
		public bool NeedReConnect{get{return _needReConnect;}set{_needReConnect=value;}}
		private bool m_bLoopUpdate=false;

        #region 生命周期
        /// <summary>
		/// 根据gameconst初始化网络层
		/// </summary>
		protected override void Init(){
			XLogger.Log("PhotonManager 初始化");
			m_iStateEnum=(int)Server_State_Enum.None;
			
        }

        public void SetCallback(Action complete,Action fail){
			m_funcComplete=complete;
			m_funcFail=fail;
		}

        public void ClearCallback(){
			m_funcComplete=null;
			m_funcFail=null;
		}



        #endregion

        #region 链接断线等逻辑方法
        public void TryConnect(string ip,string port,string serverName){
			if(!m_IsConnecting){
				m_sServerName=serverName;
				m_sServerAddress=ip+":"+port;
			}
			_TryConnect();
		}

        
        private void _TryConnect(){
			if(!m_IsConnecting){
				m_bLoopUpdate=true;
				m_IsConnecting=true;
				TryStop();
				m_kPeer=new PhotonPeer(this,m_eProtoncol);
				m_kPeer.Connect(m_sServerAddress,m_sServerName);
				
			}
			
		}

        private void TryStop(){
			if(m_iStateEnum!=(int)Server_State_Enum.None){
				if(m_kPeer!=null){
					m_kPeer.Disconnect();
					m_kPeer=null;
				}
				m_iStateEnum=(int)Server_State_Enum.None;
			}
		}

        
        void Update(){
			if(m_bLoopUpdate){
				if(m_IsConnecting||m_iStateEnum==(int)Server_State_Enum.On){
					if(m_kPeer!=null&&
					(m_kPeer.PeerState==PeerStateValue.Connected||m_kPeer.PeerState==PeerStateValue.Connecting)&&
					!GameUtils.IS_QUIT){
						
						m_kPeer.Service();
					}
				}
			}


		}
        #endregion

        #region photon生命周期方法
        public void DebugReturn(DebugLevel level, string message){

        }
        public void OnEvent(EventData eventData){

        }
        public void OnOperationResponse(OperationResponse operationResponse){

        }
        public void OnStatusChanged(StatusCode statusCode){
            switch(statusCode){
				case StatusCode.Connect:
					//正常连接
					m_iStateEnum=1;
					XLogger.LogImport("连接成功");
				break;
				case StatusCode.Disconnect:
					//断开连接
					m_iStateEnum=2;
					XLogger.LogImport("断开连接");
				break;
				case StatusCode.TimeoutDisconnect:
					//超时
					m_iStateEnum=2;
					XLogger.LogImport("超时");
				break;
				case StatusCode.ExceptionOnReceive:
					//接受错误 服务器没有启动 直接弹窗
					m_iStateEnum=2;
				break;
				default:
					m_iStateEnum=2;
				break;
			}
        }
        #endregion
    }
}