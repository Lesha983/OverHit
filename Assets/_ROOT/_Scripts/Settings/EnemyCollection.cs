using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Enemy;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Settings
{
	[CreateAssetMenu(menuName = ("OverHit/" + nameof(EnemyCollection)), fileName = nameof(EnemyCollection))]
	public class EnemyCollection : ScriptableObject
	{
		[field: SF]
		public Enemy.Enemy MeleeEnemy { get; private set; }

		[field: SF]
		public Enemy.Enemy RangedEnemy { get; private set; }
	}
}
