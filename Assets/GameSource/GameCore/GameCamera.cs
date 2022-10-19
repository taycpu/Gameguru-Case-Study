using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Core
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private Collider2D[] objects;
        [SerializeField] private Camera cam;
        [SerializeField] private float _buffer;


        private void Update()
        {
            SetCameraSize();
        }

        public void SetCameraSize()
        {
            var (center, size) = Calculate();
            cam.transform.position = center;
            cam.orthographicSize = size;
        }

        private (Vector3 center, float size) Calculate()
        {
            var bounds = new Bounds();

            foreach (var obj in objects)
            {
                bounds.Encapsulate(obj.bounds);
            }

            bounds.Expand(_buffer);
            var vertical = bounds.size.y;
            var horizontal = bounds.size.x * cam.pixelHeight / cam.pixelWidth;

            var size = Mathf.Max(horizontal, vertical) * 0.5f;
            var center = bounds.center + new Vector3(0, 0, -10);

            return (center, size);
        }

        public void InjectObjects(Collider2D[] cols)
        {
            objects = cols;
        }
    }    
}
