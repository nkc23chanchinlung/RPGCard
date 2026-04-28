using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

//カードの生成と管理を行うクラス

public class CardManager : MonoBehaviour
{
    static public CardManager Instance;


    [SerializeField] GameObject _cardPre; //カードのプレハブ
    int _cardNum = 0; //カードの枚数
    [SerializeField]int _instanceX, _instanceY; //カードの生成位置
    [SerializeField]Sprite[] _cardSprite; //カードのスプライト
    [SerializeField] List<Card> _instantCardList; //生成したカード管理するリスト
     int _sameCardValue = 0; //同じカードの値を管理する変数
    DataManager _dataManager;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _dataManager = DataManager.Instance;
        InstanceCard(2.0f, -4).Forget() ;



    }
    private void FixedUpdate()
    {
       _dataManager._sameCardValue = _sameCardValue;
        

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Noduplicatesultiple().Forget();
        }
    }
    /// <summary>
    /// カードを生成する関数
    /// </summary>
    /// <param name="distance">カード間の距離</param>
    /// <param name="initvalue">初期位置の値</param>
    /// <returns></returns>
    async  UniTask InstanceCard(float distance,int initvalue)
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
                
                Card cardInfo = instobj.GetComponent<Card>();
                _instantCardList.Add(cardInfo);
                cardInfo.SetCardNum(Random.Range(0, _cardSprite.Length));

                //カードのスプライトを設定
                img.sprite = _cardSprite[cardInfo.GetCardNum()];

                if (cardInfo.GetCardNum()==0)
                {
                    Debug.Log("攻撃");
                }
                await UniTask.Delay(100); //カード生成の間隔を調整
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
        
        target.transform.DOMove(position, duration)
            .SetEase(Ease.OutQuad)
            .AsyncWaitForCompletion()
            .AsUniTask().Forget();
            
          
        await UniTask.Yield();
    }

    /// <summary>
    /// 生成したカードチェック関数
    /// </summary>
    void CheckCard()
    {
        for (int i = 0; i < _instantCardList.Count; i++)
        {
            int cardA = _instantCardList[i].GetComponent<Card>().GetCardNum();

            for (int j = i + 1; j < _instantCardList.Count; j++)
            {
                int cardB = _instantCardList[j].GetComponent<Card>().GetCardNum();

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
    async UniTask Noduplicatesultiple()
    {
        
        foreach (var c in _instantCardList)
        {
            c.ResetCard();
            await UniTask.WhenAll(MoveCardAsync(c.gameObject, new Vector3(5, 0, 0)));
            await UniTask.Delay(150);
            Destroy(c.gameObject,0.5f);
        }
        _instantCardList.Clear();
        await UniTask.Delay(500);


        InstanceCard(2.0f, -4).Forget();


    }
    public async UniTask RemoveCardList(Card card1,Card card2)
    {
        if (card1!=null)
        _instantCardList.Remove(card1);
        if (card2!=null)
        _instantCardList.Remove(card2);

        await UniTask.Yield();
    }
}
