using System;
using System.Collections;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Weapons
{
	public class ProjectilePrefab : MonoBehaviour, ISlowMotion
	{
		[SF] protected ParticleSystem effectPrefab;

		protected Vector3 _direction;
		protected int _damage;
		protected LayerMask _layer;
		protected bool _isShooting;

		public Action OnHit;

		public void Shoot(Vector3 direction, int damage, LayerMask layer)
		{
			_direction = direction;
			_damage = damage;
			_layer = layer;
			_isShooting = true;
		}

		public void SetTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}

		//protected virtual void Hit(RaycastHit hit)
		//{
		//	if (!hit.collider.TryGetComponent<IDamageable>(out var damageable))
		//		return;

		//	damageable.TakeDamage(_damage);
		//	Instantiate(effectPrefab, transform.position, Quaternion.identity);
		//	OnHit?.Invoke();
		//}
	}
}
