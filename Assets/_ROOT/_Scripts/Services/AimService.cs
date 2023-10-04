using System;
using System.Collections;
using System.Collections.Generic;
using Chillplay.Input;
using Chillplay.Input.Drags;
using ChillPlay.OverHit.Settings;
using UnityEngine;
using Zenject;

namespace ChillPlay.OverHit.Service
{
	public class AimService : IInitializable, IDisposable
	{
		[Inject] IInputProvider _inputProvider;
		[Inject] AimSettings _aimSettings;

		public Action OnStartAim;
		public Action OnEndAim;

		public float Force { get; private set; }
		public Vector3 Direction { get; private set; }

		public void Initialize()
		{
			_inputProvider.OnPointerDown += OnPointerDown;
			_inputProvider.OnDrag += OnDrag;
			_inputProvider.OnPointerUp += OnPointerUp;
		}

		public void Dispose()
		{
			_inputProvider.OnPointerDown -= OnPointerDown;
			_inputProvider.OnDrag -= OnDrag;
			_inputProvider.OnPointerUp -= OnPointerUp;
		}

		private void OnPointerDown(Vector2 position)
		{
			OnStartAim?.Invoke();
		}

		private void OnDrag(Drag drag)
		{
			Force = drag.OverallDelta.magnitude * _aimSettings.ForceCoeff;
			var direction = -drag.OverallDelta.normalized;
			Direction = new Vector3(direction.x, 0f, direction.y);
		}

		private void OnPointerUp(Vector2 position)
		{
			OnEndAim?.Invoke();
			Force = 0f;
			Direction = Vector3.zero;
		}
	}
}
