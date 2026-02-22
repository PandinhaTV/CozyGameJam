using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CropAudio : MonoBehaviour
{
    public static CropAudio instance;

    [Header("Audio")]
    [SerializeField] private AudioSource cropAudioSource;
    [SerializeField] private List<AudioClip> grabbedClips;
    [SerializeField] private List<AudioClip> runClips;

    [Header("Cooldown Settings")]
    [SerializeField] private float runCooldown = 10f;
    [SerializeField] private float grabbedCooldown = 10f;
    [SerializeField] private float nextRunTime = 0f;
    [SerializeField] private float nextGrabbedTime = 0f;

    private void Awake()
    {
        instance = this;
        cropAudioSource = GetComponent<AudioSource>();
    }

    public void OnGrab()
    {
        TryPlayGrabbed();
    }
    public void OnRelease()
    {
        TryPlayRunAudio();
    }

    private void TryPlayRunAudio()
    {
        if (Time.time < nextRunTime)
            return;

        if (runClips == null || runClips.Count == 0)
            return;

        PlayRandomFromList(runClips);

        nextRunTime = Time.time + runCooldown;
    }

    private void TryPlayGrabbed()
    {
        if (Time.time < nextGrabbedTime)
            return;

        if (grabbedClips == null || grabbedClips.Count == 0)
            return;

        PlayRandomFromList(grabbedClips);

        nextGrabbedTime = Time.time + grabbedCooldown;
    }

    private void PlayRandomFromList(List<AudioClip> list)
    {
        AudioClip clip = list[Random.Range(0, list.Count)];
        cropAudioSource.PlayOneShot(clip);
    }

}
