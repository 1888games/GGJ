
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField] CanvasGroup _adoptionNotice;
    void OnEnable()
    {
        Pixelplacement.Tween.CanvasGroupAlpha(_adoptionNotice, 0f, 1f, 1.5f, 0.5f);
    }
}
