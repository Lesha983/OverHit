using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Factory;
using ChillPlay.OverHit.Service;
using ChillPlay.OverHit.Settings;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Level
{
	public class LevelPrefab : MonoBehaviour
	{
		[SF] private Transform playerRoot;
		[SF] private Transform playerSpawnMarker;

		[SF] private Enemy.Enemy[] enemies;

		[SF] private NavMeshSurface meshSurface;
		[SF] private NavMeshData meshData;

		[SF] private Lift startLift;
		[SF] private Lift endLift;

		[Inject] private GameState _state;
		[Inject] private PlayerFactory _playerFactory;
		[Inject] private EnemyCollection _enemyCollection;
		[Inject] private SlowMotionService _slowMotionService;

		public Action OnPlayerWin;
		public Action OnPlayerLose;
		public Action OnLevelCompleted;

		private Player _player;
		private bool _levelCompleted;
		private bool _playerIsWin;

		private void Awake()
		{
			meshSurface.navMeshData = meshData;
			SetupPawns();
			StartCoroutine(nameof(CoreRoutine));
		}

		private void OnEnable()
		{
			foreach (var enemy in enemies)
				enemy.OnDie += CheckEnemiesAlive;
		}

		private void OnDisable()
		{
			_player.OnDie -= PlayerIsDie;
			foreach (var enemy in enemies)
			{
				enemy.OnDie -= CheckEnemiesAlive;
				_slowMotionService.RemoveObject(enemy);
			}
		}

		private IEnumerator CoreRoutine()
		{
			yield return startLift.InteractionRoutine(_player);
			StartCoroutine(_player.CoreRoutine());
			yield return new WaitUntil(() => _levelCompleted);
			if (_playerIsWin)
			{
				yield return endLift.InteractionRoutine(_player);
				OnPlayerWin?.Invoke();
			}
			else
				OnPlayerLose?.Invoke();
		}

		private void SetupPawns()
		{
			_player = _playerFactory.Create(_state.CurrentPlayer, playerRoot);
			_player.Setup(_state.CurrentPlayerSettings);
			_player.transform.position = playerSpawnMarker.position;
			_player.OnDie += PlayerIsDie;

			foreach (var enemy in enemies)
			{
				var settings = enemy.AttackType == Utility.AttackType.Melee
								? _enemyCollection.MeleeEnemySettings
								: _enemyCollection.RangedEnemySettings;
				enemy.Setup(settings);
				_slowMotionService.AddObject(enemy);
			}
		}

		private void CheckEnemiesAlive()
		{
			foreach(var enemy in enemies)
			{
				if (enemy.IsAlive)
					return;
			}

			_playerIsWin = true;
			_levelCompleted = true;
		}

		private void PlayerIsDie()
		{
			_playerIsWin = false;
			_levelCompleted = true;
		}
	}
}
