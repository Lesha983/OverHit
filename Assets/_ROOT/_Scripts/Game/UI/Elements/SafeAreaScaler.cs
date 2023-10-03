namespace Chillplay.OverHit.UI
{
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaScaler : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            var safeArea = GetSafeArea();
            ScaleTo(safeArea);
        }

        private Rect GetSafeArea()
        {
            var safeArea = Screen.safeArea;
            return CropRect(safeArea);
        }

        private Rect CropRect(Rect safeArea)
        {
            var yPos = Mathf.Min(40, safeArea.yMin);
            var delta = safeArea.yMin - yPos;
            return new Rect(safeArea.xMin, yPos, safeArea.width, safeArea.height + delta);
        }

        private void ScaleTo(Rect rect)
        {
            // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
            var anchorMin = rect.position;
            var anchorMax = rect.position + rect.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}