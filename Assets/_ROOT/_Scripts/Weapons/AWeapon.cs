using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Weapons
{
	public abstract class AWeapon : MonoBehaviour
	{
		public abstract void StartShooting(LayerMask damageablelayer, Action callback = null);

		public abstract IEnumerator StartShortShooting(LayerMask damageablelayer, float duration, Action callback = null);

		public abstract void EndShooting();
	}
}
