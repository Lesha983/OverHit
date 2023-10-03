namespace Chillplay.OverHit.Purchasing
{
    using Chillplay.Purchasing;
    using UnityEngine.Purchasing;

    public class Product : IProduct
    {
        public string Id { get; set; }
        public ProductType ProductType { get; set; }
    }
}