using CodeBase.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.Settings
{
    internal class SoundService : MonoBehaviour, IService
    {
        [SerializeField] private AudioMixerGroup mixer;

        [SerializeField] private AudioSource sounds;
        [SerializeField] private AudioSource bMusic;
        [SerializeField] private AudioSource music;

        [SerializeField] private AudioClip bgMusic;
        [SerializeField] private AudioClip winMusic;
        [SerializeField] private AudioClip loseMusic;

        [SerializeField] private AudioClip effectOn;
        [SerializeField] private AudioClip effectOff;
        [SerializeField] private AudioClip btnClick;

        [field: SerializeField] internal bool DefaultSound { get; private set; }
        [field: SerializeField] internal bool DefaultMusic { get; private set; }

        public IEnumerator Initialize()
        {
            yield return null;
        }

        public void MusicChange(bool value)
        {
            if (value) mixer.audioMixer.SetFloat("Music", 0);
            else mixer.audioMixer.SetFloat("Music", -80);
        }

        public void SoundChange(bool value)
        {
            if (value) mixer.audioMixer.SetFloat("Sound", 0);
            else mixer.audioMixer.SetFloat("Sound", -80);
        }

        public void PlaySoundBtnClick()
        {
            sounds?.Stop();
            sounds.clip = btnClick;
            sounds?.Play();
        }
        public void PlaySoundOnOff(bool value)
        {
            sounds.Stop();
            if (value) sounds.clip = effectOn;
            else sounds.clip = effectOff;
            sounds.Play();
        }
        public void PlayMusicBG()
        {
            bMusic.Stop();
            bMusic.clip = bgMusic;
            bMusic.Play();
        }
        public void PlayMusicResult(bool value)
        {
            music.Stop();
            if (value) music.clip = winMusic;
            else music.clip = loseMusic;
            music.Play();
        }
    }
}
