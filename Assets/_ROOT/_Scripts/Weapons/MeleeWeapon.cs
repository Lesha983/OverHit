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

		private ProjectilePrefab _projectile;
		private bool _isShooting;

		public override void StartShooting(LayerMask damageablelayer, Action callback = null)
		{
			if (_isShooting)
				return;

			_projectile = Instantiate(_projectilePrefab, _spawnMarker);
			_projectile.Shoot(transform.forward, projectileDamage, damageablelayer, callback);
			_isShooting = true;
		}

		public override void EndShooting()
		{
			if (!_isShooting)
				return;

			Destroy(_projectile.gameObject);
			_isShooting = false;
		}
	}
}
