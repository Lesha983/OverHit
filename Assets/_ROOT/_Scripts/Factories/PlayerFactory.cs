using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Level;
using UnityEngine;
using Zenject;

namespace ChillPlay.OverHit.Factory
{
	public class PlayerFactory : IFactory<Player, Transform, Player>
	{
		private DiContainer _container;

		public PlayerFactory(DiContainer container)
		{
			_container = container;
		}

		public Player Create(Player player, Transform parent)
		{
			return _container.InstantiatePrefabForComponent<Player>(player, parent);
		}
	}
}
