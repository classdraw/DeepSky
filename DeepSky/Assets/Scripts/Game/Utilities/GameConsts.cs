using UnityEngine;
using YooAsset;

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

        #region 内置配置

        public static EPlayMode PlayMode;
        public static GameConfig.EPartType PartType;
        public static EDefaultBuildPipeline DefaultBuildPipeline;
        #endregion
    }

}
