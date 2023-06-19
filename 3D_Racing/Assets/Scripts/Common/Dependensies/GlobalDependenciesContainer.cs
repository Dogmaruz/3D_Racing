using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependenciesContainer : Dependency
{
    [SerializeField] private GamePause m_gamePause;

    private static GlobalDependenciesContainer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);

            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void BindFoundMonoBehaviour(MonoBehaviour mono)
    {
        Bind<GamePause>(mono, m_gamePause);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectToBind();
    }
}
