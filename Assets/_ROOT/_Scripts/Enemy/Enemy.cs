using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Settings;
using ChillPlay.OverHit.Utility;
using ChillPlay.OverHit.Weapons;
using DG.Tweening;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Enemy
{
	public class Enemy : Pawn, IInteractable, ISlowMotion
	{
		[SF] private AttackType attackType;
		[SF] private LayerMask obstaclesLayer;
		[SF] private CollisionZone zone;
		[SF] private AWeapon weapon;

		public InteractableType Type => InteractableType.Enemy;
		public AttackType AttackType => attackType;

		private EnemySettings _settings;
		private Transform _targetTransform;
		private bool _hasTarget;

		public void Setup(EnemySettings settings)
		{
			_settings = settings;
			zone.Setup(damageableLayer, _settings.DetectRadius);
			_movement.UpdateSpeedValue(_settings.AgentSpeed);
		}

		public void SetTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}

		private void OnEnable()
		{
			zone.OnColliderInZone += DetectAgent;
		}

		private void OnDisable()
		{
			zone.OnColliderInZone -= DetectAgent;
		}

		private void DetectAgent(Transform agent)
		{
			if (_hasTarget)
				return;

			_targetTransform = agent;
			_hasTarget = true;
			StartCoroutine(nameof(CoreRoutine));
		}

		private IEnumerator CoreRoutine()
		{
			var reload = new WaitForSeconds(_settings.ReloadTime);

			while (IsAlive)
			{
				var angle = Vector3.Angle(transform.forward, _targetTransform.forward);
				var rotateDuration = angle / _settings.AngularSpeed;
				var rotateTween = transform.DOLookAt(_targetTransform.position, rotateDuration);
				yield return rotateTween.WaitForCompletion();

				yield return MoveToAttackRangeRoutine();

				yield return AttackRoutine();
				yield return reload;
			}

			Die();
		}

		private IEnumerator MoveToAttackRangeRoutine()
		{
			var distance = float.MaxValue;
			_movement.MoveTo(_targetTransform.position);

			while (distance > _settings.AttackRange)
			{
				var direction = (_targetTransform.position - transform.position).normalized;
				distance = (transform.position - _targetTransform.position).magnitude;
				if (Physics.Raycast(transform.position,
									direction,
									out var obstacle,
									_settings.AttackRange,
									obstaclesLayer))
					distance = float.MaxValue;

				yield return null;
			}

			_movement.StopMovement();
		}

		private IEnumerator AttackRoutine()
		{
			yield return weapon.StartShortShooting(damageableLayer, _settings.HitDuration);
		}
	}
}
