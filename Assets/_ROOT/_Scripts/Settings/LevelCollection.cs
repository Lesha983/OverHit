using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Level;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/Collections/" + nameof(LevelCollection)), fileName = nameof(LevelCollection))]
	public class LevelCollection : ScriptableObject
	{
		[field: SF]
		public LevelPrefab[] LevelPrefabs { get; private set; }

		public LevelPrefab GetLevelPrefab(int index)
		{
			if (index >= LevelPrefabs.Length)
				index %= LevelPrefabs.Length;

			return LevelPrefabs[index];
		}
	}
}
