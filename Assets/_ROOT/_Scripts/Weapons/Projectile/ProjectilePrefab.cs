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
		protected LayerMask _damageablelayer;
		protected bool _isShooting;

		protected Action _callback;

		public void Shoot(Vector3 direction, int damage, LayerMask damageablelayer, Action callback = null)
		{
			_direction = direction;
			_damage = damage;
			_damageablelayer = damageablelayer;
			_isShooting = true;
			_callback = callback;
		}

		public void SetTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}
	}
}
