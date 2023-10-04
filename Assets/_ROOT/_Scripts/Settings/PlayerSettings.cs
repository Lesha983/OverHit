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
	}
}
