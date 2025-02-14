using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdsDataSO))]
public class AdsDataCustomEditor : ExtendedCustomEditor
{
    bool setGDPR = false;

    public override void OnInspectorGUI()
    {
        AdsDataSO adsData = target as AdsDataSO;
        serializedObject.Update();

        DrawAdsSettings(adsData);

        switch (adsData.GetAdsType)
        {
            case AdsType.Admob:
                if (!setGDPR)
                {
                    setGDPR = true;
                    adsData.EnableGDPR = PlayerPrefs.GetInt("gdprEnabled", 1) == 1 ? true : false;
                }
                PlayerPrefs.SetInt("gdprEnabled", adsData.HasGDPR ? 1 : 0);
                DrawAdmob(adsData);
                break;
            case AdsType.UnityAds:
                setGDPR = false;
                adsData.EnableGDPR = false;
                DrawUnityAds();
                break;
            default:
                break;
        };

        SetActiveScene("GDPR", adsData.HasGDPR);

        GUILayout.Space(10);
        serializedObject.ApplyModifiedProperties();
        DrawWatermark();
    }

    private void DrawAdsSettings(AdsDataSO data)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawHeader("Ads Setting");

        EditorGUI.BeginDisabledGroup(data.GetAdsType == AdsType.UnityAds);
        SerializedProperty prop = serializedObject.FindProperty("_GDPR");
        EditorGUILayout.PropertyField(prop);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.HelpBox("Selected Ad Type will be used, Not both! ", MessageType.Info);
        DrawProperty("_adsType");

        EditorGUI.BeginDisabledGroup(!data.InterstitialEnabled);
        EditorGUILayout.HelpBox("'Interstitial Ad Interval' is the number of gameplay count before the next interstitial is shown", MessageType.Info);
        DrawProperty("_interstitialAdInterval");
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(!data.RewardedEnabled);
        DrawProperty("_rewardedAdFrequency");
        GUILayout.Space(5);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUI.BeginDisabledGroup(!data.InterstitialEnabled);
        DrawLabel("Minimum Delay Between Interstitial (seconds)");
        DrawProperty("_minDelayBetweenInterstitial", GUIContent.none);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.Space(5);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUI.BeginDisabledGroup(!data.RewardedEnabled);
        DrawLabel("Minimum Delay Between Rewarded (seconds)");
        DrawProperty("_minDelayBetweenRewarded", GUIContent.none);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.Space(5);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawLabel("Toggle Enable Ads");

        DrawProperty("_enableBanner");

        DrawProperty("_enableInterstitial");

        EditorGUI.BeginDisabledGroup(true);
        SerializedProperty rewardedAdProp = serializedObject.FindProperty("_enableRewarded");
        rewardedAdProp.boolValue = false;
        DrawProperty("_enableRewarded");
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
        GUILayout.Space(20);
    }

    private void SetActiveScene(string sceneName, bool sceneEnabled)
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.path.Contains(sceneName))
            {
                scene.enabled = sceneEnabled;
            }
        }
        EditorBuildSettings.scenes = scenes;
    }

    private void DrawAdmob(AdsDataSO data)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawHeader("Admob Ad Units");

        EditorGUI.BeginDisabledGroup(!data.BannerEnabled);
        DrawProperty("idBanner");
        EditorGUI.EndDisabledGroup();
        EditorGUI.BeginDisabledGroup(!data.InterstitialEnabled);
        DrawProperty("idInterstitial");
        EditorGUI.EndDisabledGroup();
        EditorGUI.BeginDisabledGroup(!data.RewardedEnabled);
        DrawProperty("idReward");
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
    }

    private void DrawUnityAds()
    {

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        {
            DrawHeader("Unity Settings");
            GUILayout.Space(5);
            DrawProperty("_androidGameId");
            DrawProperty("_iosGameId");
            DrawProperty("_testMode");
            GUILayout.Space(10);
        }
        {
            DrawLabel("Android Ad Unit ID");
            DrawProperty("_androInterstitialId");
            DrawProperty("_androRewardedId");
            DrawProperty("_androBannerId");
            GUILayout.Space(10);
        }
        {
            DrawLabel("iOS Ad Unit ID");
            DrawProperty("_iosInterstitialId");
            DrawProperty("_iosRewardedId");
            DrawProperty("_iosBannerId");
            GUILayout.Space(5);
        }
        EditorGUILayout.EndVertical();
    }
}
