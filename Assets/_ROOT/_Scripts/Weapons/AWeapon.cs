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

		public Action OnHit;

		public abstract void StartShooting(LayerMask layer);

		public abstract void EndShooting();
	}
}
