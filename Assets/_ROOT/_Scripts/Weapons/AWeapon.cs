using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Weapons
{
	public abstract class AWeapon : MonoBehaviour
	{
		public abstract void StartShooting(LayerMask damageablelayer, int projectileDamage, Action callback = null);

		public abstract IEnumerator StartShortShooting(LayerMask damageablelayer, int projectileDamage, float duration, Action callback = null);

		public abstract void EndShooting();
	}
}
