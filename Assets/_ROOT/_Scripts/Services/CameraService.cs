using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Service
{
	public class CameraService : MonoBehaviour
	{
		[SF] private Vector3 _offset;

		private Transform _currentTarget;

		private void LateUpdate()
		{
			if (_currentTarget == null)
				return;

			transform.position = _currentTarget.position + _offset;
		}

		public void ChangeTarget(Transform target)
		{
			_currentTarget = target;
		}
	}
}
