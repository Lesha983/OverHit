namespace Chillplay.Tools.StateMachine.Rules
{
    using UnityEngine;

    public class DelayRule : Rule
    {
        [SerializeField]
        private float delay;

        private float timer;

        private void OnEnable()
        {
            timer = 0;
        }

        private void OnDisable()
        {
            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
        }

        public override bool IsValid()
        {
            return timer >= delay;
        }
    }
}