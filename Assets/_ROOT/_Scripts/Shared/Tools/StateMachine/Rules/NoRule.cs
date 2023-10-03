namespace Chillplay.Tools.StateMachine.Rules
{
    using UnityEngine;

    public class NoRule : Rule
    {
        [SerializeField]
        private Rule rule;
        public override bool IsValid()
        {
            return !rule.IsValid();
        }
    }
}