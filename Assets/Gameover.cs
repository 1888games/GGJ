using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField] CanvasGroup _adoptionNotice;
    [SerializeField] AudioSource _clip;
 
    
    void OnEnable()
    {
		_clip.PlayDelayed (1f);
		Pixelplacement.Tween.CanvasGroupAlpha(_adoptionNotice, 0f, 1f, 1.5f, 0.5f);
    }
}
