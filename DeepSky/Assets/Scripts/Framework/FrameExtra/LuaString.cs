using UnityEngine;

namespace XEngine
{

    public class LuaString
    {

        public LuaString(string strValue)
        {
            this.value = strValue;
        }

        public string value;

        //public static explicit operator LuaString(string obj)
        //{
        //    return new LuaString(obj);
        //}
        //public static explicit operator string(LuaString obj)
        //{
        //    return obj.value;
        //}

    }


}
