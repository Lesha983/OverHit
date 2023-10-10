using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Weapons
{
	public class RangedProjectilePrefab : ProjectilePrefab
	{
		[SF] private float speed;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
			{
				if ((_damageablelayer.value & 1 << collision.gameObject.layer) == 0)
					return;

				damageable.TakeDamage(_damage);
				Instantiate(effectPrefab, collision.contacts[0].point, Quaternion.identity);
				_callback?.Invoke();
				Destroy(gameObject);
			}

			if (collision.collider.TryGetComponent<IInteractable>(out var interactable))
			{
				Instantiate(effectPrefab, collision.contacts[0].point, Quaternion.identity);
				Destroy(gameObject);
			}
		}

		private void FixedUpdate()
		{
			transform.Translate(_direction * speed * Time.deltaTime);
		}
	}
}
