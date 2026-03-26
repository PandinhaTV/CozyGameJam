using UnityEngine;
using UnityEngine.SceneManagement;

public class MMButtons : MonoBehaviour
{
    [SerializeField] private AudioSource buttonsAudioSource;
    [SerializeField] private AudioClip buttonInteracAudioClip;

    public void StartButton()
    {
        if (buttonsAudioSource != null && buttonInteracAudioClip != null)
        {
            buttonsAudioSource.PlayOneShot(buttonInteracAudioClip);
        }

        SceneManager.LoadScene("Level1");
        Debug.Log("Changing to Level1 Scene...");
    }
    
    public void QuitButton()
    {
        if (buttonsAudioSource != null && buttonInteracAudioClip != null)
        {
            buttonsAudioSource.PlayOneShot(buttonInteracAudioClip);
        }

        Application.Quit();
        Debug.Log("Game is exiting...");
    }
}
