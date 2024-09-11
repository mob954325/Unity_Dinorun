using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Button,
    Jump,
    Land,
    Die
}

public class SoundManager : MonoBehaviour
{
    private AudioSource audioScoure;

    /// <summary>
    /// 실행할 오디오 파일들
    /// </summary>
    public AudioClip[] audioClips;

    private void Awake()
    {
        audioScoure = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 오디오 플레이 함수
    /// </summary>
    /// <param name="type">플레이할 타입</param>
    public void PlayAudio(AudioType type)
    {
        audioScoure.Stop();

        if(audioScoure.clip != audioClips[(int)type])
        {
            audioScoure.clip = audioClips[(int)type];
        }

        audioScoure.Play();
    }

    /// <summary>
    /// 오디오 플레이 함수 (int형)
    /// </summary>
    /// <param name="typeInt">플레이 할 타입의 int형</param>
    public void PlayAudio(int typeInt)
    {
        PlayAudio((AudioType)typeInt);
    }
}
