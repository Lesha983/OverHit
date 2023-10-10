using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ChillPlay.OverHit.Enemy;

namespace ChillPlay.OverHit.Factory
{
	public class EnemyFactory : IFactory<Enemy.Enemy, Transform, Enemy.Enemy>
	{
		private DiContainer _container;

		public EnemyFactory(DiContainer container)
		{
			_container = container;
		}

		public Enemy.Enemy Create(Enemy.Enemy enemy, Transform parent)
		{
			return _container.InstantiatePrefabForComponent<Enemy.Enemy>(enemy, parent);
		}
	}
}
