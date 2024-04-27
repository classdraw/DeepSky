using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YooAsset;

namespace XEngine.Loader
{
    public class PatchOperation:GameAsyncOperation
    {
        private enum ESteps
        {
            None,
            Update,
            Done,
        }
        private string m_DefaultPackageName;
        public PatchOperation(string packageName, string buildPipeline, EPlayMode playMode)
	    {
            m_DefaultPackageName=packageName;
        }

        protected override void OnStart()
        {

        }
        protected override void OnUpdate()
        {
            Status = EOperationStatus.Succeed;
        }
        protected override void OnAbort()
        {
        }
    }
}