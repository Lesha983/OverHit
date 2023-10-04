using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/" + nameof(DashMarkerSettings)), fileName = nameof(DashMarkerSettings))]
	public class DashMarkerSettings : ScriptableObject
	{
		[field: SF]
		public Color MarkerMoveColor { get; private set; }

		[field: SF]
		public Color MarkerAttackColor { get; private set; }

		[field: SF]
		public Sprite MarkerMoveSprite { get; private set; }

		[field: SF]
		public Sprite MarkerAttackSprite { get; private set; }
	}
}
