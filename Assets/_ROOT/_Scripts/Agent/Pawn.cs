using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Weapons;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	[RequireComponent(typeof(PawnMovement))]
	public class Pawn : MonoBehaviour, IDamageable
	{
		[SF] protected LayerMask damageableLayer;
		[SF] protected int heals;
		[SF] protected AWeapon weapon;

		public Action OnDie;

		private int _currentHeals;
		protected PawnMovement _movement;

		public bool IsAlive => _currentHeals > 0;

		public void TakeDamage(int damage)
		{
			_currentHeals -= damage;
		}

		protected virtual void Awake()
		{
			_movement = GetComponent<PawnMovement>();
			_currentHeals = heals;
		}

		protected void Die()
		{
			Debug.Log($"gameObject: {name}; OnDie");
			OnDie?.Invoke();
		}
	}
}
