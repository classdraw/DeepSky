using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class BuildCustomFilterAssemblies : IFilterBuildAssemblies
{
    public int callbackOrder => 1;
    public static bool ServerFilterEnable=false;
    public string[] OnFilterAssemblies(BuildOptions buildOptions, string[] assemblies)
    {
        if(ServerFilterEnable){
            List<string> list=new List<string>();
            for(int i=0;i<assemblies.Length;i++){
                string s=assemblies[i];
                string assName=Path.GetFileNameWithoutExtension(s);
                if(assName.Contains("UpdateInfo")){
                    Debug.Log("BuildSceneFilterAssemblies Filter "+assName);
                }else{
                    list.Add(s);
                }
            }//for
            return list.ToArray();

        }else{
            // return assemblies;
            List<string> list=new List<string>();
            for(int i=0;i<assemblies.Length;i++){
                string s=assemblies[i];
                string assName=Path.GetFileNameWithoutExtension(s);
                if(assName.Contains("Server")){
                    Debug.Log("BuildSceneFilterAssemblies Filter "+assName);
                }else{
                    list.Add(s);
                }
            }//for
            return list.ToArray();
        }
        

    }
}
