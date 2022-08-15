using System;
using ScriptableObjects.SettingsVariable;
using UnityEngine;
using UnityEngine.Events;

namespace CustomUnityEvents
{
    [Serializable]
    public class MeshUnityEvent : UnityEvent<Mesh>
    {
    }

    [Serializable]
    public class MaterialUnityEvent : UnityEvent<Material>
    {
    }
    
    [Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {
    }

    [Serializable]
    public class FloatUnityEvent : UnityEvent<float>
    {
    }

    [Serializable]
    public class ArmyEvent : UnityEvent<Army>
    {
    }    
    
    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    {
    }
}