using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MMBoxDuck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image boxDuckImage;

    [SerializeField] private Sprite closedBoxSprite;
    [SerializeField] private Sprite openedBoxSprite;

    [SerializeField] private AudioSource boxDuckAudioSource;
    [SerializeField] private AudioClip quackAudioClip;

    void Start()
    {
        boxDuckImage.sprite = closedBoxSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        boxDuckImage.sprite = openedBoxSprite;

        if (boxDuckAudioSource != null && quackAudioClip != null)
        {
            boxDuckAudioSource.pitch = Random.Range(0.7f, 1.3f);
            boxDuckAudioSource.PlayOneShot(quackAudioClip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        boxDuckImage.sprite = closedBoxSprite;
    }
}