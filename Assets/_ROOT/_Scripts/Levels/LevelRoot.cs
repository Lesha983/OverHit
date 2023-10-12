using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Factory;
using ChillPlay.OverHit.Service;
using UnityEngine;
using Zenject;

namespace ChillPlay.OverHit.Level
{
	public class LevelRoot : MonoBehaviour
	{
		[Inject] private LevelFactory _levelFactory;
		[Inject] private GameState _state;

		public LevelPrefab CurrentLevel { get; private set; }

		public Action<LevelPrefab> OnLevelCreated;

		private void Awake()
		{
			CurrentLevel = _levelFactory.Create(_state.CurrentLevel, transform);
			OnLevelCreated?.Invoke(CurrentLevel);
		}
	}
}
