using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



enum CardState
{
    Move,
    Disable,
    Cooling,
    WaitingSun,
    Ready,
}

public enum PlantType
{
    Sunflower,
    PeaShooter,
    WallNut,
    Squash,
    SnowPea,
    Jalapeno,
    Chomper,
    PotatoMine,
    Repeater
}
public class Card : MonoBehaviour
{
    private CardState cardState = CardState.Move;
    public PlantType plantType = PlantType.Sunflower;
    public Button cardButton;
    public GameObject cardLight;
    public GameObject cardGray;
    public Image cardMask;
    [SerializeField]
    private float cdTime = 2f;
    private float cdTimer = 0f;
    [SerializeField]
    private int needSunPoint = 50;
    private void Update()
    {
        switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }
    }

    void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;
        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;

        if (cdTimer>=cdTime)
        {
            TransitionToWaitingSun();
        }
    }
    void WaitingSunUpdate()
    {
        if (needSunPoint<=SunManager.Instance.SunPoint)
        {
            TransitionToReady();
        }
    }
    void ReadyUpdate()
    {
        if (needSunPoint>SunManager.Instance.SunPoint)
        {
            TransitionToWaitingSun();
        }
    }

    void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }
    void TransitionToReady()
    {
        cardState = CardState.Ready;
        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }
    void TransitionToCooling()
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true);
    }



    public void OnClick()
    {
        // Debug.Log("当前状态: " + cardState);
        AudioManager.Instance.Play("coffee");
        if (cardState == CardState.Move)
        {
            MoveOnClick();
            return;
        }

        if (cardState == CardState.Ready)
        {
            PlantOnClick();
            return;
        }
    }
    void PlantOnClick()
    {
        if (needSunPoint > SunManager.Instance.SunPoint) return;

        //TODO：进行种植      
        bool isSuccess=HandManager.Instance.AddPlant(plantType);
        if (isSuccess)
        {
            SunManager.Instance.SubSun(needSunPoint);
            TransitionToCooling();
        }
       
    }
    public Transform targetGrid; // CardListUI/Grid
    void MoveOnClick()
    {
        // 1. 換父物體
        transform.SetParent(targetGrid);

        // 2. UI 必須重置位置
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = Vector3.one;

        // 3. 狀態改為 Disable（你設計的流程）
        cardState = CardState.Disable;
    }
    
    public void DisabieCard()
    {
        cardState = CardState.Disable;
    }
    public void EnableCard()
    {
        TransitionToCooling();
    }

  
    public void MoveCard()
    {
        cardState = CardState.Move;
        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }




}
