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

    public List<Card> InstantCardList;//生成したカード管理するリスト
     int _sameCardValue = 0; //同じカードの値を管理する変数
    PlayerBase _player;
    DataManager _dataManager;
    bool _isOnce;

    int TestGameStartNum = 0;

    private void Awake()
    {
        Debug.Log("Init");
        Instance = this;
        
       
     
    }
    void Start()
    {
        //if (!GameManager.IsGameInit)
        //{
        //    Debug.Log("ADDGameStart");

        //   GameManager.OnGameStart += this.OnGameStart;
        //    GameManager.IsGameInit=true;

        //}
        GameManager.Instance.IsGameInit = false;
    }
    async  UniTaskVoid OnGameStartAsync()
    {
        await UniTask.Yield();
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        //_dataManager = DataManager.Instance;
        
        InstantCardList = new List<Card>();
        await UniTask.Yield();
        InstanceCard(2.0f, -4).Forget();
        _cardSprite = _player.GetCardList();
    }
   void OnGameStart()
    {

        OnGameStartAsync().Forget();

    }
    void Update()
    {
     //  _dataManager._sameCardValue = _sameCardValue;
        Debug.Log("GameInit:" + GameManager.Instance.IsGameInit);
        if (Input.GetKeyDown(KeyCode.R))
        {
            Noduplicatesultiple().Forget();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            InstanceCard(2.0f, -4).Forget();

        }
        if (!GameManager.Instance.IsGameInit)
        {
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
            InstanceCard(2.0f, -4).Forget();
            _cardSprite = _player.GetCardList();

            GameManager.Instance.IsGameInit = true;

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
        Debug.Log("Instace");
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

               
                
                Card cardInfo = instobj.GetComponent<Card>();
                Debug.Log(cardInfo.GetCardNum());
                InstantCardList.Add(cardInfo);
                TestGameStartNum++;
                Debug.Log("TestGameStartNum:" + TestGameStartNum);
                cardInfo.SetCardNum(Random.Range(0, _cardSprite.Length));
                var img = instobj.transform.Find("Img").GetComponent<SpriteRenderer>();
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
        for (int i = 0; i < InstantCardList.Count; i++)
        {
            int cardA = InstantCardList[i].GetComponent<Card>().GetCardNum();

            for (int j = i + 1; j < InstantCardList.Count; j++)
            {
                int cardB = InstantCardList[j].GetComponent<Card>().GetCardNum();

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
        
        foreach (var c in InstantCardList)
        {
            c.ResetCard();
            await UniTask.WhenAll(MoveCardAsync(c.gameObject, new Vector3(5, 0, 0)));
            await UniTask.Delay(150);
            Destroy(c.gameObject,0.5f);
        }
        InstantCardList.Clear();
        await UniTask.Delay(500);


        InstanceCard(2.0f, -4).Forget();


    }
    public async UniTask RemoveCardList(Card card1,Card card2)
    {
        if (card1!=null)
        InstantCardList.Remove(card1);
        if (card2!=null)
        InstantCardList.Remove(card2);

        await UniTask.Yield();
    }
    public List<Card> GetCardList()
    {
        return InstantCardList;
    }
}
