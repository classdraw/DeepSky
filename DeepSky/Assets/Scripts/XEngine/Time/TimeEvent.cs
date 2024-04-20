using UnityEngine;
using System;


namespace XEngine.Time
{
    [Serializable]
    public class TimeEvent
    {
        private int m_Id;//唯一编号
        public int GetId(){
            return m_Id;
        }

        //时间回调方法
        private Action m_Callback;

        //当前开始时间
        private float m_StartTime;
        //当前事件延迟开始时间后执行时间
        private float m_DueTime;
        //当前存在时间 //存活时间
        private float m_LifeTime;

        //当前事件迭代次数 如果为0那么销毁自身 如果不为0 那么继续执行 每次执行--
        private int m_Iterations = 1;
        //当前事件是否暂停 暂停事件 dueTime不会增加 但是lifeTime存活时间会增加
        private bool m_IsPause = false;

        //事件执行的间隔时间
        private float m_Interval;
#region 生命周期操作
        public void Init(int id,Action cb,float time,int iterations,float interval){
            this.m_Id=id;
            this.m_Callback=cb;

            
            this.m_LifeTime = 0.0f;
            this.m_Iterations = iterations;
            this.m_Interval = interval;
            this.m_StartTime = UnityEngine.Time.time;
            this.m_DueTime = m_StartTime + time;
            if(this.m_DueTime==0){
                this.m_DueTime=1;
            }
            this.m_IsPause = false;
        }

        public void Tick(){
            if(UnityEngine.Time.time>=m_DueTime||this.m_Id==0){
                Execute();
            }else{
                if(this.IsPause()){
                    m_DueTime+=UnityEngine.Time.deltaTime;//下一次激活时间增加
                }else{
                    m_LifeTime+=UnityEngine.Time.deltaTime;//存活时间增加
                }

            }//

        }

        public void Execute(){
            if(!IsActive()){
                TimeManager.GetInstance().ReleaseOne(this);
                return;
            }

            if(this.m_Callback!=null){
                this.m_Callback();
            }

            if(this.m_Iterations!=TimeManager.REPEAT_INDEX){
                //当前执行的迭代次数--
                this.m_Iterations--;

                if(this.m_Iterations<=0){
                    TimeManager.GetInstance().ReleaseOne(this);
                    return;
                }
            }
        
            //每次执行结束 都需要把当前下一次执行时间增加当前的间隔时间
            this.m_DueTime = UnityEngine.Time.time + this.m_Interval;

        }

        public void Release(){
            this.m_Callback = null;
            this.m_DueTime = 0.0f;
            this.m_StartTime = 0.0f;
            this.m_LifeTime = 0.0f;
            this.m_Iterations = 0;
            this.m_IsPause = false;

            this.m_Interval = 0.0f;

        }


#endregion


#region 逻辑操作方法
        /// <summary>
        /// 是否存活
        /// </summary>
        /// <value></value>
        public bool IsActive(){
            if(m_Id==0||m_DueTime==0){
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是否暂停
        /// </summary>
        /// <returns></returns>
        public bool IsPause(){
            return m_IsPause;
        }

        /// <summary>
        /// 设置暂停属性
        /// </summary>
        /// <param name="flag"></param>
        public void SetPause(bool flag){
            m_IsPause=flag;
        }



#endregion
        
    }

}