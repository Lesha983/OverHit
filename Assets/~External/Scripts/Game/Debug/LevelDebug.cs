namespace Chillplay.OverHit.Debug
{
    using Base.Flow;
    using UnityEngine;
    using Zenject;

    public class LevelDebug : MonoBehaviour
    {
        [Inject] 
        public SignalBus SignalBus { get; set; }

        public void Complete()
        {
            SignalBus.AbstractFire<LevelCompleting>();
        }

        public void Fail()
        {
            SignalBus.AbstractFire<LevelFailing>();
        }
    }
}