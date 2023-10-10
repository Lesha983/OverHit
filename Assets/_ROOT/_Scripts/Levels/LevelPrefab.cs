using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Factory;
using ChillPlay.OverHit.Service;
using ChillPlay.OverHit.Settings;
using DG.Tweening;
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
		[SF] private Transform enemiesRoot;

		[SF] private Transform[] meleeEnemiesSpawnMarkers;
		[SF] private Transform[] rangedEnemiesSpawnMarkers;

		[SF] private Transform playerSpawnMarker;

		[SF] private NavMeshSurface meshSurface;
		[SF] private NavMeshData meshData;

		[SF] private Lift startLift;
		[SF] private Lift endLift;

		[Inject] private GameState _state;
		[Inject] private PlayerFactory _playerFactory;
		[Inject] private EnemyFactory _enemyFactory;
		[Inject] private EnemyCollection _enemyCollection;

		public Action OnPlayerWin;
		public Action OnPlayerLose;

		private Player _player;
		private List<Enemy.Enemy> _enemies = new();
		private bool _levelCompleted;
		private bool _playerIsWin;

		private void Awake()
		{
			meshSurface.navMeshData = meshData;
			SetupPawns();
			StartCoroutine(nameof(CoreRoutine));
		}

		private void OnDisable()
		{
			_player.OnDie -= PlayerIsDie;
			foreach(var enemy in _enemies)
				enemy.OnDie -= CheckEnemiesAlive;
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
			_player.transform.position = playerSpawnMarker.position;
			_player.OnDie += PlayerIsDie;

			foreach (var spawnMarker in meleeEnemiesSpawnMarkers)
				CreateEnemy(_enemyCollection.MeleeEnemy, spawnMarker.position);

			foreach (var spawnMarker in rangedEnemiesSpawnMarkers)
				CreateEnemy(_enemyCollection.RangedEnemy, spawnMarker.position);
		}

		private void CreateEnemy(Enemy.Enemy enemyPrefab, Vector3 spawnPosition)
		{
			var enemy = _enemyFactory.Create(enemyPrefab, enemiesRoot);
			enemy.transform.position = spawnPosition;
			_enemies.Add(enemy);
			enemy.OnDie += CheckEnemiesAlive;
		}

		private void CheckEnemiesAlive()
		{
			foreach(var enemy in _enemies)
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
