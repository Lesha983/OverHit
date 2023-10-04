namespace Chillplay.Debug
{
    using System;
    using TMPro;
    using UnityEngine;

    public class BuildVersionDebug : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text versionText;

        private void Start()
        {
            BuildInfo info = BuildInfo.Load();
            
            string text = info.Version;
            
#if UNITY_EDITOR
            text += $"({info.AndroidBuildId})";
#elif UNITY_IOS
            text += $"({info.iOSBuildId})";
#elif UNITY_ANDROID
            text += $"({info.AndroidBuildId})";
#endif

            versionText.SetText(text);
        }
    }
}