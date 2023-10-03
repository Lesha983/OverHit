namespace Chillplay.AppFlow
{
    using System;
    using System.Collections.Generic;
    using AppFlow.Scenes;
    using Zenject;

    public class GameStartedAwaiter : IInitializable
    {
        [Inject]
        public SignalBus SignalBus { get; set; }

        private readonly List<Action> actions = new List<Action>();

        private bool gameStarted;

        public void Initialize()
        {
            SignalBus.Subscribe<GameSceneLoaded>(OnGameSceneLoaded);
        }

        private void OnGameSceneLoaded()
        {
            gameStarted = true;
            foreach (var action in actions)
            {
                action.Invoke();
            }

            actions.Clear();
        }

        public void ExecuteAfterGameStarted(Action action)
        {
            if (gameStarted)
            {
                action.Invoke();
            }
            else
            {
                actions.Add(action);
            }
        }
    }
}