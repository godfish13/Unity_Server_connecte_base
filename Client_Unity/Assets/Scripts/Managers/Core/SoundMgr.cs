using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> audioClipTable = new Dictionary<string, AudioClip>(); // Effect Sound ĳ�� �ؽ����̺�

    public void init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] SoundNames = System.Enum.GetNames(typeof(Define.Sound));

            for(int i = 0; i < SoundNames.Length - 1; i++)          // MaxCount�� Sound������ �ƴϹǷ� -1
            {
                GameObject go = new GameObject { name = SoundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            /*_audioSources[(int)Define.Sound.Bgm].spatialBlend = 1.0f;   // ���� ��ҵ� ���ٹ� ����
            _audioSources[(int)Define.Sound.Bgm].dopplerLevel = 0.0f;
            _audioSources[(int)Define.Sound.Bgm].rolloffMode = AudioRolloffMode.Linear;
            _audioSources[(int)Define.Sound.Bgm].maxDistance = 30.0f;*/
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        audioClipTable.Clear();
    }

    public void Play(string path, Define.Sound SoundType = Define.Sound.EffectSound,float pitch = 1.0f)     // AudioClip path�޴� ����
    {
        AudioClip audioClip = GetorAddAudioClip(path, SoundType);
        Play(audioClip, SoundType, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound SoundType = Define.Sound.EffectSound, float pitch = 1.0f)    // AudioClip ���� �޴� ����
    {
        if (audioClip == null)
            return;
        if (SoundType == Define.Sound.Bgm)
        {                 
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.EffectSound];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetorAddAudioClip(string path, Define.Sound SoundType = Define.Sound.EffectSound)
    {                                               // �ѹ� ����� AudioClip�� �ؽ����̺� ĳ���صּ� ������ �θ� �� �ٽ� �ε����� �ʵ��� ��
        if (path.Contains("Sounds/") == false)       // �Է��� path�� resource�� Sounds/�� �ȵ��󰡸� ������      
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (SoundType == Define.Sound.Bgm)
        {
            audioClip = Managers.resourceMgr.Load<AudioClip>(path);                 
        }
        else
        {           
            if (audioClipTable.TryGetValue(path, out audioClip) == false)  // TryGetValue : dictionary���� key�� ��ȿ�ϸ� out�� �ش簪 �����ϰ� true��ȯ
            {
                audioClip = Managers.resourceMgr.Load<AudioClip>(path);
                audioClipTable.Add(path, audioClip);
            }       // if���� true�� TryGetValue�� out�� audioClip�� path�� �ش�Ǵ� value�� audioClip�� �����ص����� ���� audioClip�� ���� �������ص� ��
        }

        if (audioClip == null)
            Debug.Log($"missing AudioClip {path}");

        return audioClip;
    }
}
