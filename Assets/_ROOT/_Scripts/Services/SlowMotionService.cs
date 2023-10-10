using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ChillPlay.OverHit.Service
{
	public class SlowMotionService
	{
		private float _slowMoTimeScale = 0.25f;
		private float _originalTimeScale = 1f;
		private float _changeDuration = 0.25f;
		private List<ISlowMotion> _slowObjects = new();

		public void TurnOn()
		{
			var sequence = DOTween.Sequence();
			foreach(var slowObject in _slowObjects)
			{
				sequence.Join(DOVirtual.Float(
											_originalTimeScale,
											_slowMoTimeScale,
											_changeDuration,
											(value) => slowObject.SetTimeScale(value))
										.SetEase(Ease.OutSine));
			}
		}

		public void TurnOff()
		{
			var sequence = DOTween.Sequence();
			foreach (var slowObject in _slowObjects)
			{
				sequence.Join(DOVirtual.Float(
											_slowMoTimeScale,
											_originalTimeScale,
											_changeDuration,
											(value) => slowObject.SetTimeScale(value))
										.SetEase(Ease.OutSine));
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
