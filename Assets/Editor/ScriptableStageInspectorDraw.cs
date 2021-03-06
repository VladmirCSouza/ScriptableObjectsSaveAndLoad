﻿using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools.Utils;

[CustomEditor(typeof(ScriptableStage))]
public class ScriptableStageInspectorDraw : Editor {

    GUILayoutOption[] buttonLayoutOptions;
    private Texture blueTexture;
    private Texture greenTexture;
    private Texture pinkTexture;
    private Texture redTexture;

    private int stateWidth = 13;
    private int stageHeight = 13;

    private int[,] stage;

    private void OnEnable()
    {
        buttonLayoutOptions = new GUILayoutOption[]
        {
            GUILayout.MinWidth(21.0f),
            GUILayout.MinHeight(21.0f),
            GUILayout.MaxWidth(21.0f),
            GUILayout.MaxHeight(21.0f)

        };

        stage = new int[stateWidth, stageHeight];

        blueTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/blue.png", typeof(Texture));
        greenTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/green.png", typeof(Texture));
        pinkTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/pink.png", typeof(Texture));
        redTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/red.png", typeof(Texture));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawInspector();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawInspector()
    {
        SerializedProperty col = serializedObject.FindProperty("stageMatrix").FindPropertyRelative("cols");
        
        Debug.Log("col.arraySize: " + col.arraySize);
        for (int i = 0; i < col.arraySize; i++)
        {
            SerializedProperty row = col.GetArrayElementAtIndex(i).FindPropertyRelative("rows");

            Debug.Log("for row.arraySize: " + i +  " | " +  row.arraySize);

            //            if (col.arraySize != row.arraySize)
            //                col.arraySize = row.arraySize;

            //Get the selected object
            ScriptableStage obj = (ScriptableStage)target;

            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < row.arraySize; j++)
            {
                GUIContent btn;
                int value = row.GetArrayElementAtIndex(j).intValue;
                switch (value)
                {
                    case 1:
                        btn = new GUIContent(greenTexture);
                        break;
                    case 2:
                        btn = new GUIContent(pinkTexture);
                        break;
                    case 3:
                        btn = new GUIContent(redTexture);
                        break;
                    default:
                        btn = new GUIContent(blueTexture);
                        break;
                }

                if(GUILayout.Button(btn, buttonLayoutOptions))
                {
                    value++;
                    if (value > 3)
                        value = 0;
                    obj.SetMatrixValue(i, j, value);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
