using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

//カードの生成と管理を行うクラス

public class CardController : MonoBehaviour
{
    [SerializeField] GameObject _cardPre; //カードのプレハブ
    
    int _cardNum = 0; //カードの枚数
    [SerializeField]int _instanceX, _instanceY; //カードの生成位置
    [SerializeField]Sprite[] _cardSprite; //カードのスプライト
    [SerializeField] List<CardManager> _cardInfoList; //カードの情報を管理するリスト
     int _sameCardValue = 0; //同じカードの値を管理する変数
    DataManager _dataManager;


    private void Awake()
    {
        _dataManager = DataManager.Instance;
    }
    void Start()
    {
        InstanceCard(2.0f,-4);
        
    }
    private void FixedUpdate()
    {
        _dataManager._sameCardValue = _sameCardValue; 
    }
    void Update()
    {
        
    }
    void InstanceCard(float distance,int initvalue)
    {
        GameObject instobj;
        
        for (int i = 0; i < _instanceY; i++)
        {
            for (int j = 0; j < _instanceX; j++)
            {
                instobj = 
                    Instantiate(_cardPre,
                    new Vector3(5,0,0), 
                    Quaternion.identity);

                

                //カードを移動させる
                MoveCardAsync(instobj,
                    new Vector3(initvalue + j * distance, initvalue + i * distance, 0)).Forget();

                var img = instobj.transform.Find("Img").GetComponent<SpriteRenderer>();
                
                CardManager cardInfo = instobj.GetComponent<CardManager>();
                _cardInfoList.Add(cardInfo);
                cardInfo.SetCardNum(Random.Range(0, _cardSprite.Length));

                //カードのスプライトを設定
                img.sprite = _cardSprite[cardInfo.GetCardNum()];

                if (cardInfo.GetCardNum()==0)
                {
                    Debug.Log("攻撃");
                }

            }
        }
        CheckCard();
    }
    /// <summary>
    /// カード移動関数
    /// </summary>
    /// <param name="target">移動オブジェクト</param>
    /// <param name="position">目的地</param>
    /// <returns></returns>
    async UniTask MoveCardAsync(GameObject target, Vector3 position)
    {
        float duration = 0.5f; // 移動にかかる時間
        Vector3 startPosition = target.transform.position;
        
        target.transform.DOMove(position, duration).SetEase(Ease.OutQuad);
           
        await UniTask.Yield();
    }

    void CheckCard()
    {
        for (int i = 0; i < _cardInfoList.Count; i++)
        {
            int cardA = _cardInfoList[i].GetComponent<CardManager>().GetCardNum();

            for (int j = i + 1; j < _cardInfoList.Count; j++)
            {
                int cardB = _cardInfoList[j].GetComponent<CardManager>().GetCardNum();

                if (cardA == cardB)
                {
                    Debug.Log(cardA+","+ cardB);
                    cardA = -1;
                    cardB = -1;
                    Debug.Log("同じカードがある");
                    _sameCardValue++;
                }
               
            }
        }
    }
    //重複カードないとき回収する
    void Noduplicatesultiple()
    {
        MoveCardAsync(gameObject, new Vector3(0, 0, 0)).Forget();
    }
}
