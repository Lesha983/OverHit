using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Enemy;
using ChillPlay.OverHit.Settings;
using ChillPlay.OverHit.Weapons;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	public class RangedPlayer : Player
	{
		[SF] private float radiusMeleeZone;
		[SF] private AWeapon rangedWeapon;
		[SF] private CollisionZone collisionZone;
		[SF] private GameObject meleeZoneSprite;

		protected override void Awake()
		{
			base.Awake();
			collisionZone.Setup(targetLayer);
		}

		protected override void SetupBeforeAiming()
		{
			base.SetupBeforeAiming();
			ShowMeleeZone();
		}

		protected override void SetupAfterAiming()
		{
			base.SetupAfterAiming();
			HideMeleeZone();
		}

		protected override void AimToEnemy()
		{
			var markerSprite = collisionZone.HasColliderInZone
								? settings.MarkerMeleeAttackSprite
								: settings.MarkerRangedAttackSprite;
			dashMarker.UpdateView(settings.MarkerAttackColor, markerSprite);
		}

		protected override IEnumerator MoveAndAttack(AimInfo aimInfo)
		{
			if (aimInfo.TargetIsEnemy && !CheckTargetForMelleZone(aimInfo.TargetPosition))
			{
				RangedShoot(aimInfo.TargetPosition);
				yield break;
			}

			yield return base.MoveAndAttack(aimInfo);
		}

		private bool CheckTargetForMelleZone(Vector3 targetPos)
		{
			var distance = (targetPos - transform.position).magnitude;
			return distance <= radiusMeleeZone;
		}

		private void RangedShoot(Vector3 targetPos)
		{
			var direction = (targetPos - transform.position).normalized;
			transform.forward = direction;
			rangedWeapon.StartShooting(targetLayer);
		}

		private void ShowMeleeZone()
		{
			if (!collisionZone.HasColliderInZone)
				return;

			meleeZoneSprite.SetActive(true);
		}

		private void HideMeleeZone()
		{
			meleeZoneSprite.SetActive(false);
		}
	}
}
