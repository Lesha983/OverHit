using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Weapons
{
	public class MeleeWeapon : AWeapon
	{
		[SF] private Transform _spawnMarker;
		[SF] private ProjectilePrefab _projectilePrefab;
		[SF] private int projectileDamage;

		private LayerMask _layer;
		private ProjectilePrefab _projectile;
		private bool _isShooting;

		public override void StartShooting(LayerMask layer)
		{
			if (_isShooting)
				return;

			_layer = layer;
			_projectile = Instantiate(_projectilePrefab, _spawnMarker);
			_projectile.OnHit += () => OnHit?.Invoke();
			_projectile.Shoot(transform.forward, projectileDamage, _layer);
			_isShooting = true;
		}

		public override void EndShooting()
		{
			if (!_isShooting)
				return;

			_projectile.OnHit -= () => OnHit?.Invoke();
			Destroy(_projectile.gameObject);
			_isShooting = false;
		}
	}
}
