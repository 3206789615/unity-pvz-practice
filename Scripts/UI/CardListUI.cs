using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class CardListUI : MonoBehaviour
{
    public List<Card> cardList;
    public Transform grid;
    public int maxCardCount = 8;
    public GameObject shovelPrefab;
    public Button ShovelButton;
    private GameObject currentShovel;
    public Image ShovelUI;
    public int CurrentCount
    {
        get
        {
            if (grid == null)
            {
                Debug.LogError("grid Ã»¸³Öµ£¡");
                return 0;
            }

            return grid.childCount;
        }
    }
    private void Start()
    {        
        ShowCardList();
        ShovelButton.interactable = false;
    }
    public void ShowCardList()
    {
        //625
        GetComponent<RectTransform>().DOAnchorPosY(450, 2);
    }   
    public void EnableCardList()
    {
        foreach (Card card in cardList)
        {
            card.EnableCard();
        }
    }

    public void DisableCardList()
    {
        foreach (Card card in cardList)
        {
            card.DisabieCard();
        }
    }
    public void OnClickShovel()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        currentShovel = Instantiate(shovelPrefab, mousePos, Quaternion.identity);
        currentShovel.GetComponent<Shovel>().Init(this);
        ShovelUI.enabled = false;
    }

    public void ClearShovel()
    {
        ShovelUI.enabled = true;
        currentShovel = null;
    } 
   
}

