using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Utility;
using UnityEngine;

namespace ChillPlay.OverHit
{
	public class Wall : MonoBehaviour, IInteractable
	{
		public InteractableType Type => InteractableType.Wall;
	}
}