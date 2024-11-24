using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using XEngine.Utilities;

namespace XEngine.Time
{
    public class TimeManager:MonoSingleton<TimeManager>
    {
        public static int MAX_POOL_COUNT=50;//池中最大的
        public static int MAX_EVENT_COUNT=500;//最多update批次处理的时间节点数目

        public static int REPEAT_INDEX=999;//设置这个次数就是无限

        private int _index=0;

        //时间池
        private List<TimeEvent> _eventPools;

        //正在运行时间对象
        

        private List<TimeEvent> _eventActives;

        private Action _nextFrameAction;
        // private Action _frameAction;
        private List<Action> _frameActions;
        [Header("活动的数量")]
        public int _activeCount=0;
        [Header("池的数量")]
        public int _poolCount=0;

        //迭代器的第几个事件
        private int _eventIndex=0;
        //批次处理个数
        private int _eventBatchCount=0;
        
        #region 自身生命周期
        protected override void Init(){
            XLogger.Log("TimeManager初始化");
            _eventPools=new List<TimeEvent>();
            _eventActives=new List<TimeEvent>();
            _frameActions=new List<Action>();
        }
    
        public void Tick(){
            _activeCount=_eventActives.Count;
            _poolCount=_eventPools.Count;
            _eventBatchCount=0;
            //执行方法 活动的方法 的数量数量大于0  并且执行批处理次数小于最大数目
            while(_eventActives.Count>0&&_eventBatchCount<MAX_EVENT_COUNT){
                if(_eventIndex<0){
                    _eventIndex = _eventActives.Count - 1;
                }
                if(_eventIndex>=_eventActives.Count){
                    _eventIndex = _eventActives.Count - 1;
                }
                var curEvent=_eventActives[_eventIndex];
                if(curEvent!=null){
                    curEvent.Tick();
                }

                _eventIndex--;
                _eventBatchCount++;
            }


            //每帧调用方法
            if(_frameActions!=null){
                for(var i=0;i<_frameActions.Count;i++){
                    _frameActions[i]();
                }
            }

            if(_nextFrameAction!=null){
                _nextFrameAction();
                _nextFrameAction=null;
            }

        }

        protected override void Release(){
            _eventActives.Clear();
            _eventPools.Clear();
            _eventIndex=0;
            _eventBatchCount=0;
            _nextFrameAction=null;
            _frameActions.Clear();
        }

        #endregion


        #region 一个timeevent操作方法
        
        /// <summary>
        /// 获得一个timeevent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimeEvent GetTimeEvent(int id){
            if(id==0){
                return null;
            }
            for(int i=0;i<_eventActives.Count;i++){
                if(_eventActives[i].GetId()==id){
                    return _eventActives[i];
                }
            }
            return null;
        }


        /// <summary>
        /// 一个时间event 从所有容器移除
        /// </summary>
        /// <param name="te"></param>
        public void RemoveFromContainer(TimeEvent te){
            if(te!=null){
                if(_eventActives.Contains(te)){
                    _eventActives.Remove(te);
                }
                
                if(_eventPools.Contains(te)){
                    _eventPools.Remove(te);
                }
            }
            
        }

        public void RemoveFromContainerById(int id){
            var te=GetTimeEvent(id);
            RemoveFromContainer(te);
        }

        public void AddCallFrame(Action action){
            if(!_frameActions.Contains(action)){
                _frameActions.Add(action);
            }
        }

        public void RemoveCallFrame(Action action){
            if(!_frameActions.Contains(action)){
                _frameActions.Add(action);
            }
        }

        public void CallFrameLater(Action action){
            _nextFrameAction=action;
        }

        /// <summary>
        /// 构建一个简单的回调方法
        /// </summary>
        /// <param name="time">等待时间</param>
        /// <param name="cb">回调方法</param>
        /// <returns></returns>
        public int Build(float time,Action cb,bool isNew=false){
            return this.Schedule(time,cb,1,0,isNew);
        }

        /// <summary>
        /// 构建一个带执行次数以及间隔的回调方法
        /// </summary>
        /// <param name="time">延迟时间</param>
        /// <param name="cb">回调方法</param>
        /// <param name="iterations">执行次数最少执行一次 填0也会执行一次</param>
        /// <param name="interval">间隔</param>
        /// <returns></returns>
        public int Build(float time,Action cb,int iterations,float interval=1,bool isNew=false){
            return this.Schedule(time,cb,iterations,interval,isNew);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="time"></param>
        /// <param name="cb0"></param>
        /// <param name="cb1"></param>
        /// <param name="cb2"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="iterations"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        private int Schedule(float time,Action cb,int iterations,float interval,bool isNew=false){
            if(cb==null){//没有回调没有意义
                //报错
                return 0;
            }

            if(interval==0&&iterations==TimeManager.REPEAT_INDEX){
                return 0;
            }

            TimeEvent timeEvent=null;
            if(isNew){
                timeEvent=new TimeEvent();
            }else{
                if(_eventPools.Count>0){
                    timeEvent=_eventPools[0];
                    _eventPools.Remove(timeEvent);
                }else{
                    timeEvent=new TimeEvent();
                }//else
            }


            _index++;
            timeEvent.Init(_index,cb,time,iterations,interval);
            if(_eventActives.Contains(timeEvent)){
                //报错
            }
            _eventActives.Add(timeEvent);
            return _index;
        }
        /// <summary>
        /// 销毁一个时间event
        /// </summary>
        /// <param name="te"></param>
        public void ReleaseOneById(int id){
            if(id==0){
                return;
            }
            var te=GetTimeEvent(id);
            ReleaseOne(te);
            
        }

        public void ReleaseOne(TimeEvent te){
            if(te!=null){
                te.Release();
                if(_eventActives.Contains(te)){
                    _eventActives.Remove(te);
                }
                if(_eventPools.Count<=TimeManager.MAX_POOL_COUNT){
                    if(!_eventPools.Contains(te)){
                        _eventPools.Add(te);
                    }
                }
                
            }
        }

        public void SetPause(int id,bool isPause){
            var te=GetTimeEvent(id);
            if(te!=null){
                te.SetPause(isPause);
            }
        }

        public bool IsActive(int id){
            var te=GetTimeEvent(id);
            if(te!=null){
                return te.IsActive();
            }
            return false;
        }

        public bool IsPause(int id){
            var te=GetTimeEvent(id);
            if(te!=null){
                return te.IsPause();
            }
            return true;
        }


        public void Clear(){
            _eventActives.Clear();
            _eventPools.Clear();
            _eventIndex=0;
            _eventBatchCount=0;
        }

        #endregion
    }
}