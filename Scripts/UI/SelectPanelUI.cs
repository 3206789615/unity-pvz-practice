using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanelUI : MonoBehaviour
{
    public List<Card> cardList;
    public CardListUI cardListUI;
    private void Start()
    {
        MoveCardList();
        ShowCardList();
    }
    public void ShowCardList()
    {
        //-950
        GetComponent<RectTransform>().DOAnchorPosY(-143, 2);
    }
    void MoveCardList()
    {
        foreach (Card card in cardList)
        {
            card.MoveCard();
        }
    }
   
    public void Startgame()
    {
        if (cardListUI.CurrentCount == cardListUI.maxCardCount)
        {
            GameManager.Instance.GameStart();
        }
        else
        {
            return;
        }
    }

   
}
