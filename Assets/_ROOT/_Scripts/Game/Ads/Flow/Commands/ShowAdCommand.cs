namespace Chillplay.OverHit.Ads
{
    using System.Threading.Tasks;
    using Base.LevelLoading;
    using Chillplay.Ads;
    using Zenject;
    using Zenject.Extensions.Commands;

    public abstract class ShowAdCommand : Command
    {
        [Inject] 
        public IAdvertiser Advertiser { get; set; }
        
        [Inject] 
        public ILevelProvider LevelProvider { get; set; }
        
        protected abstract string Placement { get; }
        
        public override async Task Execute()
        {
            if (Advertiser.HasInterstitialAndNotCapped(Placement, LevelProvider.CurrentLevelNumber))
            {
                await Advertiser.ShowInterstitialAd(Placement);
            }
        }
    }
    
    public class ShowLevelFailedAdCommand : ShowAdCommand
    {
        protected override string Placement => "LevelFailed";
    }
    
    public class ShowLevelCompletedAdCommand : ShowAdCommand
    {
        protected override string Placement => "LevelCompleted";
    }
}