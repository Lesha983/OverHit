using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Agent
{
	[RequireComponent(typeof(Collider))]
	public class CollisionZone : MonoBehaviour
	{
		public Action<Transform> OnColliderInZone;
		public Action<Transform> OnColliderOutZone;

		private LayerMask _targetLayer;

		public bool HasColliderInZone { get; private set; }

		public void Setup(LayerMask targetLayer)
		{
			_targetLayer = targetLayer;
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((_targetLayer.value & 1 << other.gameObject.layer) == 0)
				return;

			OnColliderInZone?.Invoke(other.transform);
		}

		private void OnTriggerStay(Collider other)
		{
			if (HasColliderInZone)
				return;

			if ((_targetLayer.value & 1 << other.gameObject.layer) == 0)
				return;

			HasColliderInZone = true;
		}

		private void OnTriggerExit(Collider other)
		{
			if ((_targetLayer.value & 1 << other.gameObject.layer) == 0)
				return;

			HasColliderInZone = false;
			OnColliderOutZone?.Invoke(other.transform);
		}
	}
}