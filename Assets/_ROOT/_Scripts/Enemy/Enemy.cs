using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using ChillPlay.OverHit.Service;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Enemy
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class Enemy : Pawn, IInteractable, ISlowMotion
	{
		[SF] private LayerMask obstaclesLayer;
		[SF] private CollisionZone zone;
		[SF] private float attackRange;
		[SF] private float reloadTime;
		[SF] private float angularSpeed;
		[SF] private int damage;
		[SF] private float hitDuration;

		public InteractableType Type => InteractableType.Enemy;

		private NavMeshAgent _meshAgent;

		private Transform _targetTransform;
		private bool _hasTarget;

		[Inject] private SlowMotionService _slowMotionService;

		protected override void Awake()
		{
			base.Awake();
			_meshAgent = GetComponent<NavMeshAgent>();
			zone.Setup(targetLayer);
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
			while (distance > attackRange)
			{
				var direction = (transform.position - _targetTransform.position).normalized;
				distance = (transform.position - _targetTransform.position).magnitude;
				if (Physics.Raycast(transform.position,
									direction,
									out var obstacle,
									attackRange,
									obstaclesLayer))
				{
					Debug.Log("HAVE OBSTACLE");
					distance += float.MaxValue;
					DebugLine(direction, Color.red);
				}

				DebugLine(direction, Color.green);

				//var targetPos = _targetTransform.position + direction * attackRange;
				_meshAgent.SetDestination(_targetTransform.position);
				//yield return MoveToRoutine(targetPos);

				yield return null;
			}
		}

		private IEnumerator AttackRoutine()
		{
			weapon.StartShooting(targetLayer);
			yield return new WaitForSeconds(hitDuration);
			weapon.EndShooting();
		}

		private IEnumerator MoveToRoutine(Vector3 targetPos)
		{
			_meshAgent.SetDestination(targetPos);

			while (_meshAgent.hasPath)
				yield return null;
		}

		private void DebugLine(Vector3 direction, Color color)
		{
			Debug.DrawRay(transform.position, direction, color);
		}

		public void SetTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}
	}
}
