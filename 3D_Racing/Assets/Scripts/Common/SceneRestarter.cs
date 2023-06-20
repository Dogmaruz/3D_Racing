using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour, IDependency<RaceLockController>
{
    private RaceLockController _raceLookController;
    public void Construct(RaceLockController obj)
    {
        _raceLookController = obj;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            PlayerPrefs.DeleteAll();

            FileHandler.Reset(RaceLockController.filename);

            SceneManager.LoadScene(SceneLoader.MAIN_MENU_SCENE_TITLE);
        }
    }
}
