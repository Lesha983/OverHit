using System.Collections;
using System.Collections.Generic;
using ChillPlay.OverHit.Factory;
using ChillPlay.OverHit.Level;
using ChillPlay.OverHit.Service;

namespace ChillPlay.OverHit.Installer
{
	public class GameInstaller : Installer
	{
		public override void InstallBindings()
		{
			foreach (var so in settingsObjects)
				BindSettings(so);

			BindService<AimService>();
			BindService<SlowMotionService>();
			BindService<GameState>();
			BindService<GameSave>();

			FindMonoService<CameraService>();
			FindMonoService<LevelRoot>();

			BindService<LevelFactory>();
			BindService<PlayerFactory>();
			BindService<EnemyFactory>();
		}
	}
}
