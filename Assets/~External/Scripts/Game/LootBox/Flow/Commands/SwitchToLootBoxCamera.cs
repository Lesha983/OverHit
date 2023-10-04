namespace Chillplay.OverHit.LootBox
{
    using Camera;
    using UnityEngine;
    using CameraType = Camera.CameraType;

    public class SwitchToLootBoxCamera : SwitchCamera

    {
        protected override CameraType CameraType => CameraType.LootBox;
        protected override Transform Target => null;
    }
}