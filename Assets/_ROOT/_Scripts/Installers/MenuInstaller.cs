using ChillPlay.OverHit.Service;

namespace ChillPlay.OverHit.Installer
{
	public class MenuInstaller : Installer
	{
		public override void InstallBindings()
		{
			foreach (var so in settingsObjects)
				BindSettings(so);

			BindService<GameState>();
			BindService<GameSave>();
		}
	}
}
