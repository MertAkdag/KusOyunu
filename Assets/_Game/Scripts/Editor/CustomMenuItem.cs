using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CustomMenuItem : MonoBehaviour
{
    [MenuItem("SansDev/Open Game Scene", priority = 1)]
    static void LoadGDPRScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/_Game/Scenes/Main.unity");
        }
    }

    [MenuItem("SansDev/Customize/Credit Panel")]
    static void OpenCreditData()
    {
        string path = "Assets/_Game/ScriptableObject/Credit Data.asset";
        CreditDataSO data = (CreditDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(CreditDataSO));
        Selection.activeObject = data;
    }

    [MenuItem("SansDev/Customize/Audio Data")]
    static void OpenAudioData()
    {
        string path = "Assets/_Game/ScriptableObject/Audio Data.asset";
        AudioDataSO data = (AudioDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(AudioDataSO));
        Selection.activeObject = data;
    }
}
