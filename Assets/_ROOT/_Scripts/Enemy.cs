using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit
{
	public class Enemy : MonoBehaviour, IInteractable
	{
		public InteractableType Type => InteractableType.Enemy;
	}
}
