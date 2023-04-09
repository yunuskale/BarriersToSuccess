using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel, losePanel, menuPanel, gamePanel;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image bar;
    [SerializeField] private GameObject[] cardButtons;
    [SerializeField] private RectTransform card, akademiImage, altKapan, ustKapan;
    [SerializeField] private RectTransform[] akademiParcalari;
    private GameObject currentPanel;
    public static UIManager instance;

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
    private void Start()
    {
        currentPanel = menuPanel;
    }
    public void StartGame()
    {
        timeText.text = "";
        bar.fillAmount = 0;
        for (int i = 0; i < cardButtons.Length; i++)
        {
            cardButtons[i].GetComponent<Button>().interactable = false;
            cardButtons[i].GetComponent<PassCard>().password = 0;
            cardButtons[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0.39f, 0.39f, 0.39f, 0.7f);
        }
        StartCoroutine(CountDown());
        GamePanel();
    }
    public void WinPanel()
    {
        currentPanel.SetActive(false);
        Camera cam = Camera.main;
        cam.transform.DOLocalRotate(new Vector3(25, 0, 0), 4f);
        cam.transform.DOMove(new Vector3(62.8f, 3.7f, -6.3f), 4f).OnComplete(() =>
        {
            gamePanel.GetComponent<Image>().DOColor(new Color(0.990566f, 0.9892463f, 0.62144f), 2f).OnComplete(() =>
            {
                winPanel.SetActive(true);
                currentPanel = winPanel;
                gamePanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            });
        });       

    }
    public void LosePanel()
    {
        currentPanel.SetActive(false);
        altKapan.DOAnchorPosY(0, 1f);
        ustKapan.DOAnchorPosY(0, 1f).OnComplete(() =>
        {
            ustKapan.anchoredPosition = Vector2.up * 1300;
            altKapan.anchoredPosition = Vector2.down * 1300;
            losePanel.SetActive(true);
            currentPanel = losePanel;
            
        });
    }
    public void GamePanel()
    {
        currentPanel.SetActive(false);
        gamePanel.SetActive(true);
        currentPanel = gamePanel;
    }
    public void MenuPanel()
    {
        currentPanel.SetActive(false);
        menuPanel.SetActive(true);
        currentPanel = menuPanel;
    }
    public void KartAnim(int index, int password)
    {
        card.DOScale(1, 0.7f).OnComplete(() =>
        {
            card.DOAnchorPos(cardButtons[index].GetComponent<RectTransform>().anchoredPosition, 0.7f).OnComplete(() =>
            {
                card.localScale = Vector3.zero;
                card.anchoredPosition = Vector3.zero + Vector3.up * 500;
                cardButtons[index].GetComponent<Button>().interactable = true;
                cardButtons[index].GetComponent<PassCard>().password = password;
                cardButtons[index].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            });
        });
    }
    public void AkademikParcaAnim()
    {
        int r = Random.Range(0, akademiParcalari.Length);
        akademiParcalari[r].DOScale(1, 0.7f).OnComplete(() =>
        {
            akademiParcalari[r].DOAnchorPos(Vector3.right * -222, 0.7f).OnComplete(() =>
            {
                akademiImage.DOScale(1.3f, 0.5f).OnComplete(() =>
                {
                     akademiImage.DOScale(1, 0.5f);
                });
                akademiParcalari[r].localScale = Vector3.zero;
                akademiParcalari[r].anchoredPosition = r > 1 ? akademiParcalari[r - 1].anchoredPosition : akademiParcalari[r + 1].anchoredPosition;
                bar.DOFillAmount(bar.fillAmount + 0.17f, 0.5f).OnComplete(() =>
                {
                    if (bar.fillAmount > 0.95)
                    {
                        GameManager.instance.PcActive = true;
                    }
                });
            });
        });
    }
    WaitForSeconds oneSecond = new WaitForSeconds(1);
    public IEnumerator CountDown()
    {
        int time = GameManager.instance.LevelTime;

        while (time > 0 && GameManager.instance.IsGame)
        {
            yield return oneSecond;
            time -= 1;
            timeText.text = time.ToString() + " sec";
        }
        if(time == 0)
        {
            GameManager.instance.Lose();
        }
    }
}
