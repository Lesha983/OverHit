namespace Chillplay.Tools.Pool
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NaughtyAttributes;
    using UnityEngine;
    using Zenject;

    public abstract class UniversalPoolSettings<TPoolable, TId, TInfo> : ScriptableObject
        where TPoolable : CustomPoolable<TInfo>
        where TId : struct
        where TInfo : IReinitializingInfo
    {

        public List<UniversalMemoryPoolSetting<TPoolable, TId, TInfo>> settings;

        [Button()]
        private void RemoveCopies()
        {
            settings = settings.DistinctBy(b => b.id).ToList();
        }

        public TPoolable GetPrefab(TId id)
        {
            return settings.First(b => b.id.Equals(id)).prefab;
        }

        public MemoryPoolSettings GetPoolSettings(TId id)
        {
            return settings.First(b => b.id.Equals(id)).memoryPoolSettings;
        }
    }

    [Serializable]
    public class UniversalMemoryPoolSetting<TPoolable, TId, TInfo> where TPoolable : CustomPoolable<TInfo>
        where TInfo : IReinitializingInfo
        where TId : struct
    {
        public TPoolable prefab;
        public TId id;
        public MemoryPoolSettings memoryPoolSettings;
    }
}