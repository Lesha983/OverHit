namespace Chillplay.OverHit.Base.Flow
{
    using System.Collections;
    using Utils.Coroutine;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class WaitFrameCommand : LockableCommand
    {
        [Inject]
        public ICoroutineProvider CoroutineProvider { get; set; }
        
        public override void Execute()
        {
            CoroutineProvider.StartCoroutine(WaitFrame());
        }

        private IEnumerator WaitFrame()
        {
            Lock();
            yield return null;
            Unlock();
        }
    }
}