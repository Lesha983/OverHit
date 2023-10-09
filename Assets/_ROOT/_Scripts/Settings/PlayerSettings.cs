using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/" + nameof(PlayerSettings)), fileName = nameof(PlayerSettings))]
	public class PlayerSettings : ScriptableObject
	{
		[field: SF]
		public LayerMask Layer { get; private set; }

		[field: Header("Dash Marker")]
		[field: SF]
		public Color MarkerMoveColor { get; private set; }

		[field: SF]
		public Color MarkerAttackColor { get; private set; }

		[field: SF]
		public Sprite MarkerMoveSprite { get; private set; }

		[field: SF]
		public Sprite MarkerMeleeAttackSprite { get; private set; }

		[field: SF]
		public Sprite MarkerRangedAttackSprite { get; private set; }
	}
}
