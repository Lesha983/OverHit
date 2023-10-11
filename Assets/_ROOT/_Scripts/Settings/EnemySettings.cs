using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/Settings/" + nameof(EnemySettings)), fileName = nameof(EnemySettings))]
	public class EnemySettings : ScriptableObject
	{
		[field: SF]
		public int Health { get; private set; }

		[field: Header("Shoot")]
		[field: SF]
		public float AttackRange { get; private set; }

		[field: SF]
		public float DetectRadius { get; private set; }

		[field: SF]
		public int Damage { get; private set; }

		[field: SF]
		public float ReloadTime { get; private set; }

		[field: SF]
		public float HitDuration { get; private set; }

		[field: Header("Movement")]
		[field: SF]
		public float AngularSpeed { get; private set; }

		[field: SF]
		public float AgentSpeed { get; private set; }
	}
}
