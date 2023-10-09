using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Level;
using ChillPlay.OverHit.Settings;
using UnityEngine;
using Zenject;

namespace ChillPlay.OverHit.Service
{
	public class GameState
	{
		[Inject] private LevelCollection _levelCollection;
		[Inject] private PlayerCollection _playerCollection;
		[Inject] private GameSave _save;

		public LevelPrefab CurrentLevel => _levelCollection.LevelPrefabs[_save.LevelIndex];
		public Player CurrentPlayer => _playerCollection.Players[_save.PlayerIndex];
	}
}
