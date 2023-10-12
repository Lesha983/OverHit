using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Utility;
using UnityEngine;

namespace ChillPlay.OverHit
{
	public class WallObject : MonoBehaviour, IInteractable
	{
		public InteractableType Type => InteractableType.Wall;
	}
}
