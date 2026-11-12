using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxPlayer : MonoBehaviour
{
    [Tooltip("Plusieurs sons : un est choisi au hasard à chaque fois")]
    [SerializeField] private AudioClip[] clips;
    [SerializeField, Range(0f, 0.3f)] private float pitchVariation = 0.1f;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Appelable depuis un événement d'animation, un UnityEvent ou un bouton
    public void Play()
    {
        Debug.Log("SfxPlayer.Play appelé");

        if (clips == null || clips.Length == 0) return;

        source.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
    }
}