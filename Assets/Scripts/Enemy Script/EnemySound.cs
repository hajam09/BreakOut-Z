using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
	private AudioSource audio_source;

	[SerializeField]
    private AudioClip screaming_sound, dying_sound;

    [SerializeField]
    private AudioClip[] attacking_sound;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Screaming_Sound(){
        audio_source.clip = screaming_sound;
        audio_source.Play();
    }

    public void Attacking_Sound(){
        audio_source.clip = attacking_sound[Random.Range(0, attacking_sound.Length)];
        audio_source.Play();
    }

    public void Dying_Sound(){
        audio_source.clip = dying_sound;
        audio_source.Play();
    }
}
