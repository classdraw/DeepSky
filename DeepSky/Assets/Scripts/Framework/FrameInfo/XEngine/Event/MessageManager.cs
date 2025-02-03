using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Utilities;

namespace XEngine.Event{
    /// <summary>
    /// 消息管理器
    /// </summary>
    public class MessageManager:MonoSingleton<MessageManager>{
        private Dictionary<int,List<System.Action<System.Object>>> _events=null;
        protected override void Init(){
            _events=new Dictionary<int, List<Action<System.Object>>>();
            XLogger.Log("MessageManager初始化");
        }

        private List<Action<System.Object>> GetListEventsByKey(int key){
            List<Action<System.Object>> list=null;
            if(_events.ContainsKey(key)){
                list=_events[key];
            }else{
                list=new List<Action<System.Object>>();
                _events.Add(key,list);
            }
            return list;
        }

        public void Tick(){

        }


        public void AddListener(int key,Action<System.Object> action){
            List<Action<System.Object>> list=GetListEventsByKey(key);
            if(!list.Contains(action)){
                list.Add(action);
            }
        }

        public void RemoveListener(int key,Action<System.Object> action){
            List<Action<System.Object>> list=GetListEventsByKey(key);
            if(list.Contains(action)){
                list.Remove(action);
            }

            if(list.Count==0){
                _events.Remove(key);
            }
        }

        public void SendMessage(int key,object obj=null){
            List<Action<System.Object>> list=GetListEventsByKey(key);
            for(int i=0;i<list.Count;i++){
                if(list[i]!=null){
                    list[i](obj);
                }
            }
        }

        
    }

}
