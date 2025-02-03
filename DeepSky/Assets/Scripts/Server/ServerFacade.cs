using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Event;
using XEngine.Net;
using XEngine.Utilities;
using XEngine.Pool;
using XEngine.Loader;
using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Driver;

namespace XEngine.Server{

    //BsonId是主键 无论什么变量名 在mongodb都是_id
    //BsonElement 要求一定序列化 即使private
    //BsonIgnore忽视的变量
    //[BsonDateTimeOptions(Kind =System.DateTimeKind.Local)]设置时区读取方式
    public class TestUser{
        [BsonId]
        public int m_iUseId;
        public string m_sName;

        public int m_iAge;
        [BsonDateTimeOptions(Kind =System.DateTimeKind.Local)]
        public DateTime m_iTime;
        // [BsonElement]
        // private int m_iLv;
    }
    public class ServerFacade:MonoBehaviour{
        private static ServerFacade m_kInstance;
        public static ServerFacade GetInstance(){
            return m_kInstance;
        }
        private ServerGlobal m_kServerGlobal;
        public ServerGlobal SC_ServerGlobal{get=>m_kServerGlobal;}
        private ClientsManager m_kClientManager;
        private ServerAOIManager m_kServerAOIManager;
        private DatabaseManager m_kDatabaseManager;
        private ServerGameSceneManager m_kServerGameSceneManager;
        private ResHandle m_ServerHandle;
        private ResHandle m_kServerResHandle;

        private void TestConnectDB(){
            string connectStr="mongodb://localhost:27017";
            MongoClient mongoClient=new MongoClient(connectStr);
            //没有自动创建
            IMongoDatabase mongoDatabase=mongoClient.GetDatabase("MMONetCode");
            //查找库建立集合
            IMongoCollection<TestUser> userInfo=mongoDatabase.GetCollection<TestUser>("UserInfo");

            //插入数据
            TestUser testUser=new TestUser();
            testUser.m_iAge=37;
            testUser.m_iUseId=123;
            testUser.m_sName="jiayanyin";
            testUser.m_iTime=DateTime.Now;
            userInfo.InsertOne(testUser);


            
            //查询
            // var t=userInfo.Find(Builders<TestUser>.Filter.Eq("m_iUseId",123)).FirstOrDefault();
            var t=userInfo.Find(Builders<TestUser>.Filter.And(Builders<TestUser>.Filter.Eq("m_iUseId",123),Builders<TestUser>.Filter.Eq("m_iUseId",345))).FirstOrDefault();
            if(t!=null){
                XLogger.LogError(">>>>"+t.m_sName);
            }

            List<TestUser> list=userInfo.Find(Builders<TestUser>.Filter.Empty).ToList();
            if(list!=null){
                XLogger.LogError(">>>>"+list.Count);
            }
            // userInfo.Find(Builders<TestUser>.Filter.And(Builders<TestUser>.Filter.Eq("m_iUseId",123),Builders<TestUser>.Filter.Gt("m_iAge",35))).FirstOrDefault();
            //修改
            var res=userInfo.UpdateOne(Builders<TestUser>.Filter.Eq("m_iUseId",123),Builders<TestUser>.Update.Set("m_iAge",100));
            XLogger.LogError(">>>>"+res);

            testUser.m_sName="jyy";
            //替换
            var res1=userInfo.ReplaceOne(Builders<TestUser>.Filter.Eq("m_iUseId",123),testUser);
            XLogger.LogError(">>>>"+res1);

            //删除
            // userInfo.DeleteOne(Builders<TestUser>.Filter.Eq("m_iUseId",123));
        }

        private void ClearServerCache(){
            if(m_kDatabaseManager!=null){
                m_kDatabaseManager.UnInit();
                m_kDatabaseManager=null;
            }
            if(m_kServerGameSceneManager!=null){
                m_kServerGameSceneManager.UnInit();
                m_kServerGameSceneManager=null;
            }
            if(m_kServerAOIManager!=null){
                m_kServerAOIManager.UnInit();
                m_kServerAOIManager=null;
            }

            if(m_kClientManager!=null){
                m_kClientManager.UnInit();
                m_kClientManager=null;
            }
            if(m_kServerGlobal!=null){
                m_kServerGlobal.UnInit();
                m_kServerGlobal=null;
            }
            if(m_ServerHandle!=null){
                m_ServerHandle.Dispose();
                m_ServerHandle=null;
            }
            if(m_kServerResHandle!=null){
                m_kServerResHandle.Dispose();
                m_kServerResHandle=null;
            }

        }

        private void OnServerInit(object obj){
            // TestConnectDB();
            InitServer();
            m_kDatabaseManager.Init();
            m_kServerGlobal.Init();
            m_kClientManager.Init();
            m_kServerAOIManager.Init();
            m_kServerGameSceneManager.Init();
            
            
            
            XLogger.LogServer("OnServerInit!!!");
        }

        private void OnServerEnd(object obj){
            ClearServerCache();
            if(MessageManager.HasInstance()){
                GlobalEventListener.RemoveListener(GlobalEventDefine.ServerStart,OnServerInit);
                GlobalEventListener.RemoveListener(GlobalEventDefine.ServerEnd,OnServerEnd);
            }
            XLogger.LogServer("ServerEnd!!!");
        }

        private void InitServer(){
            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("server_ServerCtrl");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
            m_kDatabaseManager=obj.GetComponent<DatabaseManager>();
            m_kServerGlobal=obj.GetComponent<ServerGlobal>();
            m_kClientManager=obj.GetComponent<ClientsManager>();
            m_kServerAOIManager=obj.GetComponent<ServerAOIManager>();
            obj.transform.parent=this.transform;

            m_kServerResHandle=GameResourceManager.Instance.LoadResourceSync("server_ServerGameScene");
            var obj1=m_kServerResHandle.GetGameObject();
            obj1.transform.parent=this.transform;
            m_kServerGameSceneManager=obj1.GetComponent<ServerGameSceneManager>();
            XLogger.LogServer("ServerInit!!!");
        }

        public ServerAOIManager GetServerAOIManager(){
            return m_kServerAOIManager;
        }

        
        private void Awake(){
            m_kInstance=this;
            GlobalEventListener.AddListenter(GlobalEventDefine.ServerStart,OnServerInit);
            GlobalEventListener.AddListenter(GlobalEventDefine.ServerEnd,OnServerEnd);
        }

    }
}
