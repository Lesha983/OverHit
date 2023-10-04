namespace Chillplay.Debug
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
#endif
    
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(BuildInfo), menuName = "Chillplay/BuildInfo", order = 0)]
    public class BuildInfo : ScriptableObject
    {
        public string Version;
        
        public string iOSBuildId;

        public string AndroidBuildId;

        private void OnValidate()
        {
            LoadInfo();
        }

        public static BuildInfo Load()
        {
            return Resources.Load<BuildInfo>("BuildInfo");
        }

        public void LoadInfo()
        {
#if UNITY_EDITOR
            Version = PlayerSettings.bundleVersion;
            iOSBuildId = PlayerSettings.iOS.buildNumber;
            AndroidBuildId = PlayerSettings.Android.bundleVersionCode.ToString();
#endif
        }
    }
    
#if UNITY_EDITOR
    public class BuildInfoPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder => default;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            BuildInfo buildInfo = BuildInfo.Load();

            if (buildInfo != null)
            {
                buildInfo.LoadInfo();
            }
        }
    }
#endif
}