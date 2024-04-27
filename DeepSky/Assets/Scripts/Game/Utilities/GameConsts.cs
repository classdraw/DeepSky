using UnityEngine;

namespace Utilities
{
    public class GameConsts
    {
        #region 通用配置
        public static bool EnableBundle = false;
        public static bool EnableCSharpHotfix=false;//是否c#热更

        public static bool UseAssetFromBundle(string assetPath){
            if(assetPath.StartsWith("_Keep/")){
                return false;
            }

            if(EnableBundle){
                //后续会加入自定义
                return true;
            }

            return EnableBundle;
        }
        #endregion
    }

}
