using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ToolAndTargetUI : MonoBehaviour
{
    [SerializeField] RectTransform _toast;
    [SerializeField] TextMeshProUGUI _stateText;
    [SerializeField] string _testToolString;
    [SerializeField] string _testTargetString;
    
    void Awake()
    {
        // listen to tools and targets state changes
    }
    
    void UpdateUI()
    {
        
    }

    [Button("test pickup")]
    void TestPickup()
    {
        _stateText.text = String.Format("Picked up a {0}", _testToolString);
        Pixelplacement.Tween.AnchoredPosition(_toast, new Vector2(0, 100), Vector2.zero, 1f, 0f);
    }
}
