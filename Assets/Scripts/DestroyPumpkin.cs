using UnityEngine;
using System;

public class Pumpkin : MonoBehaviour
{
    public event Action OnDestroyed;

    void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
