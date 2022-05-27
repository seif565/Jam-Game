using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;
    public static GameStateManager Instance
    {
        get
        {
            if (instance is null)
            {
                instance = new GameStateManager();
            }
            return instance;
        }    
                
    }        
        
    public GameState currentGameState { get; private set; }
    public delegate void GameStateHandler(GameStateManager state);

    public event GameStateHandler OnGameStateChanged;

}
