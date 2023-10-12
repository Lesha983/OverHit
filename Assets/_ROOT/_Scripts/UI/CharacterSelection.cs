using System;
using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Service;
using ChillPlay.OverHit.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.UI
{
	public class CharacterSelection : MonoBehaviour
	{
		[SF] private Transform parent;
		[SF] private AgentAvatar avatarPrefab;
		//[SF] private Image firstBG;
		//[SF] private Image secondBG;
		//[SF] private Button firstPlayer;
		//[SF] private Button secondPlayer;
		[SF] private Button startLevel;

		[SF] private MeshRenderer mesh;
		//[SF] private Color color;

		[Inject] private PlayerCollection _playerCollection;
		[Inject] private GameState _gameState;

		private void Awake()
		{
			SetupAvatars();
		}

		private void SetupAvatars()
		{
			foreach(var playerSettings in _playerCollection.PlayerSettings)
			{
				var avatar = Instantiate(avatarPrefab, parent);
				avatar.Setup(playerSettings.Icon);
				avatar.Button.onClick.AddListener(() => SelectAgent(playerSettings));
			}
		}

		//private void Awake()
		//{
		//	Choose(0);
		//	SetViewFirst();
		//}

		private void OnEnable()
		{
			//firstPlayer.onClick.AddListener(SetViewFirst);
			//secondPlayer.onClick.AddListener(SetViewSecond);

			//firstPlayer.onClick.AddListener(() => Choose(0));
			//secondPlayer.onClick.AddListener(() => Choose(1));
			startLevel.onClick.AddListener(StartLevel);
		}

		private void OnDisable()
		{
			//firstPlayer.onClick.RemoveAllListeners();
			//secondPlayer.onClick.RemoveAllListeners();
			startLevel.onClick.RemoveListener(StartLevel);
		}

		//private void Choose(int index)
		//{
		//	_gameState.UpdatePlayer(index);
		//	mesh.material = _playerCollection.PlayerSettings[index].Material;
		//}

		//private void SetViewFirst()
		//{
		//	color.a = 1f;
		//	firstBG.color = color;
		//	color.a = 0f;
		//	secondBG.color = color;
		//}

		//private void SetViewSecond()
		//{
		//	color.a = 1f;
		//	secondBG.color = color;
		//	color.a = 0f;
		//	firstBG.color = color;
		//}

		private void SelectAgent(PlayerSettings playerSettings)
		{
			var index = 0;
			foreach(var settings in _playerCollection.PlayerSettings)
			{
				if (settings == playerSettings)
					break;
				index++;
			}

			_gameState.UpdatePlayer(index);
			mesh.material = playerSettings.Material;
		}

		private void StartLevel()
		{
			SceneManager.LoadScene("Main");
		}
	}
}
