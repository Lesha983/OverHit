using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Service;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Weapons
{
	public class RangedWeapon : AWeapon
	{
		[SF] private Transform spawnMarker;
		[SF] private ProjectilePrefab projectilePrefab;
		[SF] private int projectileCount;
		[SF] private float projectileDelay;
		[SF] private int projectileDamage;
		[Range(0,45)]
		[SF] private int directionOffset;

		private LayerMask _damageablelayer;
		private int _projectileDamage;
		private Action _callback;
		private List<ProjectilePrefab> _projectiles = new();

		private int _randomOffset => UnityEngine.Random.Range(-directionOffset, directionOffset);

		[Inject] private SlowMotionService _slowMotionService;

		public override void StartShooting(LayerMask damageablelayer, int projectileDamage, Action callback = null)
		{
			_damageablelayer = damageablelayer;
			_projectileDamage = projectileDamage;
			_callback = callback;
			StartCoroutine(nameof(SpawnProjectiles));
		}

		public override IEnumerator StartShortShooting(LayerMask damageablelayer, int projectileDamage, float duration, Action callback = null)
		{
			StartShooting(damageablelayer, projectileDamage, callback);
			yield break;
		}

		public override void EndShooting()
		{
		}

		private IEnumerator SpawnProjectiles()
		{
			var delay = new WaitForSeconds(projectileDelay);
			for (var i = 0; i < projectileCount; i++)
			{
				var projectile = Instantiate(projectilePrefab, spawnMarker.position, Quaternion.identity);
				_projectiles.Add(projectile);
				_slowMotionService.AddObject(projectile);
				_callback += ()=> _slowMotionService.RemoveObject(projectile);
				var direction = Quaternion.Euler(0f, _randomOffset, 0f) * transform.forward;
				projectile.Shoot(direction, _projectileDamage, _damageablelayer, _callback);
				yield return delay;
			}
		}
	}
}
