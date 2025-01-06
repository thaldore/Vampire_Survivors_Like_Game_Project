<<<<<<< Updated upstream
=======
using System.Collections.Generic;
using Unity.VisualScripting;
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

<<<<<<< Updated upstream
    // Define the states of the game

    public enum GameState
=======
    // define the different state of game
   public enum GameState
>>>>>>> Stashed changes
    {
        Gameplay,
        Paused,
        GameOver
    }

<<<<<<< Updated upstream
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

=======
    // store the current gameState  of the game ;
    public GameState currentState;
    public GameState previousState;


    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    
    [Header("Current Stat Displays")]
    // current stats display
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;


    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public List<Image> chosenWeaponsUI = new List<Image> (6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);


    // flag the check game is over
    public bool isGameOver = false;

    void Awake()
    {
        // warning check too see  if there is another  singleton of this kind in the game
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
        }
        DisableScreens();
    }

    void Update()
    {

     

        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;

            case GameState.Paused:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f; // stop the game entirely
                    Debug.Log("GAME ÝS OVER ");
                    DisplayResults();
                }
                break;

            default:
                Debug.LogWarning("state  does  not exist");
                break;
        }
    }

    // define the method  to change  the state of the game
>>>>>>> Stashed changes
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
<<<<<<< Updated upstream
    public void PauseGame()
    {
        if (currentState != GameState.Paused) 
=======

    public void PauseGame()
    {

        if (currentState != GameState.Paused)
>>>>>>> Stashed changes
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
<<<<<<< Updated upstream
            Debug.Log("Game is paused.");
        }

=======
            Debug.Log("Game is Pasued");
        }
       
>>>>>>> Stashed changes
    }

    public void ResumeGame()
    {
<<<<<<< Updated upstream
        if (currentState != GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game is resumed.");
        }

    }
    
    // Define the method to check for pause and resume input
=======
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f; //resume the game;
            pauseScreen?.SetActive(false);
            Debug.Log("resume the game");
        }
    }

>>>>>>> Stashed changes
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
<<<<<<< Updated upstream
            if (currentState == GameState.Paused)
=======
            if (currentState ==  GameState.Paused)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
        resultsScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }


    public void AssignWeaponsAndPassiveItemsUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if (chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveItemsData.Count!= chosenPassiveItemsUI.Count)
        {
            Debug.Log("WEAPONS DATA OR AND UI OR PASSÝVEITEMS DATA OR UI DÝFFERENT LENGHT");
            return;
        }
        // assign chosen weapon data to chosenWeaponsUI
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            //check that the sprite of the corresponding element in chosenWeaponData

            if (chosenWeaponsData[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {

                //if the sprite is null, disable the  corresponding  element  in chosen WeaponUI
                chosenWeaponsUI[i].enabled = false;
            }
        }




        // assign chosen weapon data to chosenPassiveItemsUI
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            //check that the sprite of the corresponding element in chosenPassiveItemData

            if (chosenWeaponsData[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {

                //if the sprite is null, disable the  corresponding  element  in chosenPassiveItemUI
                chosenPassiveItemsUI[i].enabled = false;
            }
        }

>>>>>>> Stashed changes
    }
}
