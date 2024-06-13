using UnityEngine;
using System.Collections.Generic;

namespace XEngine.UI
{
   public class XSubBtnParent : MonoBehaviour
    {
        public Transform subBtnParent = null;
        public Transform ThisTrans
        {
            get
            {
                return this.transform;
            }
        }

    }
}
