using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Level;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/" + nameof(LevelCollection)), fileName = nameof(LevelCollection))]
	public class LevelCollection : ScriptableObject
	{
		[field: SF]
		public LevelPrefab[] LevelPrefabs { get; private set; }
	}
}
