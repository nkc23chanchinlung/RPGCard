using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// プレイヤー操作処理クラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    public List<GameObject> _selectedCard = new List<GameObject>(); //選択されたカードのリスト
    [SerializeField] int _cardLimit; //選択できるカードの上限
    [SerializeField] GameObject _enemy;
    GameObject[] _attackEffect;//0:斬撃 //1:サンダー
    [SerializeField] int Max_chanceLimit;
    int _chanceLimit;
    [SerializeField] ScreenEffect _screenEffect;
    PlayerBase _player;

    

    private void Awake()
    {
        Instance = this;
        _chanceLimit = Max_chanceLimit;
        _enemy = GameObject.FindWithTag("Enemy");
        _player=gameObject.GetComponent<PlayerBase>();
    }
    void Start()
    {
        _attackEffect = _player.GetAttackEffect();
    }
    void Update()
    {
        SelectCard();

        //デバッグ用の攻撃処理
        if (Input.GetKeyDown(KeyCode.A))
        {
           AttackProcess(_enemy.transform, 10,1,1).Forget();
        }

    }
    private void FixedUpdate()
    {
        ListManagement(_cardLimit);
       
       
    }

    /// <summary>
    /// カードを選択する関数
    /// </summary>
   public void SelectCard()
    {
        GameObject hitObject = MouseCollider();

        if (hitObject == null) return;

        if (hitObject.CompareTag("Card") && Input.GetMouseButtonDown(0))
        {
            Card card = hitObject.GetComponent<Card>();
            if(card.IsChoose) return;
            Debug.Log("カードを選択");
            _selectedCard.Add(hitObject);

           
            if (card != null)
            {
                card.ShowSprite();
                card.IsChoose = true;
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
            Card card = hit.collider.gameObject.GetComponent<Card>();
            if(card == null) return null;
            card.TouchPocess();
            if (GameManager.Instance._isDebugMode)
            {
                Debug.Log("カードの番号は" + card.GetCardNum());

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
                DifferentCardProcess(firstCardInfo, cardInfo).Forget();
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
        int _cardNum = card1.GetCardNum();

        //職業の攻撃パターン　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　<---独立させるべき
        if (_cardNum==1) AttackProcess(_enemy.transform, 10, card1.GetCardNum(), 1).Forget();
        else if(_cardNum==2) AttackProcess(_enemy.transform, 10, card1.GetCardNum(), 2).Forget();
        else AttackProcess(_enemy.transform, 10, card1.GetCardNum(), 0).Forget();

        CardManager.Instance.RemoveCardList(card1, card2).Forget();
        Destroy(card1.gameObject);
        Destroy(card2.gameObject);
        

    }
    /// <summary>
    /// 違うカードが選択されたときの処理
    /// </summary>
    /// <param name="card1">カード1</param>
    /// <param name="card2">カード2</param>
    async UniTask DifferentCardProcess(Card card1, Card card2)
    {
        await UniTask.Delay(1000);
        card1.ResetCard();
        card2.ResetCard();

        //チャンス回数を減らす処理
        _chanceLimit--;
        UiManager.Instance.EditChanceText(_chanceLimit).Forget();

        if(_chanceLimit <= 0)
        {
           EnemyBase enemy=GameObject.FindWithTag("Enemy").GetComponent<EnemyBase>();
            await enemy.AttackProcess(transform, enemy.Attack);

            _chanceLimit= Max_chanceLimit;
            UiManager.Instance.EditChanceText(_chanceLimit).Forget();

        }
    }
    public int GetSameCardNum(int num)
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
    public async UniTask AttackProcess(Transform target, int atk,int atkEffectIndex,int AttackPatterns)
    {
        float origin = -6f; //原点
        float moveDuration = 0.5f; //移動時間
        PlayerBase playerBase = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();

        //攻撃パターン0は近距離攻撃
        if (AttackPatterns == 0)
        {
            
                playerBase.SetAttackTrue(playerBase.gameObject);
                transform.DOMoveX(target.position.x - 2f, moveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
               {
                   GameObject EF = Instantiate(_attackEffect[atkEffectIndex], target.position + new Vector3(0, 1, 0), Quaternion.identity);
                   Destroy(EF, 1f);
                   target.gameObject.GetComponent<EnemyBase>().TakeDamage(atk);
               });

                await UniTask.Delay(TimeSpan.FromSeconds(1));

                transform.DOMoveX(origin, moveDuration).SetEase(Ease.OutQuad);
            
        }
        //1は遠距離攻撃
        else if (AttackPatterns == 1)
        {
           
            playerBase.SetAttackTrue(playerBase.gameObject);
            GameObject EF = Instantiate(_attackEffect[atkEffectIndex], target.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(EF, 1f);
           

            target.gameObject.GetComponent<EnemyBase>().TakeDamage(atk);
        }
        //２は飛び道具攻撃
        else if (AttackPatterns == 2)
        {
            playerBase.SetAttackTrue(playerBase.gameObject);
            GameObject EF = Instantiate(_attackEffect[atkEffectIndex], transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            EF.transform.DOMove(target.position + new Vector3(0, 1, 0), moveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                Destroy(EF);
                target.gameObject.GetComponent<EnemyBase>().TakeDamage(atk);
                if(atkEffectIndex==2)//2は火の攻撃
            　　EffectManager.Instance.InstanceFireEffect(target).Forget();
            });
        }


        await UniTask.Yield();
    }
    

}
