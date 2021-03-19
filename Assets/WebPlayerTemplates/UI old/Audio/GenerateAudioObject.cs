#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CoreSystem.Audio
{
    public class GenerateAudioObject : EditorWindow
    {
        string id;
        AudioClip clip;

        [MenuItem("Custom Tools/Audio Object Spawner")]
        public static void ShowWindow()
        {
            GetWindow(typeof(GenerateAudioObject));
        }

        private void OnGUI()
        {
            GUILayout.Label("Spawn New Audio Object", EditorStyles.boldLabel);

            id = EditorGUILayout.TextField("Audio ID", id);
            clip = EditorGUILayout.ObjectField("Audio Clip", clip, typeof(AudioClip), false) as AudioClip;

            if (GUILayout.Button("Generate"))
            {
                Generate();
            }
        }

        private void Generate()
        {
            AudioObject audioObject = ScriptableObject.CreateInstance("AudioObject") as AudioObject;
            audioObject.Init((AudioID)System.Enum.Parse(typeof(AudioID), id), clip);

            AssetDatabase.CreateAsset(audioObject, "Assets/Audio Objects/" + id + ".asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = audioObject;
        }
    }
}
#endif
