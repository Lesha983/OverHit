using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Service;
using ChillPlay.OverHit.Settings;
using ChillPlay.OverHit.Weapons;
using DG.Tweening;
using UnityEngine;
using Zenject;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	public class Player : Pawn
	{
		protected class AimInfo
		{
			public Vector3 TargetPosition;
			public bool TargetIsEnemy;
		}

		[SF] private float speed;
		[SF] protected PlayerSettings settings;
		[SF] protected DashMarker dashMarker;

		[Inject] AimService _aimService;
		[Inject] AimSettings _aimSettings;
		[Inject] CameraService _cameraService;
		[Inject] SlowMotionService _slowMotionService;

		protected override void Awake()
		{
			base.Awake();
			StartCoroutine(nameof(CoreRoutine));
		}

		public IEnumerator CoreRoutine()
		{
			while (IsAlive)
			{
				SetupBeforeAiming();
				var aimInfo = new AimInfo();
				var startAiming = false;

				Action startAimCallback = () => startAiming = true;
				_aimService.OnStartAim += startAimCallback;
				yield return new WaitUntil(() => startAiming);
				_aimService.OnStartAim -= startAimCallback;

				_slowMotionService.TurnOn();
				yield return Aiming(aimInfo);
				_slowMotionService.TurnOff();
				SetupAfterAiming();
				yield return MoveAndAttack(aimInfo);
			}

			Die();
		}

		private IEnumerator Aiming(AimInfo aimInfo)
		{
			var targetPos = new Vector3();
			var targetIsEnemy = false;
			var aiming = true;

			dashMarker.Show(settings.MarkerMoveColor, settings.MarkerMoveSprite);

			Action endAimCallback = () => aiming = false;
			_aimService.OnEndAim += endAimCallback;

			while (aiming)
			{
				targetIsEnemy = false;
				targetPos = transform.position + _aimService.Direction * _aimService.Force;
				var (interactable, ray) = GetHitObject();

				switch (interactable)
				{
					case InteractableType.Enemy:
						AimToEnemy();
						targetIsEnemy = true;
						targetPos = ray.transform.position;
						break;
					case InteractableType.Wall:
						dashMarker.UpdateView(settings.MarkerMoveColor, settings.MarkerMoveSprite);
						targetPos = ray.point + ray.normal * _aimSettings.WallOffset;
						break;
					default:
						dashMarker.UpdateView(settings.MarkerMoveColor, settings.MarkerMoveSprite);
						break;
				}

				dashMarker.UpdatePosition(targetPos);
				yield return null;
			}

			_aimService.OnEndAim -= endAimCallback;
			dashMarker.Hide();
			aimInfo.TargetPosition = targetPos;
			aimInfo.TargetIsEnemy = targetIsEnemy;
		}

		private (InteractableType, RaycastHit) GetHitObject()
		{
			if (!Physics.SphereCast(transform.position,
						_aimSettings.SphereCastRadius,
						_aimService.Direction,
						out var ray,
						_aimService.Force,
						settings.Layer))
				return (InteractableType.None, ray);

			if (!ray.collider.TryGetComponent<IInteractable>(out var interactable))
				return (InteractableType.None, ray);

			return (interactable.Type, ray);
		}

		protected virtual void SetupBeforeAiming()
		{
			_cameraService.ChangeTarget(dashMarker.SpriteTransform);
		}

		protected virtual void SetupAfterAiming()
		{
			_cameraService.ChangeTarget(transform);
		}

		protected virtual void AimToEnemy()
		{
			dashMarker.UpdateView(settings.MarkerAttackColor, settings.MarkerMeleeAttackSprite);
		}

		protected virtual IEnumerator MoveAndAttack(AimInfo aimInfo)
		{
			var targetPos = aimInfo.TargetPosition;
			var direction = (targetPos - transform.position).normalized;
			var distance = (targetPos - transform.position).magnitude;
			var duration = distance / speed;

			transform.forward = direction;
			var twin = transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
			weapon.StartShooting(targetLayer);

			Action callback = () => twin.Kill();

			weapon.OnHit += callback;
			yield return twin.WaitForCompletion();
			weapon.OnHit -= callback;

			weapon.EndShooting();
		}
	}
}
