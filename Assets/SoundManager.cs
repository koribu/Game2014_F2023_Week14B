using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    List<AudioSource> _channels;
    List<AudioClip> _clips;

    // Start is called before the first frame update
    void Awake()
    {
        _channels = GetComponents<AudioSource>().ToList();

        _clips = new List<AudioClip>();
        //Sound effects
        _clips.Add(Resources.Load<AudioClip>("Audios/Player_Jump_SFX"));
        _clips.Add(Resources.Load<AudioClip>("Audios/Player_Hurt_SFX"));
        _clips.Add(Resources.Load<AudioClip>("Audios/Player_Dying_SFX"));

        //Musics
        _clips.Add(Resources.Load<AudioClip>("Audios/Main_Music"));
        _clips.Add(Resources.Load<AudioClip>("Audios/GameOver_Music"));
    }
    
    public void PlaySound(Channel channel,Sound sound)
    {
        _channels[(int)channel].clip = _clips[(int)sound];
        _channels[(int)channel].Play();
    }

    public void PlayMusic(Sound sound)
    {
        _channels[(int)Channel.MUSIC_CHANNEL].clip = _clips[(int)sound];
        _channels[(int)Channel.MUSIC_CHANNEL].volume = .25f;
        _channels[(int)Channel.MUSIC_CHANNEL].loop = true;

        _channels[(int)Channel.MUSIC_CHANNEL].Play();
    }
}
