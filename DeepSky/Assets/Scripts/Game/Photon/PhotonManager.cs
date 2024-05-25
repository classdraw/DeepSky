using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using ExitGames.Client.Photon;
using System;

namespace Game.Photon{
    public class PhotonManager:Singleton<PhotonManager>
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

        private PhotonPeer m_kPeer;//服务器连接对象
        ConnectionProtocol m_eProtoncol=ConnectionProtocol.Udp;
    
        private string m_sServerName;
		private string m_sServerAddress;
		private bool m_IsConnecting=false;
        private bool _needReConnect=false;//是否需要断线重连
		public bool NeedReConnect{get{return _needReConnect;}set{_needReConnect=value;}}
		private bool m_bLoopUpdate=false;
    }
}