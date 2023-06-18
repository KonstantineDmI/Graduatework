using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;
    private AudioSource _audioSource;
    private int _soundIndex = 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = sounds[_soundIndex];
        StartCoroutine(WaitBeforeNextMusic());
        _audioSource.Play();
    }

    private void Update()
    {
        ChangeMusic();
    }

    private void ChangeMusic()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Stop();
            StartCoroutine(WaitBeforeNextMusic());
            if (_soundIndex < sounds.Length)
            {
                _soundIndex += 1;
                _audioSource.clip = sounds[_soundIndex];
                _audioSource.Play();
            }
            
            if (_soundIndex > sounds.Length)
            {
                _soundIndex = 0;
            }
        }
    }

    private IEnumerator WaitBeforeNextMusic()
    {
        yield return new WaitForSeconds(1f);
    }
}
