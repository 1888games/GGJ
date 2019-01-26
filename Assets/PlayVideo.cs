using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
   //public UnityEngine.Video.VideoClip videoClip;

    void Start()
    {
		var videoPlayer = GetComponent<VideoPlayer> ();
        var audioSource = gameObject.AddComponent<AudioSource>();

		videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

		videoPlayer.Play ();
    }
    
    
}
