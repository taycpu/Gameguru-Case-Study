using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Prototype.Core
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private Piece piecePrefab;
        [SerializeField] private Transform startPos;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Board board;
        [SerializeField] private GameCamera gameCam;

        [SerializeField] private List<GridPosition> total;
        [SerializeField] private BonusUI bonusUI;


        private int rowSize, columnSize;
        private int totalBonus;

        private void Start()
        {
            SetBoardSettings("10");
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            GenerateBoard();
            SetCam();
            board.FinishBuilding(rowSize, columnSize);
        }

        public void SetBoardSettings(string row = "10")
        {
            if (int.TryParse(row, out int rowResult))
            {
                rowSize = rowResult;
                columnSize = rowResult;
                print("Row result=" + rowResult);
            }
        }


        [ContextMenu("Generate Board")]
        public void GenerateBoard()
        {
            ClearBoard();
            for (int i = 0; i < rowSize; i++)
            {
                board.AddRow();
                for (int j = 0; j < columnSize; j++)
                {
                    var piece = Instantiate(piecePrefab, board.transform);
                    piece.transform.position = startPos.position + new Vector3(i * offset.x, j * offset.y, 0);
                    piece.SetPosition(i, j);
                    board.AddColumn(i, piece);
                }
            }
        }

        [ContextMenu("Clear Board")]
        public void ClearBoard()
        {
            board.ClearBoard();
        }

        private void SetCam()
        {
            var lastRow = board.BoardItems.Count - 1;
            var lastCell = board.BoardItems[lastRow].Count - 1;


            Collider2D[] cols = new Collider2D[]
            {
                board.BoardItems[0][0].Collider2d,
                board.BoardItems[lastRow][lastCell].Collider2d,
            };
            gameCam.InjectObjects(cols);
        }

        #region Matching

        public void CheckMatch(GridPosition gridPosition)
        {
            IterativeCheckRow(gridPosition);

            for (int i = 0; i < total.Count; i++)
            {
                IterativeCheckColumn(new GridPosition(total[i].row, gridPosition.column));
            }

            for (int i = 0; i < total.Count; i++)
            {
                IterativeCheckRow(new GridPosition(total[i].row, total[i].column));
            }


            if (total.Count >= 3)
            {
                for (int i = 0; i < total.Count; i++)
                {
                    board.BoardItems[total[i].row][total[i].column].ExplodeTick();
                }

                totalBonus++;
            }


            total.Clear();
            bonusUI.SetBonusText(totalBonus);
        }

        private void IterativeCheckRow(GridPosition gridPosition)
        {
            int rowIndex = gridPosition.row;
            bool isNextChecked = rowIndex < rowSize && board.BoardItems[rowIndex][gridPosition.column].HaveTick();

            while (isNextChecked)
            {
                var piece = new GridPosition(rowIndex, gridPosition.column);

                if (!total.Contains(piece))
                    total.Add(piece);

                rowIndex++;
                isNextChecked = rowIndex < rowSize && board.BoardItems[rowIndex][gridPosition.column].HaveTick();
            }

            rowIndex = gridPosition.row - 1;
            bool isBackChecked = rowIndex >= 0 && board.BoardItems[rowIndex][gridPosition.column].HaveTick();

            while (isBackChecked)
            {
                var piece = new GridPosition(rowIndex, gridPosition.column);

                if (!total.Contains(piece))
                    total.Add(piece);
                rowIndex--;
                isBackChecked = rowIndex >= 0 && board.BoardItems[rowIndex][gridPosition.column].HaveTick();
            }
        }

        private void IterativeCheckColumn(GridPosition gridPosition)
        {
            int columnIndex = gridPosition.column + 1;
            bool isNextChecked = columnIndex < columnSize && board.BoardItems[gridPosition.row][columnIndex].HaveTick();

            while (isNextChecked)
            {
                var piece = new GridPosition(gridPosition.row, columnIndex);

                if (!total.Contains(piece))
                    total.Add(piece);
                columnIndex++;
                isNextChecked = columnIndex < columnSize && board.BoardItems[gridPosition.row][columnIndex].HaveTick();
            }

            columnIndex = gridPosition.column - 1;
            bool isBackChecked = columnIndex >= 0 && board.BoardItems[gridPosition.row][columnIndex].HaveTick();

            while (isBackChecked)
            {
                var piece = new GridPosition(gridPosition.row, columnIndex);

                if (!total.Contains(piece))
                    total.Add(piece);
                columnIndex--;
                isBackChecked = columnIndex >= 0 && board.BoardItems[gridPosition.row][columnIndex].HaveTick();
            }
        }

        #endregion
    }
}