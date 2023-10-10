using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ChillPlay.OverHit.Agent;
using DG.Tweening;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Level
{
	public class Lift : MonoBehaviour
	{
		[SF] private Transform leftDoor;
		[SF] private Transform rightDoor;

		[SF] private Vector3 leftOpenLocalPos;
		[SF] private Vector3 rightOpenLocalPos;

		[SF] private float moveDuration;

		[SF] private Transform playerTargetMarker;

		public IEnumerator InteractionRoutine(Player player)
		{
			var leftDoorStartPos = leftDoor.transform.localPosition;
			var rightDoorStartPos = rightDoor.transform.localPosition;

			var openDoorSequence = DOTween.Sequence();
			openDoorSequence.Append(leftDoor.DOLocalMove(leftOpenLocalPos, moveDuration).SetEase(Ease.InSine));
			openDoorSequence.Join(rightDoor.DOLocalMove(rightOpenLocalPos, moveDuration).SetEase(Ease.InSine));

			yield return openDoorSequence.WaitForCompletion();
			yield return player.MoveToRoutine(playerTargetMarker.position);

			var closeDoorSequence = DOTween.Sequence();
			closeDoorSequence.Append(leftDoor.DOLocalMove(leftDoorStartPos, moveDuration).SetEase(Ease.InSine));
			closeDoorSequence.Join(rightDoor.DOLocalMove(rightDoorStartPos, moveDuration).SetEase(Ease.InSine));

			yield return closeDoorSequence.WaitForCompletion();
		}
	}
}
