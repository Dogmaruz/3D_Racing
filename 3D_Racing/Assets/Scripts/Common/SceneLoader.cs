using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string MAIN_MENU_SCENE_TITLE = "Main_menu";

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_TITLE);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
