namespace Chillplay.OverHit.Base.LevelLoading
{
    using Saves;

    [SaveComponent("LevelSave")]
    public class LevelSave : ISaveComponent
    {
        public int Level { get; set; }
    }
}

