using Lean.Pool;
using ScriptableObjects.DataContainer;
using UnityEngine;

namespace Units.Effect
{
    public class ExplosionEffect : MonoBehaviour {
        private Material _pieceMaterial;
        private Mesh _mesh;
        private float _meshScale = 1;
        
        [SerializeField] 
        private GameObject particlePrefab;
        
        [SerializeField] 
        private FloatVariable explodeEffectBurstCount;
        
        public void Explode()
        {
            var unitTransform = transform;
            Vector3 spawnOffset = unitTransform.position + new Vector3(0, _meshScale / 2, 0);
            ParticleSystem explodeEffect = LeanPool.Spawn(particlePrefab, spawnOffset, unitTransform.rotation).GetComponent<ParticleSystem>();
            
            //Set Particle System shape settings
            var shapeModule = explodeEffect.shape;
            shapeModule.mesh = _mesh;
            shapeModule.scale = new Vector3(_meshScale, _meshScale, _meshScale);
            
            //Set Particle System emission settings
            var emissionModule = explodeEffect.emission;
            emissionModule.SetBurst(0, new ParticleSystem.Burst(0, explodeEffectBurstCount.value * _meshScale));
            explodeEffect.Play();
            
            //Set Particle System renderer settings
            ParticleSystemRenderer render = explodeEffect.GetComponent<ParticleSystemRenderer>();
            render.mesh = _mesh;
            render.sharedMaterial = _pieceMaterial;
        }

        public void ChangeMesh(Mesh mesh)
        {
            _mesh = mesh;
        }

        public void ChangeParticleMaterial(Material mat)
        {
            _pieceMaterial = mat;
        }

        public void ChangeShapeSize(float scale)
        {
            _meshScale = scale;
        }
    }
}
