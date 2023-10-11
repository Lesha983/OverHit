using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Utility;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/Settings/" + nameof(PlayerSettings)), fileName = nameof(PlayerSettings))]
	public class PlayerSettings : ScriptableObject
	{
		[field: SF]
		public int Health { get; private set; }

		[field: Header("Shoot")]
		[field: SF]
		public LayerMask InteractableLayer { get; private set; }

		[field: SF]
		public AttackType Attack { get; private set; }

		[field: SF]
		public int Damage { get; private set; }

		[field: SF]
		public float RadiusMeleeZone { get; private set; }

		[field: Header("Movement")]
		[field: SF]
		public float AgentSpeed { get; private set; }

		[field: SF]
		public float MoveToAttackSpeed { get; private set; }

		[field: Header("Skin")]
		[field: SF]
		public Material Material { get; private set; }

		[field: SF]
		public Sprite Icon { get; private set; }

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
