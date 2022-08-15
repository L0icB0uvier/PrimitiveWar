using Lean.Pool;
using UnityEngine;

namespace Units.Effect
{
    public class DespawnOnParticleFinishedPlaying : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_particleSystem.IsAlive() == false)
            {
                LeanPool.Despawn(gameObject);
            }
        }
    }
}
