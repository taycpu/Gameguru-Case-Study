using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Core
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private BoardManager boardManager;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (RayScreen(out var gridPos))
                {
                    PlaceTick(gridPos);
                }
            }
        }
        

        private bool RayScreen(out Piece piece)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            piece = null;
            List<RaycastHit2D> result = new List<RaycastHit2D>();
            if (Physics2D.Raycast(ray.origin, ray.direction, contactFilter, result) > 0)
            {
                var hittedPiece = result[0].transform.GetComponent<Piece>();
                piece = hittedPiece;
                return true;
            }

            return false;
        }

        private void PlaceTick(Piece piece)
        {
            if (piece.IsEmpty())
            {
                piece.PlaceTick();
                boardManager.CheckMatch(piece.GridPosition);
            }
        }
    }
}