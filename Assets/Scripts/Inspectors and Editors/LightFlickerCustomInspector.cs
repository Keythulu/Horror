using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightFlicker))]
public class LightFlickerCustomInspector : Editor
{

    LightFlicker myTarget = null;
    SerializedObject serObjVers;
    SerializedProperty colorProperty;

    //Sets up properties that requires special manipulation to display correctly
    void OnEnable()
    {
        myTarget = (LightFlicker)target;
        serObjVers = new SerializedObject(myTarget);
        //This line may cause errors if the "colors" variable in the LightFlicker script ever changes
        colorProperty = serObjVers.FindProperty("colors");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Required Flicker Components", EditorStyles.boldLabel);
        myTarget.flickerLight = (Light)EditorGUILayout.ObjectField(myTarget.flickerLight, typeof(Light), true);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Light Flicker Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Light Range Min", GUILayout.MaxWidth(160));
        myTarget.rangeMin = EditorGUILayout.Slider(myTarget.rangeMin, 0, 10);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Light Range Max", GUILayout.MaxWidth(160));
        myTarget.rangeMax = EditorGUILayout.Slider(myTarget.rangeMax, 0, 10);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Light Flicker Lerp Speed", GUILayout.MaxWidth(160));
        myTarget.lightFlickerLerpSpeed = EditorGUILayout.Slider(myTarget.lightFlickerLerpSpeed, 0, 1);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Light Flickers Before Reset", GUILayout.MaxWidth(160));
        myTarget.lightFlickersBeforeReset = EditorGUILayout.IntField(myTarget.lightFlickersBeforeReset);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Light Color Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Allow Color Interpolation", GUILayout.MaxWidth(160));
        myTarget.colorInterpolation = EditorGUILayout.Toggle(myTarget.colorInterpolation);
        EditorGUILayout.EndHorizontal();

        //Hides colors array if the user has chosen to not allow color interpolation for this light flicker object
        if (myTarget.colorInterpolation)
        {           
            EditorGUILayout.PropertyField(colorProperty, new GUIContent("Colors"), true);
            serObjVers.ApplyModifiedProperties();
        }
    }
}

