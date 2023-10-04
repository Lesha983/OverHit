namespace Chillplay.OverHit.Shop.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using Chillplay.UI;
    using Factories;
    using Group;
    using UnityEngine;
    using Zenject;

    public class ShopWindow : Window
    {
        public List<ShopGroupView> GroupViews;

        [SerializeField] 
        private RectTransform viewsParent;

        [SerializeField] 
        private List<ShopGroup> groups;
        
        private ShopGroupViewsFactory groupViewsFactory;

        [Inject]
        public void Construct(ShopGroupViewsFactory groupViewsFactory)
        {
            GroupViews = new List<ShopGroupView>();
            this.groupViewsFactory = groupViewsFactory;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (var group in groups)
            {
                if (group.ShopItems.All(i => i.BoughtLimitExhausted)) 
                    continue;
                
                var groupView = groupViewsFactory.Create(group, viewsParent);
                groupView.Initialize(group);
                GroupViews.Add(groupView);
            }
        }
    }
}