using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Parallax 
{
    public class InfiniteParallaxEffect : MonoBehaviour
    {
        [SerializeField] private List<ParallaxPart> _parts;
        [SerializeField] private Transform _target;

        private List<InfiniteParallaxLayer> _layers;

        private float _previousTargetPositionX;

        private void Start()
        {
            _previousTargetPositionX = _target.position.x;
            _layers = new List<InfiniteParallaxLayer>();
            foreach (var part in _parts)
            {
                Transform layerParent = new GameObject().transform;
                layerParent.transform.parent = transform;
                part.SpriteRenderer.transform.parent = layerParent;
                InfiniteParallaxLayer infiniteParallaxLayer = new InfiniteParallaxLayer(part.SpriteRenderer, part.Speed, layerParent);
                _layers.Add(infiniteParallaxLayer);
            }
        }

        private void Update()
        {
            
            foreach (var layer in _layers) 
                layer.UpdateLayer(_previousTargetPositionX, _target.position.x,  _target.position.y);

            _previousTargetPositionX = _target.position.x;
        }

        [Serializable]
        private class ParallaxPart
        {
            [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
            [field: SerializeField] public float Speed { get; private set; }
        }
    }
}