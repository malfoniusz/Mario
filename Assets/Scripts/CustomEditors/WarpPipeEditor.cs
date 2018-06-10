#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WarpPipe))]
public class WarpPipeEditor : Editor
{
    SerializedProperty entrances;
    SerializedProperty audioPipeEnter;
    SerializedProperty enterDirection;
    SerializedProperty enterDistanceValue;
    SerializedProperty newPlayerPos;
    SerializedProperty newCameraPosOnPlayer;
    SerializedProperty newCameraPos;
    SerializedProperty exitBackground;
    SerializedProperty exitMusic;
    SerializedProperty staticCamOnExit;
    SerializedProperty exitPipeAnim;
    SerializedProperty exitDirection;

    public void OnEnable()
    {
        entrances = serializedObject.FindProperty("entrances");
        audioPipeEnter = serializedObject.FindProperty("audioPipeEnter");
        enterDirection = serializedObject.FindProperty("enterDirection");
        enterDistanceValue = serializedObject.FindProperty("enterDistanceValue");
        newPlayerPos = serializedObject.FindProperty("newPlayerPos");
        newCameraPosOnPlayer = serializedObject.FindProperty("newCameraPosOnPlayer");
        newCameraPos = serializedObject.FindProperty("newCameraPos");
        exitBackground = serializedObject.FindProperty("exitBackground");
        exitMusic = serializedObject.FindProperty("exitMusic");
        staticCamOnExit = serializedObject.FindProperty("staticCamOnExit");
        exitPipeAnim = serializedObject.FindProperty("exitPipeAnim");
        exitDirection = serializedObject.FindProperty("exitDirection");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("CUSTOM EDITOR");
        EditorGUILayout.PropertyField(entrances, true);
        EditorGUILayout.PropertyField(audioPipeEnter);
        EditorGUILayout.PropertyField(enterDirection);
        EditorGUILayout.PropertyField(enterDistanceValue);
        EditorGUILayout.PropertyField(newPlayerPos);
        EditorGUILayout.PropertyField(newCameraPosOnPlayer);
        if (newCameraPosOnPlayer.boolValue == false)
        {
            EditorGUILayout.PropertyField(newCameraPos);
        }
        EditorGUILayout.PropertyField(exitBackground);
        EditorGUILayout.PropertyField(exitMusic);
        EditorGUILayout.PropertyField(staticCamOnExit);
        EditorGUILayout.PropertyField(exitPipeAnim);
        if (exitPipeAnim.boolValue == true)
        {
            EditorGUILayout.PropertyField(exitDirection);
        }
        //GUILayout.Space(20); base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }

}

#endif
