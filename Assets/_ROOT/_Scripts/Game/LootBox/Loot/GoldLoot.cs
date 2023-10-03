namespace Chillplay.OverHit.LootBox.Loot
{
    using ChillPlay.LootBox;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(GoldLoot), menuName = "LootBoxes/LootTypes/GoldLoot", order = 0)]
    public class GoldLoot : LootType
    {
        public override void Apply(int count)
        {
            Debug.Log($"Got {count} Gold");
        }
    }
}