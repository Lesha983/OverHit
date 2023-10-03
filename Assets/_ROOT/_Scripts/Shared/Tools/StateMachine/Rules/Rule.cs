namespace Chillplay.Tools.StateMachine.Rules
{
    using UnityEngine;

    public abstract class Rule : MonoBehaviour, IRule
    {
        public abstract bool IsValid();
        
        public virtual void FindReferences()
        {
        }

        private void OnValidate()
        {
            FindReferences();
        }
    }
}