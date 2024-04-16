using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ExcelDataReader;
using UnityEditor;
using System;

#if UNITY_EDITOR
public class TableCreator : EditorWindow
{
    private static UnityEngine.Object levelTableXlsx;
    
    string _tablePath = "Assets/RPG/Tables";
    string _savePath = "Assets/RPG/Resources/Data";

    [MenuItem("Table/Table Creator")]
    public static void OpenWindow()
    {
        TableCreator window = GetWindow<TableCreator>();
        window.titleContent = new GUIContent("Table Creator");
        window.Show();
    }

    private void OnEnable()
    {
        if (levelTableXlsx == null)
        {
            string[] sAssetGuids = AssetDatabase.FindAssets("LevelTable", new[] { _tablePath });
            string[] sAssetPathList = System.Array.ConvertAll(sAssetGuids, AssetDatabase.GUIDToAssetPath);
            foreach (string path in sAssetPathList)
            {
                if (path[^5..] == ".xlsx")
                {
                    levelTableXlsx = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
                    break;
                }
            }
        }
    }


    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        levelTableXlsx = EditorGUILayout.ObjectField(levelTableXlsx, typeof(UnityEngine.Object), false);
        try
        {
            if (GUILayout.Button("Update"))
            {
                UpdateLevelData();
            }
        }
        finally
        {
            GUILayout.EndHorizontal();
        }
    }

    void UpdateLevelData()
    {
        if (levelTableXlsx == null)
        {
            return;
        }

        string path = AssetDatabase.GetAssetPath(levelTableXlsx);
        FileStream streamer = new(path, FileMode.Open, FileAccess.Read);

        using var reader = ExcelReaderFactory.CreateReader(streamer);
        System.Data.DataTableCollection tables = reader.AsDataSet().Tables;
        System.Data.DataTable sheet = tables[0];

        LevelDataSO tableSO = CreateInstance<LevelDataSO>();

        for (int rowIndex = 1; rowIndex < sheet.Rows.Count; rowIndex++)
        {
            System.Data.DataRow row = sheet.Rows[rowIndex];

            tableSO.data.Add(UInt32.Parse(row[0].ToString()), new LevelData(
                UInt32.Parse(row[1].ToString()),
                UInt32.Parse(row[2].ToString()),
                UInt32.Parse(row[3].ToString())
                ));
        }

        AssetDatabase.CreateAsset(tableSO, $"{_savePath}/LevelData.asset");
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(tableSO));
        Debug.Log("LevelData Update!!");

        reader.Dispose();
        reader.Close();
    }
}
#endif
