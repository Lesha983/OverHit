using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Service
{
	public class GameSave
	{
		public int LevelIndex
		{
			get => PlayerPrefs.GetInt("Level");
			set => PlayerPrefs.SetInt("Level", value);
		}

		public int PlayerIndex
		{
			get => PlayerPrefs.GetInt("Player");
			set => PlayerPrefs.SetInt("Player", value);
		}
	}
}
