namespace Chillplay.Tools
{
    using UnityEngine;

    public static class LayerMaskExtensions
    {
        public static int GetIndex(this LayerMask layerMask)
        {
            return Mathf.RoundToInt(Mathf.Log(layerMask.value, 2));
        }
    }
}