using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] playlist;  // Lista de canciones
    private AudioSource audioSource;
    private int currentTrackIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Selecciona una canción aleatoria al inicio
        currentTrackIndex = Random.Range(0, playlist.Length);
        PlayTrack(currentTrackIndex);

    }

    void Update()
    {
        // Verifica si la canción actual ha terminado y pasa a la siguiente
        if (!audioSource.isPlaying && !audioSource.mute)
        {
            NextTrack();
        }
    }

    public void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % playlist.Length;
        PlayTrack(currentTrackIndex);
    }

    public void PreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + playlist.Length) % playlist.Length;
        PlayTrack(currentTrackIndex);
    }

    private void PlayTrack(int index)
    {
        if (playlist.Length > 0)
        {
            audioSource.clip = playlist[index];
            audioSource.Play();
        }
    }

    // Función de mute
    public void ToggleMute()
    {
        audioSource.mute = !audioSource.mute;
    }
}