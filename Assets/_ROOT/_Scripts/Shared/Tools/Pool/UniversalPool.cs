namespace Chillplay.Tools.Pool
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Zenject;

       public abstract class UniversalPool<TId, TPoolable, TInfo, TSettings> : IInitializable
        where TPoolable : CustomPoolable<TInfo>
        where TId : struct
        where TInfo : IReinitializingInfo
        where TSettings : UniversalPoolSettings<TPoolable, TId, TInfo>
    {
        private readonly Dictionary<TId, CustomMonoMemoryPool<TPoolable, TInfo>> pools =
            new Dictionary<TId, CustomMonoMemoryPool<TPoolable, TInfo>>();

        protected readonly DiContainer Container;
        protected readonly IFactory<TId, TPoolable> Factory;
        protected readonly TSettings PoolSettings;

        public UniversalPool(DiContainer container, IFactory<TId, TPoolable> factory, TSettings poolSettings)
        {
            Container = container;
            Factory = factory;
            PoolSettings = poolSettings;
        }

        public void Initialize()
        {
            RegisterAllPools();
        }

        private void RegisterAllPools()
        {
            foreach (var settings in PoolSettings.settings)
            {
                var id = settings.id;
                pools[id] = CreatePool(id);
            }
        }

        public void ClearAllExcept(IEnumerable<TId> ids)
        {
            foreach (var pair in pools)
            {
                if (!ids.Contains(pair.Key))
                {
                    pair.Value.Clear();
                }
            }
        }

        public void FillPool(TId id, int amount)
        {
            var settings = PoolSettings.GetPoolSettings(id);
            pools[id].Resize(amount * settings.InitialSize);
        }

        public virtual TPoolable Spawn(TId id, TInfo info)
        {
            var item = pools[id].Spawn();
            item.Reinitialize(info);
            return item;
        }

        public void Despawn(TId id, TPoolable gameObject)
        {
            pools[id].Despawn(gameObject);
        }

        protected abstract CustomMonoMemoryPool<TPoolable, TInfo> CreatePool(TId id);

        protected class FactoryProxy<TComponent> : IFactory<TComponent>
        {
            private readonly TId id;
            private readonly IFactory<TId, TComponent> factory;

            public FactoryProxy(TId id, IFactory<TId, TComponent> factory)
            {
                this.id = id;
                this.factory = factory;
            }

            public TComponent Create()
            {
                return factory.Create(id);
            }
        }

        protected class CustomMonoMemoryPool<TPoolable, TInfo> : MonoMemoryPool<TPoolable>
            where TPoolable : CustomPoolable<TInfo>
            where TInfo : IReinitializingInfo
        {
        }
    }
}