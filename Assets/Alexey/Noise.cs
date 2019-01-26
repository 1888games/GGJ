using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public static Action<Transform> OnEmitted = delegate {  };

    void Awake()
    {
        OnEmitted(transform);
    }

    [Button("Emit")]
    void Emit()
    {
        OnEmitted(transform);
    }
}
