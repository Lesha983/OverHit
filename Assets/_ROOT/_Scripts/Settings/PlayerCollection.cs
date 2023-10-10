using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/" + nameof(PlayerCollection)), fileName = nameof(PlayerCollection))]
	public class PlayerCollection : ScriptableObject
	{
		[field: SF]
		public Player[] Players { get; private set; }
	}
}
