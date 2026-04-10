using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
public class PlayerController : MonoBehaviour
{
   [SerializeField] List<GameObject> _selectedCard = new List<GameObject>(); //選択されたカードのリスト
    [SerializeField] int _cardLimit; //選択できるカードの上限
    DataManager dataManager;

    
    void Update()
    {
        SelectCard();

    }
    private void FixedUpdate()
    {
        ListManagement(_cardLimit);
    }

    /// <summary>
    /// カードを選択する関数
    /// </summary>
    void SelectCard()
    {
        GameObject hitObject = MouseCollider();

        if (hitObject == null) return;

        if (hitObject.CompareTag("Card") && Input.GetMouseButtonDown(0))
        {
            Debug.Log("カードを選択");
            _selectedCard.Add(hitObject);

            CardInfo cardInfo = hitObject.GetComponent<CardInfo>();
            if (cardInfo != null)
            {
                cardInfo.ShowSprite();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var card in _selectedCard)
            {
                Debug.Log(card.name);
            }
        }
    }

    /// <summary>
    /// マウス判定関数
    /// </summary>
    /// <returns></returns>
    GameObject MouseCollider()
    {
        Vector3 mospos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mospos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            CardInfo cardInfo = hit.collider.gameObject.GetComponent<CardInfo>();
            if(cardInfo == null) return null;
            cardInfo.TouchPocess();
            if (GameManager.Instance._isDebugMode)
            {
                Debug.Log("カードの番号は" + cardInfo.GetCardNum());

            }
            return hit.collider.gameObject;
          
        }
        else return null;
    }
    /// <summary>
    /// 選択されたカードのリストを管理する関数
    /// </summary>
    /// <param name="cardLimit">選択できるカードの上限</param>
    void ListManagement(int cardLimit)
    {
        //if (_selectedCard.Count > cardLimit)
        //{
        //    _selectedCard.Clear();
        //    return;
        //}

        if (_selectedCard.Count <= 1) return;
        CardInfo firstCardInfo = _selectedCard[0]. GetComponent<CardInfo>();
        int firstNum = firstCardInfo.GetCardNum();

        foreach (var card in _selectedCard)
        {
            if (card == _selectedCard[0]) continue;
            CardInfo cardInfo= card.GetComponent<CardInfo>();
            int num = cardInfo.GetComponent<CardInfo>().GetCardNum();
            
            if (num != firstNum)
            {
                DifferentCardProcess(firstCardInfo, cardInfo);
                _selectedCard.Clear();
                
            }
            else if (num == firstNum)
            {
                SameCardProcess(firstCardInfo, cardInfo);
                _selectedCard.Clear();

            }
        }
    }
    /// <summary>
    /// 同じカードが選択されたときの処理
    /// </summary>
    async void SameCardProcess(CardInfo card1, CardInfo card2)
    {
        await UniTask.Delay(1000);
        Destroy(card1.gameObject);
        Destroy(card2.gameObject);

    }
    /// <summary>
    /// 違うカードが選択されたときの処理
    /// </summary>
    /// <param name="card1">カード1</param>
    /// <param name="card2">カード2</param>
    async void DifferentCardProcess(CardInfo card1, CardInfo card2)
    {
        await UniTask.Delay(1000);
        card1.ResetCard();
        card2.ResetCard();


    }
    public int  GetSameCardNum(int num)
    {
        return num;
    }

}
