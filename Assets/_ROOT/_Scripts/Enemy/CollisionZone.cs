using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Enemy
{
	public class CollisionZone : MonoBehaviour
	{
		public Action<Transform> OnPlayerInZone;

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag != "Player")
				return;

			OnPlayerInZone?.Invoke(other.transform);
		}
	}
}