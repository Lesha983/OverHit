using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using SF = UnityEngine.SerializeField;

namespace ChillPlay.OverHit.Installer
{
	public class Installer : MonoInstaller
	{
		[SF] protected ScriptableObject[] settingsObjects;

		protected void FindMonoService<T>() where T : MonoBehaviour
		{
			Container.BindInterfacesAndSelfTo<T>().FromComponentInHierarchy().AsSingle();
		}

		protected void BindSettings(ScriptableObject so)
		{
			Container
				.BindInterfacesAndSelfTo(so.GetType())
				.FromInstance(so)
				.AsSingle();
		}

		protected void BindService<T>() where T : class
		{
			Container
				.BindInterfacesAndSelfTo<T>()
				.FromNew()
				.AsSingle()
				.NonLazy();
		}
	}
}
