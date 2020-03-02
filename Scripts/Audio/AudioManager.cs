using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    [System.Serializable]
    private struct Clip
    {
        public string name;
        public AudioClip clip;

        public Clip(string pName, AudioClip pClip)
        {
            name = pName;
            clip = pClip;
        }
    }

    private AudioSource[] sounds = new AudioSource[3];
    private AudioSource music = null;

    [SerializeField] private Clip[] clips = new Clip[0];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i] = gameObject.AddComponent<AudioSource>();
        }

        music = gameObject.AddComponent<AudioSource>();
    }


    private AudioClip GetByName(string name)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name.Equals(name))
            {
                return clips[i].clip;
            }
        }

        return null;
    }

    public void PlaySound(string name)
    {
        AudioClip clip = GetByName(name);

        if (clip != null)
        {
            bool played = false;

            for (int i = 0; i < sounds.Length; i++)
            {
                if (!sounds[i].isPlaying)
                {
                    sounds[i].clip = clip;
                    sounds[i].pitch = 1.0f;
                    sounds[i].Play();

                    played = true;
                }
            }

            if (!played)
            {
                sounds[0].Stop();
                sounds[0].clip = clip;
                sounds[0].pitch = 1.0f;
                sounds[0].Play();
            }
        }
    }

    public void PlayMusic(string name, bool loop)
    {
        AudioClip clip = GetByName(name);

        if (clip != null && clip != music.clip)
        {
            music.Stop();
            music.clip = clip;
            music.loop = loop;
            music.Play();
        }
    }

    public void SwitchMuteSound()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].mute = !sounds[i].mute;
        }
    }

    public void SwitchMuteMusic()
    {
        music.mute = !music.mute;
    }
}
