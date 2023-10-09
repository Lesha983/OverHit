using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Weapons
{
	public class MeleeProjectilePrefab : ProjectilePrefab
	{
		[SF] private float hitDistance;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.TryGetComponent<IDamageable>(out var damageable))
				return;

			damageable.TakeDamage(_damage);
			Instantiate(effectPrefab, transform.position, Quaternion.identity);
			OnHit?.Invoke();
		}

		//private void FixedUpdate()
		//{
		//	if (!Physics.SphereCast(transform.position,
		//						hitDistance,
		//						_direction,
		//						out var hit,
		//						hitDistance,
		//						_layer))
		//		return;

		//	Hit(hit);
		//}

		//private void OnDrawGizmos()
		//{
		//	DrawSphereCast(transform.position, transform.forward, hitDistance, hitDistance);
		//}

		//void DrawSphereCast(Vector3 origin, Vector3 direction, float radius, float distance)
		//{
		//	Gizmos.color = UnityEngine.Color.green;
		//	Gizmos.DrawWireSphere(origin + direction * distance, radius);
		//	Gizmos.DrawLine(origin, origin + direction * distance);
		//}
	}
}
