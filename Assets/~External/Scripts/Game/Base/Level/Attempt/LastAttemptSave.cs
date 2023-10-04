namespace Chillplay.OverHit.Base
{
    using Saves;

    [SaveComponent("LastAttemptSave")]
    public class LastAttemptSave : ISaveComponent
    {
        public int OverallStartedLevels { get; set; }
        public int Attempt { get; set; }
    }
}