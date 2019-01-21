using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class StageCreation : EditorWindow {

    GUILayoutOption []buttonLayoutOptions;
    private Texture blueTexture;
    private Texture greenTexture;
    private Texture pinkTexture;
    private Texture redTexture;


    private List<GUIContent> draws; 
    private int stageWidth = 13;
    private int stageHeight = 13;

    private int[,] stage;

    private string stageName = "New Stage";
    private string stageWidthLabel = "Width";
    private string stageHeightLabel = "Height";
    private string filePath = "Assets/Stages/";

    private bool setMap;
    private bool usePreviousSetup;

    private List<TileInfo> tileInfo;


    [MenuItem("Window/Create Stage")]
    public static void ShowWindow()
    {
        GetWindow<StageCreation>("Create Stage");
    }

    private void OnEnable()
    {
        buttonLayoutOptions = new GUILayoutOption[]
        {
            GUILayout.MinWidth(21.0f),
            GUILayout.MinHeight(21.0f),
            GUILayout.MaxWidth(21.0f),
            GUILayout.MaxHeight(21.0f)

        };
        draws = new List<GUIContent>();
        stageWidthLabel = stageWidth.ToString();
        stageHeightLabel = stageHeight.ToString();
        stage = new int[stageWidth, stageHeight];

        tileInfo = new List<TileInfo>();

        blueTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/blue.png", typeof(Texture));
        greenTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/green.png", typeof(Texture));
        pinkTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/pink.png", typeof(Texture));
        redTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Textures/red.png", typeof(Texture));

        tileInfo.Add(new TileInfo(blueTexture,"normal tile"));
        tileInfo.Add(new TileInfo(greenTexture, "awesome tile"));
        tileInfo.Add(new TileInfo(pinkTexture, "amazing tile"));
        tileInfo.Add(new TileInfo(redTexture, "expendable tile"));


        try
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private void OnGUI()
    {        
        //Stage name
        EditorGUILayout.LabelField("New Stage Creator", EditorStyles.boldLabel);
        stageName = EditorGUILayout.TextField("Stage name: ", stageName);
        
        //Change width and height
        stageWidthLabel = EditorGUILayout.TextField("Width: ", stageWidthLabel);
        stageHeightLabel = EditorGUILayout.TextField("Height: ", stageHeightLabel);
        EditorGUILayout.Space();


        usePreviousSetup =  GUILayout.Toggle(usePreviousSetup, "Use previous stage setup?");
        EditorGUILayout.Space();

        //Stage matrix
        DrawInspector();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        #region Buttons
        GUIContent drawStage = new GUIContent("Draw Stage");
        if (GUILayout.Button(drawStage))
        {
            DrawStage();
        }

        GUIContent saveButton = new GUIContent("Create Stage");
        if (GUILayout.Button(saveButton))
        {
            if (string.IsNullOrEmpty(stageName))
            {
                EditorUtility.DisplayDialog("ERROR", "Put a name on the stage.", "OK");
                return;
            }

            SaveAsset();
        }
        GUIContent clearButton = new GUIContent("Clear Stage");
        if (GUILayout.Button(clearButton))
        {
            ClearWindowMatrixAndName();
        }


        #endregion
        EditorGUILayout.EndHorizontal();
        #region Tile Meaning

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Tile Meaning", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        for (int i = 0; i < tileInfo.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUIContent ct = new GUIContent(tileInfo[i].texture);
            if (GUILayout.Button(ct, GUIStyle.none, buttonLayoutOptions)){}
            EditorGUILayout.LabelField(tileInfo[i].meaning);
            EditorGUILayout.EndHorizontal();
        }

        #endregion
       
    }

    private void DrawInspector()
    {
        if (!setMap) return;

        Debug.Log("DRAW INSPECTOR : " + stageWidth +  " | " + stageHeight);

        EditorGUILayout.LabelField("Stage Map", EditorStyles.boldLabel);

        for (int i = 0; i < stageHeight; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < stageWidth; j++)
            {
                GUIContent btn;
                int blockValue = stage[i, j];
                switch (blockValue)
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

                if (GUILayout.Button(btn, GUIStyle.none, buttonLayoutOptions))
                {
                    blockValue++;
                    if (blockValue > 3)
                        blockValue = 0;

                    stage[i, j] = blockValue;
                    draws.Add(btn);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

  
    private void DrawStage()
    {
        stageWidth = Int32.Parse(stageWidthLabel);
        stageHeight = Int32.Parse(stageHeightLabel);

        stage = new int[stageWidth, stageHeight];
        setMap = true;

    }

    private void SaveAsset()
    {
        string fullPath = string.Concat(filePath, stageName, ".asset");
        ScriptableStage newStage = ScriptableObject.CreateInstance(typeof(ScriptableStage)) as ScriptableStage;

        for (int i = 0; i < stageHeight; i++)
        {
            for (int j = 0; j < stageWidth; j++)
            {
                newStage.SetMatrixValue(i, j, stage[i, j]);
            }
        }

        string[] names = AssetDatabase.FindAssets(stageName);

        if (names.Length > 0)
        {
            newStage.Name = string.Concat(stageName, names.Length + 1);
            string newPath = string.Concat(filePath, newStage.Name, ".asset");
            AssetDatabase.CreateAsset(newStage, newPath);
        }
        else
        {
            AssetDatabase.CreateAsset(newStage, fullPath);
        }

        if(!usePreviousSetup) ClearWindowMatrixAndName();
    }

    private void ClearWindowMatrixAndName()
    {
        for (var i = 0; i < stageHeight; i++)
            for (var j = 0; j < stageWidth; j++)
                stage[i, j] = 0;

        stageName = "New Stage Name";
    }
}
