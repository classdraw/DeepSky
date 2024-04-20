using System;

namespace XEngine.Fsm
{
    /// <summary>
    /// 所有状态机的父类
    /// </summary>
    public interface IState
    {
        void Enter();//进入
        void Exit();//退出
        void Tick();//遍历
        void Release();//销毁
        void Reset();//重置
        bool CanChangeNext(int fsmEnum,params object[]objs);//是否可以切换下一个
    }

}