using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Prototype.Core
{
    public class Board : MonoBehaviour
    {
        public List<List<Piece>> BoardItems => boardItems;
        private List<List<Piece>> boardItems = new List<List<Piece>>();
        public UnityEvent WhenBombInvokeOnBoard;
        private int row;
        private int column;
        private int wall;


        public void AddRow()
        {
            boardItems.Add(new List<Piece>());
        }

        public void AddColumn(int row, Piece piece)
        {
            boardItems[row].Add(piece);
        }

        public void ClearBoard()
        {
            boardItems.Clear();
            for (int i = 0; i < transform.childCount;)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public void FinishBuilding(int row, int column)
        {
            this.row = row;
            this.column = column;
        }


        public void OnBombExplodeInBoard(GridPosition bombPos)
        {
            WhenBombInvokeOnBoard?.Invoke();
        }
    }
}