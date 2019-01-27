using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVController : MonoBehaviourSingleton<TVController>
{
	//public UnityEngine.Video.VideoClip videoClip;

	bool tvOn = false;
	public AudioSource music;

	VideoPlayer videoPlayer;

	

    void Start()
    {
		videoPlayer = GetComponent<VideoPlayer> ();
        //var audioSource = gameObject.AddComponent<AudioSource>();

		videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";
		//videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
		//videoPlayer.SetTargetAudioSource(0, audioSource);

		music.playOnAwake = false;
		music.Stop ();

		//videoPlayer.Play ();
    }

	public bool ToggleTV () {

		tvOn = !tvOn;

		if (tvOn) {
			videoPlayer.Play ();
			music.Play ();
		} else {
			videoPlayer.Stop ();
			music.Stop ();
		}

		return tvOn;
		
	}
    
}
