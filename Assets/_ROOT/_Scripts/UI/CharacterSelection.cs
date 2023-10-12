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
		[SF] private Button startLevel;

		[SF] private MeshRenderer mesh;

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

		private void OnEnable()
		{
			startLevel.onClick.AddListener(StartLevel);
		}

		private void OnDisable()
		{
			startLevel.onClick.RemoveListener(StartLevel);
		}

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
