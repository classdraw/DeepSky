using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Fsm
{
    public interface IFsm{

        /// <summary>
        /// 当前状态
        /// </summary>
        IState CurrentState();

        /// <summary>
        /// 默认状态
        /// </summary>
        IState DefaultState();
        
        /// <summary>
        /// 执行
        /// </summary>
        void Tick();

        /// <summary>
        /// 重置 切换为默认
        /// </summary>
        void Reset();

        /// <summary>
        /// 重置不切换状态
        /// </summary>
        void ResetNotChange();

        /// <summary>
        /// 尝试切换状态
        /// </summary>
        /// <param name="fsmEnum"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        bool TryChangeState(int fsmEnum,params object[] objs);

        void Release();
    }
}