using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "MainMenu";
    [SerializeField] private string endSceneName = "Fin";

    // Recommence la scène en cours
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Retourne au menu principal
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(menuSceneName);
    }

    // Charge la scène de fin
    public void LoadEndScene()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(endSceneName);
    }
}