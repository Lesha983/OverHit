using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.UI
{
	[RequireComponent(typeof(Button))]
	public class AgentAvatar : MonoBehaviour
	{
		[SF] private Image icon;

		private Button _button;

		public Button Button => _button;

		public void Setup(Sprite sprite)
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(ClickAnimation);
			icon.sprite = sprite;
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(ClickAnimation);
		}

		private void ClickAnimation()
		{
			transform.DOPunchScale(Vector3.one * 0.15f, 0.4f, 1).SetEase(Ease.OutCubic);
		}
	}
}
