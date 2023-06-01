using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject parchment;
    public List<GameObject> presidents;
    public GameObject winScreen, failScreen, startScreen, finalScreen, BARS;
    public GameObject LevelText, PriceCanvas, collectibleUI, LevelUI, playerBar, enemyBar, avatarCamera, IM, ShopScreen, Skills, TUTscreen, TUTdegrade,
        Shop1,Shop2,Shop3,Shop4, PlayBUTTON;

    public TextMeshPro YearText;
    public TextMeshProUGUI CoinTextWin, CoinTextFail, collectibleText;

    public int CollectibleCount = 0, earnedCollectible;

    public int levelCPI = 0;


    GameManager GM;
    Player PL;


    void Start()
    {
        if (PlayerPrefs.GetInt("Level", 1) == 1)
        {
            parchment.SetActive(true);
            //Time.timeScale = 0;
            //TUTdegrade.SetActive(true);
        }
        //LevelText.transform.GetComponent<TextMeshProUGUI>().text = "level " + PlayerPrefs.GetInt("LevelText", 1);
    }
    void Update()
    {

    }
    public GameObject pauseMenu;
    private bool isPaused = false;

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
    public void WinScreen()
    {

        //int text = CollectibleCount * PL.RampScore;
        //LevelText.gameObject.SetActive(false);

        //collectibleUI.gameObject.SetActive(false);
        //LevelUI.gameObject.SetActive(false);

        winScreen.SetActive(true);

    }
    public void FailScreen()
    {

        //LevelText.gameObject.SetActive(false);

        //collectibleUI.gameObject.SetActive(false);
        //LevelUI.gameObject.SetActive(false);

        failScreen.SetActive(true);
    }
    public void LevelCompleted()
    {

        PlayerPrefs.SetInt("LevelText", PlayerPrefs.GetInt("LevelText", 1) + 1);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);

        if (PlayerPrefs.GetInt("Level") == 6)
        {
            PlayerPrefs.SetInt("Level", 3);
        }


        SceneManager.LoadScene("Game");
    }
    public void LevelFailed()
    {
        SceneManager.LoadScene("Game");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public GameObject gameOVERscreen;
    public List<GameObject> skillUI;

    public GameObject JoystickObjUI;
    public void OverInvoke()
    {
        JoystickObjUI.SetActive(false);
        Invoke(nameof(GameOverScreen), 2);
    }
    public void GameOverScreen()
    {
        gameOVERscreen.SetActive(true);
        foreach(GameObject go in skillUI)
        {
            go.SetActive(false);
        }
    }
}
