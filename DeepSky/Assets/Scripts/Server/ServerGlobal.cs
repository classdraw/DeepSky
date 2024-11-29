using UnityEngine;
using XEngine.Event;
namespace XEngine.Server{
    /// <summary>
    /// 这个代码只有Server和Host模式有
    /// </summary>
    public class ServerGlobal : MonoBehaviour
    {
        private bool m_bInit=false;
        public bool IsValid(){
            return m_bInit;
        }
        public void Init(){
            m_bInit=true;
           
        }

        public void UnInit(){
            m_bInit=false;

        }

    }

}
