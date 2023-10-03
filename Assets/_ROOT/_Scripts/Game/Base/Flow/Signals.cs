namespace Chillplay.OverHit.Base.Flow
{
    using LevelLoading;

    public class PrepareLevel
    {
        public Level Level;
    }

    public class LevelReadyToStart
    {
        public Level Level;
    }
    
    public class LevelStarted
    {
        
    }

    public class LevelCompleting : ILevelEnding
    {
        
    }

    public class LevelCompleted : ILevelEnded
    {
       
    }

    public class LevelFailing : ILevelEnding
    {
        
    }

    public class LevelFailed : ILevelEnded
    {
        
    }

    public class LevelRestarting : ILevelEnding
    {
        
    }

    public interface ILevelEnding
    {
    }

    public interface ILevelEnded
    {
    }

    public class SwitchingToHome
    {
    }

    public class SwitchingToGame
    {
    }

    public class SkippingHome
    {
    }

    public class SwitchToSettings
    {
        
    }

    public class MoveToNextLevel
    {
        
    }
}