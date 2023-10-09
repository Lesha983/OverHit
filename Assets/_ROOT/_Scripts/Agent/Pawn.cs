using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Weapons;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	public class Pawn : MonoBehaviour, IDamageable
	{
		[SF] protected LayerMask targetLayer;
		[SF] protected int heals;
		[SF] protected AWeapon weapon;

		private int _currentHeals;

		public bool IsAlive => _currentHeals > 0;

		public void TakeDamage(int damage)
		{
			_currentHeals -= damage;
		}

		protected virtual void Awake()
		{
			_currentHeals = heals;
		}

		protected void Die()
		{

		}
	}
}
