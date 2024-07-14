using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private AudioSource sFXPlayer;

    private float MIN_PITCH = 0.9f;//最小音高
    private float MAX_PIYCH = 1.1f;//最大音高

    //以下函数均可使用AudioManager.Instance.xxx调用
    
    //用于播放按钮,玩家重生，死亡等单数音效
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.audioVolume);
    }

    //用于播放复数音效（连续发射的子弹等），采用随机音高的方法
    public void PlayerRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PIYCH);
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.audioVolume);
    }
    
    //从一个音效数组中随机抽取一个音效播放，用于同类音效>=2的情况
    void PlayerRandomSFX(AudioData[] audioDatas)
    {
        PlayerRandomSFX(audioDatas[Random.Range(0, audioDatas.Length)]);
    }

    public void StopSfxPlay()
    {
        sFXPlayer.Stop();
    }
}

[System.Serializable]
public class AudioData
{
    public AudioClip audioClip;
    public float audioVolume;
}
