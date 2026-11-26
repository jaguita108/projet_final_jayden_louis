using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossAmbience : MonoBehaviour
{
    [Tooltip("Lumière principale de la scène (Directional Light)")]
    [SerializeField] private Light sun;
    [SerializeField] private Color bossLightColor = new Color(0.8f, 0.2f, 0.15f);
    [SerializeField, Range(0f, 2f)] private float bossLightIntensity = 0.6f;
    [SerializeField] private float fadeTime = 2f;
    [SerializeField, Range(0f, 1f)] private float bossMusicVolume = 0.8f;

    private AudioSource bossSource;
    private AudioSource music;
    private float normalMusicVolume;
    private Color normalColor;
    private float normalIntensity;
    private bool started;
    private bool finished;
    private Coroutine running;

    private void Awake()
    {
        bossSource = GetComponent<AudioSource>();
        bossSource.volume = 0f;
    }

    // À appeler quand le joueur entre dans la zone du boss
    public void Activate()
    {
        if (started) return;
        started = true;

        // On retient l'état d'origine pour pouvoir y revenir
        MusicPlayer mp = FindFirstObjectByType<MusicPlayer>();
        music = mp != null ? mp.GetComponent<AudioSource>() : null;
        normalMusicVolume = music != null ? music.volume : 0f;
        normalColor = sun != null ? sun.color : Color.white;
        normalIntensity = sun != null ? sun.intensity : 1f;

        running = StartCoroutine(Fade(true));
    }

    // À appeler à la mort du boss
    public void Restore()
    {
        if (!started || finished) return;
        finished = true;
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool toBoss)
    {
        if (toBoss)
        {
            bossSource.Play();
        }
        else if (music != null)
        {
            music.UnPause();   // la musique du niveau reprend où elle s'était arrêtée
        }

        float fromBoss = bossSource.volume;
        float fromMusic = music != null ? music.volume : 0f;
        Color fromColor = sun != null ? sun.color : Color.white;
        float fromIntensity = sun != null ? sun.intensity : 1f;

        float toBossVolume = toBoss ? bossMusicVolume : 0f;
        float toMusicVolume = toBoss ? 0f : normalMusicVolume;
        Color toColor = toBoss ? bossLightColor : normalColor;
        float toIntensity = toBoss ? bossLightIntensity : normalIntensity;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeTime;
            bossSource.volume = Mathf.Lerp(fromBoss, toBossVolume, t);
            if (music != null) music.volume = Mathf.Lerp(fromMusic, toMusicVolume, t);
            if (sun != null)
            {
                sun.color = Color.Lerp(fromColor, toColor, t);
                sun.intensity = Mathf.Lerp(fromIntensity, toIntensity, t);
            }
            yield return null;
        }

        if (toBoss)
        {
            if (music != null) music.Pause();
        }
        else
        {
            bossSource.Stop();
        }
    }
}