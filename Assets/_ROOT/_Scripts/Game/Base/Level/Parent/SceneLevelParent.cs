namespace Chillplay.OverHit.Base.LevelLoading
{
    using Cysharp.Threading.Tasks;
    using OverHit.Level;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLevelParent : LevelInstanceParent<SceneLevelConfig>
    {
        private Scene initialActiveScene;
        private Scene sceneInstance;

        protected override void Start()
        {
            base.Start();
            initialActiveScene = gameObject.scene;
        }

        protected override async UniTask SetLevel(SceneLevelConfig levelConfig)
        {
            await TryCleanUp();
            var sceneName = levelConfig.LevelId;
            await LoadScene(sceneName);
            await Resources.UnloadUnusedAssets();
            SetActiveScene(sceneName);
            FindArea();
        }

        private async UniTask LoadScene(string sceneName) => 
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        private void SetActiveScene(string sceneName)
        {
            sceneInstance = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(sceneInstance);
        }

        private void FindArea()
        {
            Level = FindObjectOfType<LevelArea>();
        }

        public override async UniTask TryCleanUp()
        {
            if (Level == null)
            {
                return;
            }

            await UnloadScene();
            SceneManager.SetActiveScene(initialActiveScene);
        }
        
        private async UniTask UnloadScene() => 
            await SceneManager.UnloadSceneAsync(sceneInstance);
    }
}