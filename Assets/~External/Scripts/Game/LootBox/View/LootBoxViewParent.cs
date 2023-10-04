namespace Chillplay.OverHit.LootBox.View
{
    using ChillPlay.LootBox;
    using Zenject.Extensions.Lazy;

    public class LootBoxViewParent : MonoProvided<LootBoxViewParent>
    {
        public LootBoxView LootBoxViewInstance { get; set; }

        public void CreateLootBox(LootBoxView lootBoxView)
        {
            CleanUp();
            LootBoxViewInstance = Instantiate(lootBoxView, transform);
        }

        public void CleanUp()
        {
            if (LootBoxViewInstance != null)
            {
                Destroy(LootBoxViewInstance.gameObject);
            }
        }
    }
}