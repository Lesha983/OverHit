namespace Chillplay.AppFlow.Scenes
{
    using System.Collections;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Utils.Coroutine;
    using Zenject;
    using Zenject.Extensions.Commands;

    public abstract class SwitchSceneCommand : LockableCommand
    {
        [Inject] public ICoroutineProvider CoroutineProvider { get; set; }

        protected abstract string Scene { get; }

        protected virtual bool BlockExecution => true;

        public override void Execute()
        {
            CoroutineProvider.StartCoroutine(Load());

            if (BlockExecution)
                Lock();
        }

        private IEnumerator Load()
        {
            var scenes = Enumerable.Range(0, SceneManager.sceneCount).Select(SceneManager.GetSceneAt);

            foreach (var scene in scenes)
            {
                if (scene.name == SceneNames.Loader)
                {
                    continue;
                }

                yield return SceneManager.UnloadSceneAsync(scene);
            }

            AsyncOperation waitScene = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Additive);

            while (!waitScene.isDone)
            {
                yield return null;
            }

#if UNITY_EDITOR
            yield return new WaitForSeconds(0.5f);
#endif

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scene));

            Unlock();
        }
    }
}