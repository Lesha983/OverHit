using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Agent;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Weapons
{
	public abstract class AWeapon : MonoBehaviour
	{
		public int Damage { get; }

		public abstract void StartShooting(LayerMask damageablelayer, Action callback = null);

		public abstract void EndShooting();
	}
}
