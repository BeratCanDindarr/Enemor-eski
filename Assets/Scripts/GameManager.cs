using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool canMove, isMoving, startSequence, FailCamera, gameOver, AbilityChooseScreen, CountdownSeq;

    public GameObject LEVEL1, LEVEL2, LEVEL3;

    public List<EnemyBehaviour> Enemies = new List<EnemyBehaviour>();
    public int DeadEnemies;
    FinalDoor FD;

    public float startTime = 3f; // geri sayýmýn baþlangýç süresi
    public TextMeshProUGUI countdownText; // geri sayýmýn gösterileceði metin kutusu
    void Start()
    {
        tutcount = 0;
        tutcount1 = 0;
        tutcount1 = 0;
        startSequence = true;
        //canVibrate = true;
        Application.targetFrameRate = 60;
        Time.timeScale = 0;


        FD = FindObjectOfType<FinalDoor>();
        if (PlayerPrefs.GetInt("Level", 1) == 1)
        {
            LEVEL1.SetActive(true);
            countdownText.transform.gameObject.SetActive(false);

        }
        if (PlayerPrefs.GetInt("Level", 1) >= 2)
        {
            LEVEL2.SetActive(true);
            StartCoroutine(StartCountdown());
        }
        CountEnemies();
    }
    private void Update()
    {

    }
    public GameObject tut4, tut5, tut6, tut7;
    int tutcount, tutcount1, tutcount2;
    public void TutMoveDialog()
    {
        if(tutcount <= 0)
        {
            tut4.SetActive(false);
            tut5.SetActive(true);
            tutcount++;
        }
    }
    public void TutMoveDialog1()
    {
        if (tutcount1 <= 0)
        {
            tut5.SetActive(false);
            tut6.SetActive(true);
            tutcount1++;
        }
    }
    public void TutMoveDialog2()
    {
        if (tutcount2 <= 0)
        {
            tut6.SetActive(false);
            tut7.SetActive(true);
            //Time.timeScale = 0;
            tutcount2++;
        }
    }
    public void TimeReset()
    {
        Time.timeScale = 1f;
    }
    public void OpenFinalDoor()
    {
        FD.StartLerping();
        Time.timeScale = 1f;
    }
    public void CountEnemies()
    {
        // Use FindObjectsOfType to get all instances of MyClass in the scene
        EnemyBehaviour[] ClassArray = FindObjectsOfType<EnemyBehaviour>();

        // Add each instance of MyClass to the myClassInstances list
        foreach (EnemyBehaviour ClassInstance in ClassArray)
        {
            Enemies.Add(ClassInstance);
        }
    }
    IEnumerator StartCountdown()
    {
        float timeRemaining = startTime;

        while (timeRemaining > 0)
        {
            countdownText.text = timeRemaining.ToString("0");
            yield return new WaitForSecondsRealtime(1f);
            timeRemaining--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.gameObject.SetActive(false); // geri sayým metni gizle
        Time.timeScale = 1f; // zamaný normal hýzda devam ettir
        startSequence = false;
    }
}
