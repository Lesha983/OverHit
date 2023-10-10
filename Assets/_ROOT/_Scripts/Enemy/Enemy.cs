using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Service;
using DG.Tweening;
using UnityEngine;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Enemy
{
	public class Enemy : Pawn, IInteractable, ISlowMotion
	{
		[SF] private LayerMask obstaclesLayer;
		[SF] private CollisionZone zone;
		[SF] private float attackRange;
		[SF] private float reloadTime;
		[SF] private float angularSpeed;
		[SF] private int damage;
		[SF] private float hitDuration;

		[Inject] private SlowMotionService _slowMotionService;

		public InteractableType Type => InteractableType.Enemy;

		private Transform _targetTransform;
		private bool _hasTarget;

		protected override void Awake()
		{
			base.Awake();
			zone.Setup(damageableLayer);
			_slowMotionService.AddObject(this);
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
			var reload = new WaitForSeconds(reloadTime);

			while (IsAlive)
			{
				var angle = Vector3.Angle(transform.forward, _targetTransform.forward);
				var rotateDuration = angle / angularSpeed;
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

			while (distance > attackRange)
			{
				var direction = (_targetTransform.position - transform.position).normalized;
				distance = (transform.position - _targetTransform.position).magnitude;
				if (Physics.Raycast(transform.position,
									direction,
									out var obstacle,
									attackRange,
									obstaclesLayer))
					distance = float.MaxValue;

				yield return null;
			}

			_movement.StopMovement();
		}

		private IEnumerator AttackRoutine()
		{
			weapon.StartShooting(damageableLayer);
			yield return new WaitForSeconds(hitDuration);
			weapon.EndShooting();
		}

		public void SetTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}
	}
}
