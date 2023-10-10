using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ChillPlay.OverHit.Factory;
using ChillPlay.OverHit.Service;
using UnityEngine;
using Zenject;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Installer
{
	public class GameInstaller : MonoInstaller
	{
		[SF] ScriptableObject[] _settingsObjects;

		public override void InstallBindings()
		{
			foreach (var so in _settingsObjects)
				BindSettings(so);

			BindService<AimService>();
			BindService<SlowMotionService>();
			BindService<GameState>();
			BindService<GameSave>();

			FindMonoService<CameraService>();

			BindService<LevelFactory>();
			BindService<PlayerFactory>();
			BindService<EnemyFactory>();
		}

		private void FindMonoService<T>() where T : MonoBehaviour
		{
			Container.BindInterfacesAndSelfTo<T>().FromComponentInHierarchy().AsSingle();
		}

		private void BindSettings(ScriptableObject so)
		{
			Container
				.BindInterfacesAndSelfTo(so.GetType())
				.FromInstance(so)
				.AsSingle();
		}

		public void BindService<T>() where T : class
		{
			Container
				.BindInterfacesAndSelfTo<T>()
				.FromNew()
				.AsSingle()
				.NonLazy();
		}
	}
}
