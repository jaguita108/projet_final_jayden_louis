using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UISound : MonoBehaviour
{
    private static UISound instance;

    [SerializeField] private AudioClip clickClip;
    private AudioSource source;

    private void Awake()
    {
        // Un seul exemplaire pour tout le jeu, qui survit aux changements de scène
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
    }

    public static void Click()
    {
        if (instance != null && instance.clickClip != null)
            instance.source.PlayOneShot(instance.clickClip);
    }
}