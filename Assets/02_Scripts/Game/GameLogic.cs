using System;
using UnityEngine;

public class GameLogic
{
    public BlockController blockController;
    
    private Constants.PlayerType[,] _board;

    public BasePlayerState firstPlayerState;
    public BasePlayerState secontPlayerState;
    
    public enum GameResult { None, Win, Lose, Draw }
    
    private BasePlayerState _currentPlayerState;
    
    public GameLogic(BlockController blockController, Constants.GameType gameType)
    {
        this.blockController = blockController;

        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        switch (gameType)
        {
            case Constants.GameType.singlePlay:
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secontPlayerState = new PlayerState(false);
                
                SetState(firstPlayerState);
                break;
            case Constants.GameType.Multiplay:
                break;
        }
    }

    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);
    }

    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        if (_board[row, col] != Constants.PlayerType.None) return false;

        if (playerType == Constants.PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        else if (playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }
        return false;
    }

    public void EndGame(GameResult gameResult)
    {
        SetState(null);
        firstPlayerState = null;
        secontPlayerState = null;
        
        Debug.Log("### Game Over ###");
    }
    public GameResult CheckGameResult()
    {
        if (CheckGameWin(Constants.PlayerType.PlayerA, _board)) return GameResult.Win;

        if (CheckGameWin(Constants.PlayerType.PlayerB, _board)) return GameResult.Lose;
        
        if (CheckGameDraw(_board)) return GameResult.Draw;
        
        return GameResult.None;
    }

    public bool CheckGameDraw(Constants.PlayerType[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i,j] == Constants.PlayerType.None) return false;
            }
        }
        return true;
    }

    private bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i,0] == playerType && board[i,1] == playerType && board[i,2] == playerType)
            { 
                return true;   
            }
            if (board[0,i] == playerType && board[1,i] == playerType && board[2,i] == playerType)
            { 
                return true;   
            }
        }
        if (board[0,0] == playerType && board[1,1] == playerType && board[2,2] == playerType) return true;
        if (board[0,2] == playerType && board[1,1] == playerType && board[2,0] == playerType) return true;
        
        return false;
    }
}
