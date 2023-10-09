using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Factory;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Level
{
	public class LevelPrefab : MonoBehaviour
	{
		[SF] private Transform playerSpawnMarker;
		[SF] private NavMeshSurface meshSurface;
		[SF] private NavMeshData meshData;

		[Inject] private EnemyFactory _enemyFactory;

		private void Awake()
		{
			meshSurface.navMeshData = meshData;
		}

	}
}
