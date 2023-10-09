using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Level;
using UnityEngine;
using Zenject;

namespace ChillPlay.OverHit.Factory
{
	public class LevelFactory : IFactory<LevelPrefab, Transform, LevelPrefab>
	{
		private DiContainer _container;

		public LevelFactory(DiContainer container)
		{
			_container = container;
		}

		public LevelPrefab Create(LevelPrefab prefab, Transform parent)
		{
			return _container.InstantiatePrefabForComponent<LevelPrefab>(prefab, parent);
		}
	}
}
