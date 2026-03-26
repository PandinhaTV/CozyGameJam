using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayMenuManager : MonoBehaviour
{
    public GameObject gameplayUI;
    public bool gameplayUIActive = true;

    public GameObject pauseUI;
    public bool pauseUIActive = false;

    public GameObject shopUI;
    public bool shopUIActive = false;

    public void Start()
    {
        gameplayUI.SetActive(true);
        gameplayUIActive = true;

        pauseUI.SetActive(false);
        pauseUIActive = false;

        shopUI.SetActive(false);
        shopUIActive = false;
    }

    public void OpenCloseShop()
    {
        if (gameplayUIActive && !shopUIActive)
        {
            gameplayUI.SetActive(false);
            gameplayUIActive = false;

            shopUI.SetActive(true);
            shopUIActive = true;

            pauseUI.SetActive(false);
            pauseUIActive = false;
        }
        else if (!gameplayUIActive && shopUIActive)
        {
            gameplayUI.SetActive(true);
            gameplayUIActive = true;

            shopUI.SetActive(false);
            shopUIActive = false;

            pauseUI.SetActive(false);
            pauseUIActive = false;
        }

    }

    public void OpenClosePause()
    {
        if (gameplayUIActive && !pauseUIActive)
        {
            gameplayUI.SetActive(false);
            gameplayUIActive = false;

            shopUI.SetActive(false);
            shopUIActive = false;

            pauseUI.SetActive(true);
            pauseUIActive = true;

            Time.timeScale = 0;
        }
        else if (!gameplayUIActive && pauseUIActive)
        {
            gameplayUI.SetActive(true);
            gameplayUIActive = true;

            shopUI.SetActive(false);
            shopUIActive = false;

            pauseUI.SetActive(false);
            pauseUIActive = false;

            Time.timeScale = 1;
        }

    }
    
    public void QuitToMM()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Changing to MainMenu Scene...");
    }
}
