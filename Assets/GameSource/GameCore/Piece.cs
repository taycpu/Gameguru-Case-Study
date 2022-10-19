using System;
using System.Collections;
using System.Collections.Generic;
using CustomFeatures.Timer;
using UnityEngine;

namespace Prototype.Core
{
    public enum Objects
    {
        Empty,
        Tick,
    }

    public class Piece : MonoBehaviour
    {
        public Collider2D Collider2d => collider2d;
        public GridPosition GridPosition => gridPosition;
        [SerializeField] private GridPosition gridPosition;
        [SerializeField] private Collider2D collider2d;
        [SerializeField] private Objects holdedObject;
        [SerializeField] private Tick tick;

        private Action<GridPosition> OnTickExplode;

        public void SetPosition(int r, int c)
        {
            gridPosition.row = r;
            gridPosition.column = c;
        }
        


        public bool IsEmpty()
        {
            return holdedObject == Objects.Empty;
        }

        public bool HaveTick()
        {
            return holdedObject == Objects.Tick;
        }

        public void PlaceTick()
        {
            holdedObject = Objects.Tick;
            tick.SetTick();
        }


        public void ExplodeTick()
        {
            holdedObject = Objects.Empty;
            tick.UnTick();
            OnTickExplode?.Invoke(GridPosition);
        }
    }
}