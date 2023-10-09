using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Factory;
using ChillPlay.OverHit.Service;
using ChillPlay.OverHit.Settings;
using UnityEngine;
using Zenject;

namespace ChillPlay.OverHit.Level
{
	public class LevelRoot : MonoBehaviour
	{
		[Inject] private LevelFactory _levelFactory;
		[Inject] private GameState _state;

		private void Awake()
		{
			_levelFactory.Create(_state.CurrentLevel, transform);
		}
	}
}
