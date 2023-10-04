using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Service;
using ChillPlay.OverHit.Settings;
using DG.Tweening;
using UnityEngine;
using Zenject;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	public class Agent : MonoBehaviour
	{
		[SF] private PlayerSettings _settings;
		[SF] private DashMarker _dashMarker;

		[Inject] AimService _aimService;
		[Inject] AimSettings _aimSettings;
		[Inject] CameraService _cameraService;

		private bool _aimActive;

		public Vector3 TargetPos { get; private set; }

		private void OnEnable()
		{
			_aimService.OnStartAim += StartAiming;
			_aimService.OnEndAim += EndAiming;
		}

		private void OnDisable()
		{
			_aimService.OnStartAim -= StartAiming;
			_aimService.OnEndAim -= EndAiming;
		}

		private void StartAiming()
		{
			_aimActive = true;
			StartCoroutine(nameof(Aiming));
			_cameraService.ChangeTarget(_dashMarker.SpriteTransform);
		}

		private void EndAiming()
		{
			_aimActive = false;
			OnMove();
			_cameraService.ChangeTarget(transform);
		}

		private IEnumerator Aiming()
		{
			_dashMarker.Show();
			while (_aimActive)
			{
				TargetPos = transform.position + _aimService.Direction * _aimService.Force;
				var (interactable, ray) = GetHitObject();

				switch (interactable)
				{
					case InteractableType.Enemy:
						_dashMarker.UpdateType(DashMarker.Type.Attack);
						TargetPos = ray.transform.position;
						break;
					case InteractableType.Wall:
						_dashMarker.UpdateType(DashMarker.Type.Move);
						TargetPos = ray.point + ray.normal * _aimSettings.WallOffset;
						break;
					default:
						_dashMarker.UpdateType(DashMarker.Type.Move);
						break;
				}

				_dashMarker.UpdatePosition(TargetPos);
				yield return null;
			}
			_dashMarker.Hide();
		}

		private (InteractableType, RaycastHit) GetHitObject()
		{
			if (!Physics.SphereCast(transform.position,
						_aimSettings.SphereCastRadius,
						_aimService.Direction,
						out var ray,
						_aimService.Force,
						_settings.Layer))
				return (InteractableType.None, ray);

			if (!ray.collider.TryGetComponent<IInteractable>(out var interactable))
				return (InteractableType.None, ray);

			return (interactable.Type, ray);
		}

		private void OnMove()
		{
			var seq = DOTween.Sequence();
			seq.Append(transform.DOMove(TargetPos, 0.5f).SetEase(Ease.OutSine));
		}
	}
}
