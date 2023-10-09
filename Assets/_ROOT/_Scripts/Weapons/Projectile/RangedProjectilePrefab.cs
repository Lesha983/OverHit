using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Weapons
{
	public class RangedProjectilePrefab : ProjectilePrefab
	{
		[SF] private float speed;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<IDamageable>(out var damageable))
			{
				if ((_layer.value & 1 << other.gameObject.layer) == 0)
					return;

				damageable.TakeDamage(_damage);
				Instantiate(effectPrefab, transform.position, Quaternion.identity);
				OnHit?.Invoke();
				Destroy(gameObject);
			}

			if (other.TryGetComponent<IInteractable>(out var interactable))
			{
				Instantiate(effectPrefab, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}

		private void FixedUpdate()
		{
			//if (!_isShooting)
			//	return;

			transform.Translate(_direction * speed * Time.deltaTime);

			//if (!Physics.SphereCast(transform.position,
			//			transform.localScale.y,
			//			 _direction,
			//			out var hit,
			//			transform.localScale.x,
			//			_layer))
			//	return;

			//Hit(hit);
		}

		//protected override void Hit(RaycastHit hit)
		//{
		//	base.Hit(hit);
		//	Destroy(gameObject);
		//}
	}
}
