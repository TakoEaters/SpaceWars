using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Core.ParticlesUtils
{
    public class ParticleEmitter
    {
        private Dictionary<ParticleSystem, ParticleSystem> _emitters
            = new Dictionary<ParticleSystem, ParticleSystem>();

        public static ParticleEmitter Current { get; private set; }
        public static void Initialize() => Current = new ParticleEmitter();

        private ParticleSystem Get(ParticleSystem prefab)
        {
            if (!_emitters.ContainsKey(prefab)) _emitters[prefab] = Object.Instantiate(prefab);
            return _emitters[prefab];
        }

        public void Play(ParticleSystem prefab, Vector3 position)
        {
            var emitter = Get(prefab);
            emitter.transform.position = position;
            emitter.Play();
        }

        public void Play(ParticleSystem prefab, Vector3 position, Quaternion rotation)
        {
            var emitter = Get(prefab);
            var emitterTransform = emitter.transform;
            emitterTransform.position = position;
            emitterTransform.rotation = rotation;
            emitter.Play();
        }
    }
}
