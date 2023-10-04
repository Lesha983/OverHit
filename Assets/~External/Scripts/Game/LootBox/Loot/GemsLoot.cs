namespace Chillplay.OverHit.LootBox.Loot
{
    using ChillPlay.LootBox;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(GemsLoot), menuName = "LootBoxes/LootTypes/GemsLoot", order = 0)]
    public class GemsLoot : LootType
    {
        public override void Apply(int count)
        {
            Debug.Log($"Got {count} Gems");
        }
    }
}