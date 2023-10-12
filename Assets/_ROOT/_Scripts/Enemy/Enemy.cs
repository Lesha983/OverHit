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
		[SF] private Collider collider;

		[SF] private int health;
		[SF] private float attackRange;
		[SF] private float detectRadius;
		[SF] private int damage;
		[SF] private float reloadTime;
		[SF] private float hitDuration;
		[SF] private float angularSpeed;
		[SF] private float agentSpeed;

		public InteractableType Type => InteractableType.Enemy;
		public AttackType AttackType => attackType;

		private Transform _targetTransform;
		private bool _hasTarget;

		public void Setup()
		{
			_movement = GetComponent<PawnMovement>();

			_currentHealth = health;
			zone.Setup(damageableLayer, detectRadius);
			_movement.Setup(agentSpeed);
		}

		public void SetTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}

		protected override void Die()
		{
			base.Die();
			//StopCoroutine(nameof(CoreRoutine));
			StopAllCoroutines();
			DieAnimation();
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
			yield return weapon.StartShortShooting(damageableLayer, damage, hitDuration);
		}

		private void DieAnimation()
		{
			collider.enabled = false;
			var sequence = DOTween.Sequence();
			sequence.Append(transform.DOLocalRotate(new Vector3(90f,0f,0f), 1.2f).SetEase(Ease.OutQuad));
			sequence.AppendCallback(() => Destroy(gameObject));
		}
	}
}
