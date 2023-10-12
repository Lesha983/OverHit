using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Settings;
using ChillPlay.OverHit.Weapons;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Agent
{
	public class RangedPlayer : Player
	{
		[SF] private RangedWeapon rangedWeapon;
		[SF] private CollisionZone collisionZone;
		[SF] private GameObject meleeZoneSprite;

		public override void Setup(PlayerSettings settings)
		{
			base.Setup(settings);
			collisionZone.Setup(damageableLayer, _settings.RadiusMeleeZone);
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
								? _settings.MarkerMeleeAttackSprite
								: _settings.MarkerRangedAttackSprite;
			dashMarker.UpdateView(_settings.MarkerAttackColor, markerSprite);
		}

		protected override IEnumerator MoveAndAttack(AimInfo aimInfo)
		{
			if (aimInfo.TargetIsEnemy && !collisionZone.HasColliderInZone)
			{
				var direction = (aimInfo.TargetPosition - transform.position).normalized;
				transform.forward = direction;
				RangedShoot(aimInfo.TargetPosition);
				yield break;
			}

			yield return base.MoveAndAttack(aimInfo);
		}

		private void RangedShoot(Vector3 targetPos)
		{
			var direction = (targetPos - transform.position).normalized;
			transform.forward = direction;
			rangedWeapon.StartShooting(damageableLayer, _settings.RangedProjectileDamage);
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
