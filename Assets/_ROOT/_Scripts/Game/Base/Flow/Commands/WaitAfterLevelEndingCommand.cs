namespace Chillplay.OverHit.Base.Flow
{
    using System.Collections;
    using Configs;
    using Utils.Coroutine;
    using UnityEngine;
    using Zenject;
    using Zenject.Extensions.Commands;

    public abstract class WaitAfterLevelEndingCommand : LockableCommand
    {
        [Inject] 
        public ICoroutineProvider CoroutineProvider { get; set; }
        [Inject] 
        public LevelDelays LevelDelays { get; set; }
        
        protected abstract float Seconds { get; }
        
        public override void Execute()
        {
            CoroutineProvider.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            Lock();
            yield return new WaitForSeconds(Seconds);
            Unlock();
        }
    }
    
    public class WaitAfterLevelCompletingCommand : WaitAfterLevelEndingCommand
    {
        protected override float Seconds => LevelDelays.Complete;
    }
    
    public class WaitAfterLevelFailingCommand : WaitAfterLevelEndingCommand
    {
        protected override float Seconds => LevelDelays.Fail;
    }
}