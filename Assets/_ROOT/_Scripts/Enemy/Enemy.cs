using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Enemy
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class Enemy : MonoBehaviour, IInteractable
	{
		[SF] private CollisionZone _zone;
		[SF] private int _heals;

		public InteractableType Type => InteractableType.Enemy;

		private NavMeshAgent _meshAgent;

		private Transform _targetTransform;
		private bool _hasTarget;
		private float _attackRange = 2f;
		private bool _isAlive => _heals > 0;

		private void Awake()
		{
			_meshAgent = GetComponent<NavMeshAgent>();
		}

		private void OnEnable()
		{
			_zone.OnPlayerInZone += DetectAgent;
		}

		private void OnDisable()
		{
			_zone.OnPlayerInZone -= DetectAgent;
		}

		private void DetectAgent(Transform agent)
		{
			if (_hasTarget)
				return;

			_targetTransform = agent;
			_hasTarget = true;
			StartCoroutine(nameof(Attack));
		}

		private IEnumerator Attack()
		{
			while (_isAlive)
			{
				var rotateTween = transform.DOLookAt(_targetTransform.position, 1f).SetRelative();
				yield return rotateTween.WaitForCompletion();

				var distance = (transform.position - _targetTransform.position).magnitude;

				if(distance > _attackRange)
				{
					var direction = (transform.position - _targetTransform.position).normalized;
					var targetPos = _targetTransform.position + direction * _attackRange;
					yield return MoveTo(targetPos);
					continue;
				}

				yield return null;
			}
		}

		private IEnumerator MoveTo(Vector3 targetPos)
		{
			_meshAgent.SetDestination(targetPos);

			while (_meshAgent.hasPath)
				yield return null;
		}
	}
}
