using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class PawnMovement : MonoBehaviour
	{
		private NavMeshAgent _agent;
		private bool _isMoving;

		public void Setup(float speedValue)
		{
			_agent = GetComponent<NavMeshAgent>();
			_agent.speed = speedValue;
		}

		public void MoveTo(Vector3 destination)
		{
			_agent.isStopped = false;
			_isMoving = true;

			_agent.SetDestination(destination);
		}

		public IEnumerator MoveToRoutine(Vector3 destination)
		{
			MoveTo(destination);

			while (ReachesDestination())
				yield return null;

			StopMovement();
		}

		public void StopMovement()
		{
			if (!_isMoving)
				return;

			_isMoving = false;
			_agent.isStopped = true;
		}

		public void UpdateSpeedValue(float value)
		{
			_agent.speed = value;
		}

		private bool ReachesDestination()
		{
			if (!_isMoving)
				return false;

			if (_agent.pathPending || _agent.remainingDistance > _agent.stoppingDistance)
				return true;

			return false;
		}
	}
}
