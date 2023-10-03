namespace Chillplay.Tools.SimpleStateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}