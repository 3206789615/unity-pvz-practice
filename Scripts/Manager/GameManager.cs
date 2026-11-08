using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CardListUI cardListUI;
    public PrepareUI prepareUI;
    public SelectPanelUI selectPanelUI;
    public FailUI failUI;
    public ZombieSlider zombieSlider;
    public Card[] cards;
    private bool isGameEnd = false;
    private bool stopgame = false;

    public GameObject menu;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameReady();       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.Play("pause");
            if (stopgame==false)
            {
                Time.timeScale = 0;
                cardListUI.ShovelButton.interactable = false;
                foreach (Card card in cards)
                {
                    if (card == null) continue;
                    card.cardButton.interactable = false;
                }
                
                stopgame = true;
            }
            else
            {
                Time.timeScale = 1;
                cardListUI.ShovelButton.interactable = true;
                foreach (Card card in cards)
                {
                    if (card == null) continue;
                    card.cardButton.interactable = true;
                }
                stopgame = false;
            }
        }
    }
    void GameReady()
    {
        AudioManager.Instance.PlayBGM("bgm3");
        Vector3 currentPosition = Camera.main.transform.position;
        Camera.main.transform.DOPath(new Vector3[] {currentPosition,new Vector3(5.5f,0,-10)},2);
    }

    public void GameStart()
    {       
        selectPanelUI.GetComponent<RectTransform>().DOAnchorPosY(-950, 2);        
        Vector3 currentPosition = Camera.main.transform.position;
        Camera.main.transform.DOPath(new Vector3[] { currentPosition, new Vector3(0f, 0, -10) }, 2);
        Invoke(nameof(ShowStart), 2.5f);
        Invoke(nameof(ZombieStart), 4f);
        Invoke(nameof(SunStart), 4f);
        Invoke(nameof(PlantStart), 4f);
        AudioManager.Instance.StopBGM("bgm3");
        AudioManager.Instance.Play("buttonclick");
    }

    void ShowStart()
    {
        AudioManager.Instance.Play("readysetplant");
        prepareUI.Show();
    }
    void SunStart()
    {
        SunManager.Instance.StartProduce();
    }
    void ZombieStart()
    {
        ZombieManager.Instance.StartSpawn();
        zombieSlider.isstart = true;       
        AudioManager.Instance.PlayBGM("bgm1");
    }
    void PlantStart()
    {
        cardListUI.EnableCardList();
        cardListUI.ShovelButton.interactable = true;
    }

    public void GameFalseEnd()
    {
        if (isGameEnd == true) return;
        AudioManager.Instance.StopBGM("bgm1");
        AudioManager.Instance.StopBGM("bgm2");
        AudioManager.Instance.Play("losemusic");
        failUI.Show();       
        ZombieManager.Instance.Pause();
        PlantManager.Instance.Pause();
        SunManager.Instance.StopProduce();
        cardListUI.DisableCardList();
        zombieSlider.isstart = false;
        isGameEnd = true;

    }

    public void GameEndSuccess()
    {
        if (isGameEnd == true) return;
        AudioManager.Instance.StopBGM("bgm1");
        AudioManager.Instance.StopBGM("bgm2");
        AudioManager.Instance.Play("winmusic");
        isGameEnd = true;
    }

    public void ToggleMenu()
    {
        menu.SetActive(true);       
        AudioManager.Instance.Play("pause");
        AudioManager.Instance.PauseBGM();
        if (stopgame == false)
        {
            Time.timeScale = 0;
            cardListUI.ShovelButton.interactable = false;
            foreach (Card card in cards)
            {
                if (card == null) continue;
                card.cardButton.interactable = false;
            }

            stopgame = true;
        }        
    }

    public void Returngame()
    {
        menu.SetActive(false);
        AudioManager.Instance.Play("pause");
        AudioManager.Instance.ResumeBGM();
        if (stopgame==true)
        {
            Time.timeScale = 1;
            cardListUI.ShovelButton.interactable = true;
            foreach (Card card in cards)
            {
                if (card == null) continue;
                card.cardButton.interactable = true;
            }
            stopgame = false;
        }
    }
}
