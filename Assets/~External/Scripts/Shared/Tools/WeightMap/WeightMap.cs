namespace Chillplay.Tools.WeightMap
{
    using System.Collections.Generic;
    using ModestTree;
    using UnityEngine;

    public class WeightMap<T> where T : IWeightable
    {
        private readonly List<T> weightables;
        private readonly Vector2Int dropCount;

        public WeightMap(List<T> weightables, Vector2Int dropCount)
        {
            this.weightables = weightables;
            this.dropCount = dropCount;
        }

        public List<T> GetItem()
        {
            List<T> items = new List<T>();
            int rolls = Random.Range(dropCount.x, dropCount.y + 1);

            if (rolls >  weightables.Count)
                rolls = weightables.Count;

            for (int i = 0; i < rolls; i++)
            {
                if (weightables.IsEmpty()) break;
                var randomItem = GetRandomItem();
                weightables.Remove(randomItem);
                items.Add(randomItem);
            }
            
            return items;
        }

        private T GetRandomItem()
        {
            int totalWeight = 0;
            weightables.ForEach(w => totalWeight += w.Weight);

            int random = Random.Range(0, totalWeight) + 1;

            foreach (var weightable in weightables)
            {
                random -= weightable.Weight;
                if (random <= 0) return weightable;
            }

            return weightables[0];
        }
    }
}