namespace Chillplay.OverHit.LootBox
{
    using Base.LevelLoading;
    using ChillPlay.LootBox;
    using UnityEngine;
    using Zenject;

    [CreateAssetMenu(fileName = nameof(LevelMagnifier), menuName = "LootBoxes/Magnifiers/Level", order = 0)]
    public class LevelMagnifier : Magnifier
    {
        [Inject] 
        public  ILevelProvider LevelProvider { get; set; }

        [SerializeField] 
        private int DummyValue;
        
        public override int Value()
        {
            return Application.isPlaying ? 
                LevelProvider.CurrentLevelNumber :
                DummyValue;
        }
    }
}