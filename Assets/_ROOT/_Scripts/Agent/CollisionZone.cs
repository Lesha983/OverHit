using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Agent
{
	[RequireComponent(typeof(SphereCollider))]
	public class CollisionZone : MonoBehaviour
	{
		public Action<Transform> OnColliderInZone;
		public Action<Transform> OnColliderOutZone;

		private LayerMask _targetLayer;
		private SphereCollider _collider;

		public bool HasColliderInZone { get; private set; }

		public void Setup(LayerMask targetLayer, float radius)
		{
			_targetLayer = targetLayer;
			HasColliderInZone = false;

			_collider = GetComponent<SphereCollider>();
			_collider.radius = radius;
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