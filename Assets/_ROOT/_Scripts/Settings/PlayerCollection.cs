using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/Collections/" + nameof(PlayerCollection)), fileName = nameof(PlayerCollection))]
	public class PlayerCollection : ScriptableObject
	{
		[field: SF]
		public PlayerSettings[] PlayerSettings { get; private set; }

		[field: SF]
		public Player MeleePlayerPrefab { get; private set; }

		[field: SF]
		public Player RangedPlayerPrefab { get; private set; }
	}
}
