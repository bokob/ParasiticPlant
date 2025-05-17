using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SoundManager
{
    GameObject _root;
    AudioSource _bgmSource;
    AudioSource _sfxSource;
    Dictionary<BGM, AudioClip> _bgmDict = new Dictionary<BGM, AudioClip>();
    Dictionary<SFX, AudioClip> _sfxDict = new Dictionary<SFX, AudioClip>();

    public void Init()
    {
        _root = GameObject.Find("@Sound");
        if (_root == null)
        {
            _root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(_root);

            GameObject bgmGO = new GameObject { name = "@BGM" };
            GameObject sfxGO = new GameObject { name = "@SFX" };
            _bgmSource = bgmGO.AddComponent<AudioSource>();
            _bgmSource.loop = true;
            _sfxSource = sfxGO.AddComponent<AudioSource>();
            _sfxSource.playOnAwake = false;
            bgmGO.transform.parent = _root.transform;
            sfxGO.transform.parent = _root.transform;

            // 클립 로드 및 캐싱
            AudioClip[] bgmClips = Managers.Resource.LoadAll<AudioClip>("Sounds/BGM");
            AudioClip[] sfxClips = Managers.Resource.LoadAll<AudioClip>("Sounds/SFX");
            foreach (AudioClip clip in bgmClips)
            {
                if (System.Enum.IsDefined(typeof(BGM), clip.name))
                {
                    BGM bgm = Util.StringToEnum<BGM>(clip.name);
                    _bgmDict.Add(bgm, clip);
                }
            }
            foreach (AudioClip clip in sfxClips)
            {
                if (System.Enum.IsDefined(typeof(BGM), clip.name))
                {
                    SFX sfx = Util.StringToEnum<SFX>(clip.name);
                    _sfxDict.Add(sfx, clip);
                }
            }
        }
    }

    // BGM 재생
    public void PlayBGM(BGM bgm)
    {
        _bgmSource.clip = _bgmDict[bgm];
        _bgmSource.Play();
    }

    // 효과음 재생
    public void PlaySFX(SFX sfx)
    {
        _sfxSource.PlayOneShot(_sfxDict[sfx]);
    }

    public void Stop()
    {
        _bgmSource.Stop();
        _sfxSource.Stop();
    }

    public void Clear()
    {
        _bgmDict.Clear();
        _sfxDict.Clear();
        Object.Destroy(_root);
    }
}