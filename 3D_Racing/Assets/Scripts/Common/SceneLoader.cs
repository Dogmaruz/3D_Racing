using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IDependency<UIPausePanel>
{
    private const string MAIN_MENU_SCENE_TITLE = "Main_menu";

    private UIPausePanel _uiPausePanel;

    public void Construct(UIPausePanel obj)
    {
        _uiPausePanel = obj;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_TITLE);

        _uiPausePanel.UnPause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        _uiPausePanel.UnPause();
    }
}
