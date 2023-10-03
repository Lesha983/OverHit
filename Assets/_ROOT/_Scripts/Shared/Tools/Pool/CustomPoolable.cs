namespace Chillplay.Tools.Pool
{
    using UnityEngine;

    public abstract class CustomPoolable<TInfo> : MonoBehaviour where TInfo : IReinitializingInfo
    {
        public abstract void Reinitialize(TInfo info);
    }
}