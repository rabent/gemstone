using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audiomanager : MonoBehaviour
{
    public static audiomanager instance;
    public AudioMixer mixer;
    [Header("#BGM")] //BGM�±�
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")] //SFX�±�
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win }

    void Awake()
    {
        instance = this;
        Init();
    }
    void Init()
    {
        //����� �÷��̾� �ʱ�ȭ, �ϳ�
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        //bgmPlayer.PlayOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        AudioMixerGroup[] audiomix=mixer.FindMatchingGroups("BGM");
        bgmPlayer.outputAudioMixerGroup=audiomix[0];

        audiomanager.instance.PlayBgm(true);

        //ȿ���� �÷��̾� �ʱ�ȭ, ä�� ������ŭ ������
        GameObject sfxObject = new GameObject("BgmPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index=0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
            AudioMixerGroup[] mix=mixer.FindMatchingGroups("SFX");
            sfxPlayers[index].outputAudioMixerGroup=mix[0];

        }
    }

    public void setmaster(float sliderval) {
        mixer.SetFloat("MASTER", Mathf.Log10(sliderval)*20);
    }
    public void setbgm(float sliderval) {
        mixer.SetFloat("BGM", Mathf.Log10(sliderval)*20);
    }
    public void setsfx(float sliderval) {
        mixer.SetFloat("SFX", Mathf.Log10(sliderval)*20);
    }

    public void PlayBgm(bool isPlay) //������� ���
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        //ä�� ������ŭ ��ȸ �ϸ鼭 ã��
        for (int index=0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue; //�̹� �÷������̶�� �ǳʶٱ�

            int ranIndex = 0;
            if(sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
