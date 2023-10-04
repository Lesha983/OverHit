namespace Chillplay.OverHit.Sounds
{
    using Chillplay.Sounds.General;

    public class OverHitSounds : GameSounds
    {
        private OverHitSoundsSettings data;
        
        public override void Initialize()
        {
            base.Initialize();
            data = (OverHitSoundsSettings) SoundSettings;
        }
    }
}
