using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Utility;
using UnityEngine;

namespace ChillPlay.OverHit.Weapons
{
	public class MeleeProjectilePrefab : ProjectilePrefab
	{
		private void OnCollisionEnter(Collision collision)
		{
			if (!collision.collider.TryGetComponent<IDamageable>(out var damageable))
				return;

			damageable.TakeDamage(_damage);
			Instantiate(effectPrefab, collision.contacts[0].point, Quaternion.identity);
			_callback?.Invoke();
		}
	}
}
