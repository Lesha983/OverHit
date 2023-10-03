namespace Chillplay.AppFlow.Scenes
{
    using Core;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Zenject;

    [RequireComponent(typeof(SceneContext))]
    public class StartSceneLoader : MonoBehaviour
    {
        private string initialScene = SceneNames.Loader;

        private SceneContext sceneContext;

        private void Awake()
        {
            sceneContext = GetComponent<SceneContext>();
#if UNITY_EDITOR
            var appRoot = FindObjectOfType<AppRoot>();
            if (appRoot == null)
            {
                SceneManager.LoadScene(initialScene);
                return;
            }
#endif
            
            sceneContext.Run();
        }
    }
}