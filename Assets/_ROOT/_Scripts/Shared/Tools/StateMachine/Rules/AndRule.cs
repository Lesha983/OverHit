namespace Chillplay.Tools.StateMachine.Rules
{
    using System.Linq;
    using UnityEngine;

    public class AndRule : Rule
    {
        [SerializeField]
        private Rule[] rules;


        public override bool IsValid()
        {
            return rules.All(r => r.IsValid());
        }
    }
}