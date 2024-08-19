// using UnityEngine;
// using UnityEditor;
// using YooAsset;
// using XEngine.Utilities;

// [CustomEditor(typeof(GameConfig))]
// public class GameConfigEditor : Editor
// {
//     //target指该编辑器类绘制的目标类，需要将它强转为目标类
//     private GameConfig Target { get { return target as GameConfig; } }

//     //GUI重新绘制
//     public override void OnInspectorGUI()
//     {
//         Target.m_ePlayMode = (EPlayMode)EditorGUILayout.EnumPopup("运行模式", (EPlayMode)Target.m_ePlayMode);
//         Target.m_ePartType = (GameConsts.Game_Package_Type)EditorGUILayout.EnumPopup("包体", (GameConsts.Game_Package_Type)Target.m_ePartType);
//         Target.m_eNetModel = (GameConsts.Game_NetModel_Type)EditorGUILayout.EnumPopup("网络模式", (GameConsts.Game_NetModel_Type)Target.m_eNetModel);
//         Target.m_eDefaultBuildPipeline = (EDefaultBuildPipeline)EditorGUILayout.EnumPopup("构成管线", (EDefaultBuildPipeline)Target.m_eDefaultBuildPipeline);
//         Target.ShowLogInfo = EditorGUILayout.Toggle("显示日志", Target.ShowLogInfo);
        
//         //EditorGUILayout.LabelField("IntValue",_target.intValue.ToString(),EditorStyles.boldLabel);
//         //_target.intValue = EditorGUILayout.IntSlider(new GUIContent("Slider"),_target.intValue, 0, 10);
//         //_target.floatValue = EditorGUILayout.Slider(new GUIContent("FloatValue"), _target.floatValue, 0, 10);
//         // _target.intValue = EditorGUILayout.IntField("IntValue", _target.intValue);
//         // _target.floatValue = EditorGUILayout.FloatField("FloatValue", _target.floatValue);
//         // _target.stringValue = EditorGUILayout.TextField("StringValue", _target.stringValue);
//         // _target.boolValue = EditorGUILayout.Toggle("BoolValue", _target.boolValue);
//         // _target.vector3Value = EditorGUILayout.Vector3Field("Vector3Value", _target.vector3Value);
//         // _target.enumValue = (Course)EditorGUILayout.EnumPopup("EnumValue", (Course)_target.enumValue);
//         // _target.colorValue = EditorGUILayout.ColorField(new GUIContent("ColorValue"), _target.colorValue);
//         // _target.textureValue = (Texture)EditorGUILayout.ObjectField(
//         // "TextureValue", _target.textureValue, typeof(Texture), true);
//     }
// }
