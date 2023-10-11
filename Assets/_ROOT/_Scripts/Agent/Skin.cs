using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Agent
{
	[RequireComponent(typeof(MeshRenderer))]
	public class Skin : MonoBehaviour
	{
		public void Setup(Material material)
		{
			GetComponent<MeshRenderer>().material = material;
		}

		public void SetAnimation()
		{

		}

		public IEnumerator SetAnimationRoutine()
		{
			yield return null;
		}
	}
}
