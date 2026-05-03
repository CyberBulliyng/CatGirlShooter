using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Tracks")]
    public AudioClip[] tracks;
    public bool shuffle = false;

    AudioSource audioSource;
    int currentIndex = 0;
    bool paused = false; // флаг паузы

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        PlayTrack(currentIndex);
    }

    void Update()
    {
        // Не переключаем если на паузе
        if (paused) return;

        if (!audioSource.isPlaying)
            PlayNext();
    }

    void PlayTrack(int index)
    {
        if (tracks.Length == 0) return;
        audioSource.clip = tracks[index];
        audioSource.Play();
    }

    void PlayNext()
    {
        currentIndex = shuffle
            ? Random.Range(0, tracks.Length)
            : (currentIndex + 1) % tracks.Length;

        PlayTrack(currentIndex);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) { audioSource.UnPause(); paused = false; }
        else { audioSource.Pause(); paused = true; }
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused) { audioSource.Pause(); paused = true; }
        else { audioSource.UnPause(); paused = false; }
    }
}