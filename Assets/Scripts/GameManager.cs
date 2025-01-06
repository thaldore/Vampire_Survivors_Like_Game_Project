using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // Define the states of the game

    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    // Store the current state of the game
    public GameState currentState;
    // Store the previous state of the game
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;

    //Current Stat Displays
    public Text CurrentHealthDisplay;
    public Text CurrentRecoveryDisplay;
    public Text CurrentMoveSpeedDisplay;
    public Text CurrentMightDisplay;
    public Text CurrentProjectileSpeedDisplay;
    public Text CurrentMagnetDisplay;

    void Awake()
    {
        //Warning check to see if there's another singleton
        if (instance == null)
        { instance = this; }
        else
        {
            Debug.LogWarning("EXTRA" + this + " DELETED");
            Destroy(gameObject);
            DisableScreens();
        }

        void Update()
        {
            // Define the behaviour for each state

            switch (currentState)
            {
                case GameState.Gameplay:
                    // Gameplay codes
                    CheckForPauseAndResume();
                    break;

                case GameState.Paused:
                    // Pause codes
                    CheckForPauseAndResume();
                    break;

                case GameState.GameOver:
                    // Game over codes
                    break;

                default:
                    Debug.LogWarning("STATE DOES NOT EXIST");
                    break;
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
    public void PauseGame()
    {
        if (currentState != GameState.Paused) 
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Game is paused.");
        }

    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game is resumed.");
        }

    }
    
    // Define the method to check for pause and resume input
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
    }
}
