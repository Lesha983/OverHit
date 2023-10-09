using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChillPlay.OverHit.Service
{
	public class SlowMotionService
	{
		private float _slowMoTimeScale = 0.25f;
		private float _originalTimeScale = 1f;
		private List<ISlowMotion> _slowObjects = new();

		public void TurnOn()
		{
			foreach(var slowObject in _slowObjects)
			{
				slowObject.SetTimeScale(_slowMoTimeScale);
			}
		}

		public void TurnOff()
		{
			foreach (var slowObject in _slowObjects)
			{
				slowObject.SetTimeScale(_originalTimeScale);
			}
		}

		public void AddObject(ISlowMotion slowObject)
		{
			_slowObjects.Add(slowObject);
		}

		public void RemoveObject(ISlowMotion slowObject)
		{
			if (!_slowObjects.Contains(slowObject))
				return;

			_slowObjects.Remove(slowObject);
		}
	}
}
