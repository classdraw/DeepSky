using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateInfo{
    public class ClientGlobal : MonoBehaviour
    {

        private static ClientGlobal s_kClientGlobal;
        public static ClientGlobal Instance{
            get{
                return s_kClientGlobal;
            }
        }

        void Awake(){
            s_kClientGlobal=this;
        }
    }

}
