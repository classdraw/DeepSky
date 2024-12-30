using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class BuildSceneFilterAssemblies : IFilterBuildAssemblies
{
    public int callbackOrder => 1;
    public static bool enable=false;
    public string[] OnFilterAssemblies(BuildOptions buildOptions, string[] assemblies)
    {
        return assemblies;
        // if(!enable){
        //     return assemblies;
        // }

        // List<string> list=new List<string>();
        // for(int i=0;i<assemblies.Length;i++){
        //     string s=assemblies[i];
        //     string assName=Path.GetFileNameWithoutExtension(s);
        //     if(assName.Contains("UpdateInfo")){
        //         Debug.Log("BuildSceneFilterAssemblies Filter "+assName);
        //     }else{
        //         list.Add(s);
        //     }
        // }//for
        // return list.ToArray();
    }
}
