using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Pool
{
    public interface IAutoReleaseComponent
    {
        void Get();
        void Release();
        bool IsGeted();
        bool IsReleased();
    }
}
