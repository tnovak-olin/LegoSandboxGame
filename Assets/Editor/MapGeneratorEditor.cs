using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//declares this as a custom editor which activates when inspecting a map generator 
[CustomEditor (typeof (MapGenerator))]
public class NewBehaviourScript : Editor
{
    //called when the inspector inspects an object
    public override void OnInspectorGUI()
    {
        //gets a reference to the map generator object so the script is called when the target is the map generator object
        MapGenerator mapGen = (MapGenerator)target;

        //restores default functionality and checks if any of the variables have been manually changed
        if (DrawDefaultInspector())
        {
            //if autoupdate is turned on
            if (mapGen.autoUpdate)
            {
                //regenerate the map
                mapGen.GenerateMap();
            }
        }

        //add in a generate button at the end
        if (GUILayout.Button("Generate"))
        {
            //this code is run when the button is pressed
            mapGen.GenerateMap(); 
        }
    }
}
