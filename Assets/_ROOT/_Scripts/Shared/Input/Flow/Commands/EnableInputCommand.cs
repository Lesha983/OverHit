namespace Chillplay.Input.Flow
{
    using Zenject;
    using Zenject.Extensions.Commands;

    public class EnableInputCommand : LockableCommand
    {
        [Inject]
        public IInputProvider InputProvider { get; set; }
        
        public override void Execute()
        {
            InputProvider.Enable();
        }
    }
    
    public class DisableInputCommand : LockableCommand
    {
        [Inject]
        public IInputProvider InputProvider { get; set; }

        public override void Execute()
        {
            InputProvider.Disable();
        }
    }
}