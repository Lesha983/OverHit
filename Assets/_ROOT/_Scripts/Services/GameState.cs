using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Level;
using ChillPlay.OverHit.Settings;
using ChillPlay.OverHit.Utility;
using UnityEngine;
using Zenject;

namespace ChillPlay.OverHit.Service
{
	public class GameState
	{
		[Inject] private LevelCollection _levelCollection;
		[Inject] private PlayerCollection _playerCollection;
		[Inject] private GameSave _save;

		public LevelPrefab CurrentLevel => _levelCollection.GetLevelPrefab(_save.LevelIndex);
		public PlayerSettings CurrentPlayerSettings => _playerCollection.PlayerSettings[_save.PlayerIndex];
		public Player CurrentPlayer => CurrentPlayerSettings.Attack == AttackType.Melee
										? _playerCollection.MeleePlayerPrefab
										: _playerCollection.RangedPlayerPrefab;

		public void UpdateLevel()
		{
			_save.LevelIndex++;
		}

		public void UpdatePlayer(int index)
		{
			_save.PlayerIndex = index;
		}
	}
}
