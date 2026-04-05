using System.Collections.Generic;
using UnityEngine;

//カードの情報を管理するクラス
public class CardInfo : MonoBehaviour
{ 
    GameObject choose;
    private void Awake()
    {
        choose = transform.Find("Choose").gameObject;
    }
    private void FixedUpdate()
    {
        choose.SetActive(false);
    }

    public Dictionary<string, int> cardDic = new Dictionary<string, int>()
    {
        {"attack",1},
        {"FireBall",2},
        {"heal",18}
    };

    [SerializeField]int _cardNum; //カードの番号

    //カードの番号を設定する
    public void SetCardNum(string cardName)
    {
        _cardNum = cardDic[cardName];
    }
    //カードの番号を設定する overload
    public void SetCardNum(int cardNum)
    {
        _cardNum = cardNum;
    }

    //カードの番号を取得する
    public int GetCardNum()
    {
        return _cardNum;
    }
    //カードのスプライトを表示する関数
    public void ShowSprite()
    {
        GameObject img= transform.Find("Img").gameObject;
        img.SetActive(true);
    }
    public void TouchPocess()
    {
        choose.SetActive(true);
    }
}
