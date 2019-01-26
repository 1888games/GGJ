using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public static Action<Transform> OnEmitted = delegate {  };

    [Button("Emit")]
    void Emit()
    {
        OnEmitted(transform);
    }
}
