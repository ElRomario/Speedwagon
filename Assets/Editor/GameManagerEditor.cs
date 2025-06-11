using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private string[] planeOptions;

    public override void OnInspectorGUI()
    {
        
        serializedObject.Update();

        GameManager manager = (GameManager)target;

        
        string resourcePath = "Assets/Resources/Planes/";
        if (Directory.Exists(resourcePath))
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { resourcePath });
            planeOptions = new string[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                planeOptions[i] = System.IO.Path.GetFileNameWithoutExtension(path);
            }
        }

        
        int currentIndex = Mathf.Max(0, System.Array.IndexOf(planeOptions, manager.SelectedPlaneName));
        int selectedIndex = EditorGUILayout.Popup("Выбери самолёт", currentIndex, planeOptions);

        if (selectedIndex >= 0 && selectedIndex < planeOptions.Length)
        {
            manager.selectedPlaneName = planeOptions[selectedIndex];
        }

        
        DrawPropertiesExcluding(serializedObject, "m_Script", "selectedPlaneName");

        
        serializedObject.ApplyModifiedProperties();
    }
}