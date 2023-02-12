using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TankController;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(TankController))]
public class TankControllerEditor : Editor
{
    private TankController script;

    SerializedProperty PlayerInfo;
    SerializedProperty AIInfo;
    SerializedProperty UniversalInfo;

    private void OnEnable()
    {
        script = (TankController)target;

        PlayerInfo = serializedObject.FindProperty("playerInfo");
        AIInfo = serializedObject.FindProperty("aiInfo");
        UniversalInfo = serializedObject.FindProperty("universalInfo");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (script.gameObject.tag == "LocalPlayer")
        {
            EditorGUILayout.PropertyField(PlayerInfo, true);
        }
        else if (script.gameObject.tag == "AI")
        {
            EditorGUILayout.PropertyField(AIInfo, true);
        }

        EditorGUILayout.PropertyField(UniversalInfo, true);

        serializedObject.ApplyModifiedProperties();
    }
}
