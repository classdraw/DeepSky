using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XEngine.Pool;

[CustomEditor(typeof(PoolConfig))]
public class PoolConfigEditor:Editor{
    PoolConfig pool;


    void OnEnable()
    {
        //获取当前编辑自定义Inspector的对象
        pool = (PoolConfig)target;
    }
    public override void OnInspectorGUI(){
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("对象池数据自定义");
        if (GUILayout.Button("增加"))
        {
            ConfigData configData=new ConfigData();
            configData.m_Path=Random.Range(0,9999).ToString();
            pool.m_Configs.Add(configData);
        }

        if (GUILayout.Button("移除最后"))
        {
            if(pool.m_Configs.Count>0){
                pool.m_Configs.RemoveAt(pool.m_Configs.Count-1);
            }
        }


        EditorGUILayout.LabelField("默认:");
        var config1=pool.m_DefaultConfig;
        config1.m_Path=EditorGUILayout.TextField("    路径:",config1.m_Path);
        config1.m_GameObjectLifeTime=EditorGUILayout.FloatField("    OBJ缩减时间:",config1.m_GameObjectLifeTime);
        config1.m_GrainLifeTime=EditorGUILayout.FloatField("    孵化池缩减时间:",config1.m_GrainLifeTime);
        config1.m_PoolMaxCount=EditorGUILayout.IntField("    单池上线:",config1.m_PoolMaxCount);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("    是否池保存prefab:");
        config1.m_SavePrefab=EditorGUILayout.Toggle(config1.m_SavePrefab);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("    池中active处理:");
        config1.m_IsActiveOpt=EditorGUILayout.Toggle(config1.m_IsActiveOpt);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.LabelField("内容:");
        for(int i=0;i<pool.m_Configs.Count;i++){
            var config=pool.m_Configs[i];
            EditorGUILayout.LabelField("  "+i+":");
            config.m_Path=EditorGUILayout.TextField("    路径:",config.m_Path);
            config.m_GameObjectLifeTime=EditorGUILayout.FloatField("    OBJ缩减时间:",config.m_GameObjectLifeTime);
            config.m_GrainLifeTime=EditorGUILayout.FloatField("    孵化池缩减时间:",config.m_GrainLifeTime);
            config.m_PoolMaxCount=EditorGUILayout.IntField("    单池上线:",config.m_PoolMaxCount);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("    是否池保存prefab:");
            config.m_SavePrefab=EditorGUILayout.Toggle(config.m_SavePrefab);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("    池中active处理:");
            config.m_IsActiveOpt=EditorGUILayout.Toggle(config.m_IsActiveOpt);
            EditorGUILayout.EndHorizontal();
        }//for

        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(pool);
    }


}
