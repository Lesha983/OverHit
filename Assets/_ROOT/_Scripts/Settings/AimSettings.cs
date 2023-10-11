using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/Settings/" + nameof(AimSettings)), fileName = nameof(AimSettings))]
	public class AimSettings : ScriptableObject
	{
		[field: SF]
		public float ForceCoeff { get; private set; }

		[field: SF]
		public float WallOffset { get; private set; }

		[field: SF]
		public float SphereCastRadius { get; private set; }
	}
}
