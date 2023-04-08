using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGame = false;
    public bool pcActive = false;
    public int levelTime = 500;
    [SerializeField] private GameObject level1;
    private GameObject currentLevel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {
        Camera.main.transform.position = new Vector3(6, 25, -10);
        Camera.main.transform.eulerAngles = new Vector3(70, 0, 0);
        currentLevel = Instantiate(level1);
        isGame = true;
        pcActive = false;
        UIManager.instance.StartGame();
    }
    public void Win()
    {
        currentLevel.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(currentLevel, 6.5f);
        isGame = false;
        UIManager.instance.WinPanel();
    }
    public void Lose()
    {
        if(isGame)
        {
            currentLevel.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(currentLevel,1f);
            isGame = false;
            UIManager.instance.LosePanel();
        }
    }

}
