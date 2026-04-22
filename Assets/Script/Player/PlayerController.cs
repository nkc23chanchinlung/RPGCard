using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
   [SerializeField] List<GameObject> _selectedCard = new List<GameObject>(); //選択されたカードのリスト
    [SerializeField] int _cardLimit; //選択できるカードの上限
    DataManager dataManager;
    [SerializeField]BattleManager battleManager;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject[] _attackEffect;//0:斬撃 //1:サンダー

    void Update()
    {
        SelectCard();
        if (Input.GetKeyDown(KeyCode.A))
        {
           AttackProcess(_enemy.transform, 10,1).Forget();
            
            
        }

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

            Card cardmanager = hitObject.GetComponent<Card>();
            if (cardmanager != null)
            {
                cardmanager.ShowSprite();
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
            Card cardmanager = hit.collider.gameObject.GetComponent<Card>();
            if(cardmanager == null) return null;
            cardmanager.TouchPocess();
            if (GameManager.Instance._isDebugMode)
            {
                Debug.Log("カードの番号は" + cardmanager.GetCardNum());

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
        Card firstCardInfo = _selectedCard[0]. GetComponent<Card>();
        int firstNum = firstCardInfo.GetCardNum();

        foreach (var card in _selectedCard)
        {
            if (card == _selectedCard[0]) continue;
            Card cardInfo= card.GetComponent<Card>();
            int num = cardInfo.GetComponent<Card>().GetCardNum();
            
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
    async void SameCardProcess(Card card1, Card card2)
    {
        await UniTask.Delay(1000);
        AttackProcess(_enemy.transform, 10, 0).Forget();
        Destroy(card1.gameObject);
        Destroy(card2.gameObject);

    }
    /// <summary>
    /// 違うカードが選択されたときの処理
    /// </summary>
    /// <param name="card1">カード1</param>
    /// <param name="card2">カード2</param>
    async void DifferentCardProcess(Card card1, Card card2)
    {
        await UniTask.Delay(1000);
        card1.ResetCard();
        card2.ResetCard();


    }
    public int  GetSameCardNum(int num)
    {
        return num;
    }
    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <param name="target">対象</param>
    /// <param name="atk">攻撃力</param>
    /// <param name="atkEffectIndex">攻撃エフェクトの
    /// <returns></returns>
    public async UniTask AttackProcess(Transform target, int atk,int atkEffectIndex)
    {
        float origin = -6f; //原点

        transform.DOMoveX(target.position.x-2f, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            GameObject EF= Instantiate(_attackEffect[atkEffectIndex], target.position+new Vector3(0,1,0), Quaternion.identity);
            Destroy(EF,2f);

            target.gameObject.GetComponent<EnemyBase>().TakeDamage(atk);
        });

        
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        transform.DOMoveX(origin, 0.5f).SetEase(Ease.OutQuad);
        await UniTask.Yield();
    }

}
