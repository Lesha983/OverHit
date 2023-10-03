namespace Chillplay.Input.Settings
{
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(CustomInputSettings), menuName = "Input/Settings", order = 0)]
    public class CustomInputSettings : ScriptableObject
    {
        [Header("Drag")]
        public float dragThreshold;

        [Header("Swipe")]
        public float swipeThreshold;

        public float maxSwipeTime;

        [Range(0, 90)]
        public float swipeAngle;
    }
}