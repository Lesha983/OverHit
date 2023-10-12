using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Level;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.UI
{
	public class GameOverScreen : MonoBehaviour
	{
		[SF] private Image background;
		[SF] private Button home;
		[SF] private Button play;

		[Inject] private LevelRoot _levelRoot;

		private void Awake()
		{
			background.gameObject.SetActive(false);
			home.gameObject.SetActive(false);
			play.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			_levelRoot.CurrebtLevel.OnLevelCompleted += Show;

			home.onClick.AddListener(OnHome);
			play.onClick.AddListener(OnPlay);
		}

		private void OnDisable()
		{
			_levelRoot.CurrebtLevel.OnLevelCompleted -= Show;

			home.onClick.RemoveListener(OnHome);
			play.onClick.RemoveListener(OnPlay);
		}

		private void Show()
		{
			var color = background.color;
			color.a = 0;
			background.color = color;
			background.gameObject.SetActive(true);

			var sequence = DOTween.Sequence();
			sequence.Append(background.DOFade(1f, 0.75f).SetEase(Ease.OutQuad));
			sequence.AppendCallback(() =>
			{
				home.gameObject.SetActive(true);
				play.gameObject.SetActive(true);
			});
		}

		private void OnHome()
		{
			SceneManager.LoadScene("Menu");
		}

		private void OnPlay()
		{
			SceneManager.LoadScene("Main");
		}
	}
}
