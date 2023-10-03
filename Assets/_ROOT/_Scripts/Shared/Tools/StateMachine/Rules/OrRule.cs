namespace Chillplay.Tools.StateMachine.Rules
{
    using System.Linq;
    using UnityEngine;

    public class OrRule : Rule
    {
        [SerializeField]
        private Rule[] rules;
        public override bool IsValid()
        {
            return rules.Any(r => r.IsValid());
        }
    }
}