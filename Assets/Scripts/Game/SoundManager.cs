using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public List<GameSoundStruct> sounds;
    public List<GameLoopSoundStruct> loopSounds;

    AudioSource audioSource;
    Dictionary<GameLoopSound, AudioSource> loopSoundSources = new();

    protected override void AwakeNew() {
        audioSource = GetComponent<AudioSource>();
        foreach (GameLoopSoundStruct loopSoundStruct in loopSounds) {
            GameObject gm = new(loopSoundStruct.gameLoopSound.ToString() + "AudioSource");
            gm.transform.SetParent(transform);
            AudioSource source = gm.AddComponent<AudioSource>();
            source.volume = audioSource.volume;
            source.playOnAwake = false;
            source.loop = true;
            source.clip = loopSoundStruct.clip;
            loopSoundSources.Add(loopSoundStruct.gameLoopSound, source);
        }
    }

    public void PlaySound(GameSound gameSound) {
        foreach (GameSoundStruct soundStruct in sounds) {
            if (soundStruct.gameSound == gameSound) {
                audioSource.PlayOneShot(soundStruct.clips.RandomElement());
                break;
            }
        }
    }

    public void StartLoopSound(GameLoopSound gameLoopSound) {
        AudioSource loopSoundSource = loopSoundSources[gameLoopSound];
        if (loopSoundSource != null) loopSoundSource.Play();
    }

    public void StopLoopSound(GameLoopSound gameLoopSound) {
        AudioSource loopSoundSource = loopSoundSources[gameLoopSound];
        if (loopSoundSource != null) loopSoundSource.Stop();
    }

    public void SetLoopSound(GameLoopSound gameLoopSound, bool status) {
        AudioSource loopSoundSource = loopSoundSources[gameLoopSound];
        if (loopSoundSource == null) return;
        if (status && !loopSoundSource.isPlaying) loopSoundSource.Play();
        else if (!status && loopSoundSource.isPlaying) loopSoundSource.Stop();
    }

    [Serializable]
    public class GameSoundStruct {
        public GameSound gameSound;
        public List<AudioClip> clips;
    }

    [Serializable]
    public class GameLoopSoundStruct {
        public GameLoopSound gameLoopSound;
        public AudioClip clip;
    }
}

public enum GameSound {
    Door,
    PickupItem,
    CombineSuccess,
    CombineFail,
    UseItem,
    GhostGiveItem,
}

public enum GameLoopSound {
    Walking,
    Typing,
}
