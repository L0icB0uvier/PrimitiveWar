using System;
using Enums;
using UnityEngine;

namespace Units.Modifiers
{
    public abstract class Modifier : ScriptableObject
    {
        [Serializable]
        public struct Mod
        {
            public ECharacteristics chara;
            public int value;
        }

        public Mod[] characteristicModifiers = Array.Empty<Mod>();
        
        //Apply characteristic modifier to units
        public virtual void ApplyModifier(Unit target)
        {
            foreach (var mod in characteristicModifiers)
            {
                target.UpdateCharacteristic(mod.chara, mod.value);
            }
        }
    }
}