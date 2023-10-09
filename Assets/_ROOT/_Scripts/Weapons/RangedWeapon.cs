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
		[SF] private int projectileDamage;
		[Range(0,45)]
		[SF] private int directionOffset;

		private LayerMask _layer;
		private float _projectileDelay = 0.1f;
		private List<ProjectilePrefab> _projectiles = new();

		private int _randomOffset => UnityEngine.Random.Range(-directionOffset, directionOffset);

		[Inject] private SlowMotionService _slowMotionService;

		public override void StartShooting(LayerMask layer)
		{
			_layer = layer;
			StartCoroutine(nameof(SpawnProjectiles));
		}

		public override void EndShooting()
		{
			foreach (var projectile in _projectiles)
			{
				projectile.OnHit -= OnHit;
				_slowMotionService.RemoveObject(projectile);
			}
		}

		private IEnumerator SpawnProjectiles()
		{
			var delay = new WaitForSeconds(_projectileDelay);
			for (var i = 0; i < projectileCount; i++)
			{
				var projectile = Instantiate(projectilePrefab, spawnMarker.position, Quaternion.identity);
				projectile.OnHit += OnHit;
				_projectiles.Add(projectile);
				_slowMotionService.AddObject(projectile);
				var direction = Quaternion.Euler(0f, _randomOffset, 0f) * transform.forward;
				projectile.Shoot(direction, projectileDamage, _layer);
				yield return delay;
			}
		}
	}
}
