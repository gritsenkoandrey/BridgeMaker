using UnityEditor;
using UnityEngine;

namespace Editor
{
    public struct EditorUtils
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            
            Debug.Log("Clear Prefs");
        }
    }
}