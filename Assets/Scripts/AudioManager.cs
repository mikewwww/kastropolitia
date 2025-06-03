using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;

    private const string VolumeKey = "MusicVolume";
    void Start()
    {
        Debug.Log("🎵 AudioManager started");

        if (musicSource != null && musicSource.clip != null)
        {
            Debug.Log("▶ Playing music: " + musicSource.clip.name);
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("⚠ Δεν έχει δοθεί μουσικό clip ή λείπει το AudioSource.");
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Υπάρχει ήδη άλλο instance
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Φόρτωση αποθηκευμένης έντασης
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        if (musicSource != null)
        {
            musicSource.volume = savedVolume;
        }
    }

    public void ToggleMusic()
    {
        if (musicSource != null)
        {
            musicSource.mute = !musicSource.mute;
        }
    }

    public void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;

            if (volume <= Mathf.Epsilon) // ασφαλής μηδενική τιμή

            {
                if (musicSource.isPlaying)
                    musicSource.Pause(); // Προσωρινή παύση
            }
            else
            {
                if (!musicSource.isPlaying)
                    musicSource.UnPause(); // Ξαναρχίζει τη μουσική
            }

            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    void OnDestroy()
    {
        Debug.LogWarning("⚠ Το AudioManager καταστράφηκε!");
    }

}
