using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Weapons;
using UnityEngine;
using UnityEngine.AI;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class Pawn : MonoBehaviour, IDamageable
	{
		[SF] protected LayerMask targetLayer;
		[SF] protected int heals;
		[SF] protected AWeapon weapon;

		public Action OnDie;

		private int _currentHeals;
		protected NavMeshAgent _meshAgent;

		public bool IsAlive => _currentHeals > 0;

		public void TakeDamage(int damage)
		{
			_currentHeals -= damage;
		}

		protected virtual void Awake()
		{
			_currentHeals = heals;
			_meshAgent = GetComponent<NavMeshAgent>();
		}

		protected void Die()
		{
			Debug.Log($"gameObject: {name}; OnDie");
			OnDie?.Invoke();
		}
	}
}
