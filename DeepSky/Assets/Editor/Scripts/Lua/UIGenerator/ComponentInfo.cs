using System;
using UnityEngine;

namespace UIGenerator
{
    public struct ComponentInfo
    {
        public TypeBinder typeBinder;
        public Type type;
        public string name;
        public string langKey;
        public GameObject gameObject;
    }
}