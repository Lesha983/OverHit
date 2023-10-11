using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Service;
using ChillPlay.OverHit.Settings;
using ChillPlay.OverHit.Utility;
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

		[SF] protected DashMarker dashMarker;
		[SF] protected Skin skin;
		[SF] protected MeleeWeapon weapon;

		[Inject] AimService _aimService;
		[Inject] AimSettings _aimSettings;
		[Inject] CameraService _cameraService;
		[Inject] SlowMotionService _slowMotionService;

		protected PlayerSettings _settings;

		public void Setup(PlayerSettings settings)
		{
			_settings = settings;
			skin.Setup(_settings.Material);
			_movement.UpdateSpeedValue(_settings.AgentSpeed);
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

		public IEnumerator AgentMoveToRoutine(Vector3 destination)
		{
			yield return _movement.MoveToRoutine(destination);
		}

		private IEnumerator Aiming(AimInfo aimInfo)
		{
			var targetPos = new Vector3();
			var targetIsEnemy = false;
			var aiming = true;

			dashMarker.Show(_settings.MarkerMoveColor, _settings.MarkerMoveSprite);

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
						dashMarker.UpdateView(_settings.MarkerMoveColor, _settings.MarkerMoveSprite);
						targetPos = ray.point + ray.normal * _aimSettings.WallOffset;
						break;
					default:
						dashMarker.UpdateView(_settings.MarkerMoveColor, _settings.MarkerMoveSprite);
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
						_settings.InteractableLayer))
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
			dashMarker.UpdateView(_settings.MarkerAttackColor, _settings.MarkerMeleeAttackSprite);
		}

		protected virtual IEnumerator MoveAndAttack(AimInfo aimInfo)
		{
			var targetPos = aimInfo.TargetPosition;
			var direction = (targetPos - transform.position).normalized;
			var distance = (targetPos - transform.position).magnitude;
			var duration = distance / _settings.MoveToAttackSpeed;

			transform.forward = direction;
			var twin = transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
			weapon.StartShooting(damageableLayer, () => twin.Kill());

			yield return twin.WaitForCompletion();

			weapon.EndShooting();
		}
	}
}
