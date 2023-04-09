using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isGame = false;
    private bool pcActive = false;
    private int levelTime;
    [SerializeField] private GameObject level1;
    private GameObject currentLevel;

    public bool IsGame { get { return isGame; } }
    public bool PcActive { get { return pcActive; } set { pcActive = value; } }
    public int LevelTime { get { return levelTime;} }
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
        SoundManager.instance.PlayBgSound();
        Camera.main.transform.position = new Vector3(6, 25, -10);
        Camera.main.transform.eulerAngles = new Vector3(70, 0, 0);
        currentLevel = Instantiate(level1);
        levelTime = currentLevel.GetComponent<Level>().Time;
        isGame = true;
        pcActive = false;
        UIManager.instance.StartGame();
    }
    public void Win()
    {
        if (isGame)
        {
            SoundManager.instance.StopBgAudio();
            SoundManager.instance.PlaySoundEffects(SoundManager.AudioCallers.win);
            currentLevel.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(currentLevel, 6.5f);
            isGame = false;
            UIManager.instance.WinPanel();
        }
    }
    public void Lose()
    {
        if(isGame)
        {
            SoundManager.instance.StopBgAudio();
            SoundManager.instance.PlaySoundEffects(SoundManager.AudioCallers.lose);
            currentLevel.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(currentLevel,1f);
            isGame = false;
            UIManager.instance.LosePanel();
        }
    }

}
