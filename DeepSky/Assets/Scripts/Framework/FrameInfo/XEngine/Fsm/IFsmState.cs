using System;

namespace XEngine.Fsm
{
    /// <summary>
    /// 所有状态机的父类
    /// </summary>
    public interface IFsmState
    {
        void Enter(params object[]objs);//进入
        void Exit();//退出
        void Tick();//遍历
        void Release();//销毁
        void Reset();//重置
        bool CanChangeNext(int fsmEnum, params object[] objs);//是否可以切换下一个
        
    }

    public class BaseFsmState: IFsmState
    {
        protected BaseFsm m_Fsm;
        public BaseFsmState(BaseFsm fsm) {
            m_Fsm = fsm;
        }
        public virtual void Enter(params object[]objs) { }//进入
        public virtual void Exit() { }//退出
        public virtual void Tick() { }//遍历
        public virtual void Release() { }//销毁
        public virtual void Reset() { }//重置
        public virtual bool CanChangeNext(int fsmEnum, params object[] objs) { return true; }//是否可以切换下一个
    }
}