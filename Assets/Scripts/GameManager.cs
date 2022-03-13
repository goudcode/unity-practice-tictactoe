using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum IndicationType
{
    None,
    X,
    O
}

public class GameManager : MonoBehaviour
{
    [SerializeField] IndicationType currentPlayer;
    [SerializeField] List<BoardSpace> boardSpaces = new List<BoardSpace>();

    public IndicationType CurrentPlayer()
    {
        return currentPlayer;
    }

    private void Awake()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        boardSpaces.ForEach(space => space.Reset());
        currentPlayer = IndicationType.X;
    }

    private bool GameFinished(out IndicationType winner)
    {
        foreach (var x in Enumerable.Range(0, 2))
        {
            var y = x * 3;

            // Horizontal Lines
            if (
                boardSpaces[y].Type != IndicationType.None &&
                boardSpaces[y].Type == boardSpaces[y + 1].Type && 
                boardSpaces[y + 1].Type == boardSpaces[y + 2].Type)
            {
                winner = boardSpaces[y].Type;
                return true;
            }

            // Vertical Lines
            if (boardSpaces[x].Type != IndicationType.None &&
                boardSpaces[x].Type == boardSpaces[x + 3].Type &&
                boardSpaces[x + 3].Type == boardSpaces[x + 6].Type)
            {
                winner = boardSpaces[x].Type;
                return true;
            }
        }

        // Diagonal top left => bottom right
        if (boardSpaces[0].Type != IndicationType.None &&
            boardSpaces[0].Type == boardSpaces[4].Type &&
            boardSpaces[4].Type == boardSpaces[8].Type)
        {
            winner = boardSpaces[0].Type;
            return true;
        }

        // Diagonal top right => bottom left
        if (boardSpaces[2].Type != IndicationType.None &&
            boardSpaces[2].Type == boardSpaces[4].Type &&
            boardSpaces[4].Type == boardSpaces[6].Type)
        {
            winner = boardSpaces[2].Type;
            return true;
        }

        // No spaces left
        if (!boardSpaces.Where(space => space.Type == IndicationType.None).Any())
        {
            winner = IndicationType.None;
            return true;
        }

        winner = IndicationType.None;
        return false;
    }

    public void ReportInteraction(BoardSpace boardSpace)
    {
        boardSpace.ChangeValue(currentPlayer);
        currentPlayer = currentPlayer == IndicationType.X ? IndicationType.O : IndicationType.X;

        if (GameFinished(out var winner))
        {
            Debug.Log($"Player {winner} won");
            ResetGame();
        }
    }
}
