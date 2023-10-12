using System;
using System.Collections;
using ChillPlay.OverHit.Utility;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	[RequireComponent(typeof(PawnMovement))]
	public class Pawn : MonoBehaviour, IDamageable
	{
		[SF] protected LayerMask damageableLayer;

		public Action OnDie;

		protected int _currentHealth;
		protected PawnMovement _movement;

		public bool IsAlive => _currentHealth > 0;
		public int CurrentHealth => _currentHealth;

		public void TakeDamage(int damage)
		{
			_currentHealth -= damage;
			Debug.Log($"TakeDamage: damage = {damage}; currentHealth = {_currentHealth}");

			if (!IsAlive)
				Die();
		}

		protected virtual void Die()
		{
			Debug.Log($"gameObject: {name}; OnDie");
			OnDie?.Invoke();
		}
	}
}
