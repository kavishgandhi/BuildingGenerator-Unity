using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (BuildingGenerator), true)]
public class BuildingEditor : Editor
{
    public override void OnInspectorGUI(){
        BuildingGenerator buildGen = (BuildingGenerator)target;
        if (DrawDefaultInspector()){
        }
        if(GUILayout.Button ("Generate")){
            if(buildGen.buildings.Count>0){
                buildGen.destroy();
                buildGen.generateWidthHeight();
            }
            else if(buildGen.buildings.Count==0){
                buildGen.generateWidthHeight();
            }
        }
        if(GUILayout.Button ("Destroy All")){
            buildGen.destroy();
        }
    }
}
