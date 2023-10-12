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
		//[SF] private int projectileDamage;

		private ProjectilePrefab _projectile;
		private bool _isShooting;

		public override void StartShooting(LayerMask damageablelayer, int projectileDamage, Action callback = null)
		{
			if (_isShooting)
				return;

			_projectile = Instantiate(_projectilePrefab, _spawnMarker);
			_projectile.Shoot(transform.forward, projectileDamage, damageablelayer, callback);
			_isShooting = true;
		}

		public override IEnumerator StartShortShooting(LayerMask damageablelayer, int projectileDamage, float duration, Action callback = null)
		{
			if (_isShooting)
				yield break;

			_projectile = Instantiate(_projectilePrefab, _spawnMarker);
			_projectile.Shoot(transform.forward, projectileDamage, damageablelayer, callback);
			_isShooting = true;

			yield return new WaitForSeconds(duration);

			EndShooting();
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
