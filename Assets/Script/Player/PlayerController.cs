using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
   [SerializeField] List<GameObject> _selectedCard = new List<GameObject>(); //選択されたカードのリスト

  

   
    void Update()
    {
        SelectCard();
    }
    //カードを選択する関数
    void SelectCard()
    {
        GameObject hitObject = MouseCollider();
        Debug.Log(hitObject);

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

    //マウス判定関数
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
            if (GameManager.instance._isDebugMode)
            {
                Debug.Log("カードの番号は" + cardInfo.GetCardNum());

            }
            return hit.collider.gameObject;
          
        }
        else return null;
    }
}
