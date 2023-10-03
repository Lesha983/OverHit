namespace Chillplay.Tools.StateMachine.State
{
    public interface IState
    {
        bool CanBeExited { get; }
        void Enter();
        void Exit();
    }
}