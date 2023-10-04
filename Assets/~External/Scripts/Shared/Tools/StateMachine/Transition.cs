namespace Chillplay.Tools.StateMachine
{
    using Rules;
    using UnityEngine;

    public class Transition : MonoBehaviour
    {
        public State.State To => state;
        
        [SerializeField]
        private Rule rule;

        [SerializeField]
        private State.State state;

        public bool IsValid()
        {
            return rule.IsValid();
        }
    }
}