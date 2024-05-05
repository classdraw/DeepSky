using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Pool
{
    public enum PoolCtrl_Type_Enum{
        None,
        DelayReleaseCtrl=0,//延迟销毁控制器
    }

    ///对象池数据类型
    public class PoolIncubator : MonoBehaviour
    {
        private Dictionary<string,PoolGrain> m_PoolsMap=new Dictionary<string, PoolGrain>();//当前对象池存放的所有data数据
        public static PoolIncubator Create(string rootName,GameObject parent){
            var go=new GameObject(rootName);
            DontDestroyOnLoad(go);
            var pObject=go.AddComponent<PoolIncubator>();
            if(parent!=null){
				go.transform.SetParent(parent.transform);
			}
            pObject.Init();
            return pObject;
        }

        private void Init(){
        

		}

        public void Tick(){
            foreach(var kvp in m_PoolsMap){
                kvp.Value.Tick();
            }
        }

        #region 获取销毁逻辑


        private PoolGrain _getOrAddGameObjectPool(string assetPath){
            if(!m_PoolsMap.ContainsKey(assetPath)){
                _addGameObjectPool(assetPath);
			}
            PoolGrain poolGrain = m_PoolsMap[assetPath];
            return poolGrain;

		}

        private void _addGameObjectPool(string assetPath){
            GameObject go = new GameObject(assetPath);
            go.transform.SetParent(this.transform);
            PoolGrain poolGrain=go.AddComponent<PoolGrain>();
            poolGrain.Init(assetPath);
            m_PoolsMap.Add(assetPath,poolGrain);
        }

        #endregion
        #region 外部调用
        public ResHandle LoadResourceSync(string resPath){
            var poolGrain=_getOrAddGameObjectPool(resPath);
            return poolGrain.LoadResourceSync();
        }

        public ResHandle LoadResourceAsync(string resPath,System.Action<ResHandle> callback){

            var poolGrain=_getOrAddGameObjectPool(resPath);
            return poolGrain.LoadResourceAsync(callback);
        }

        #endregion
    }

}