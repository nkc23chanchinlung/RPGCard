using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードの情報を管理するクラス
/// </summary>
public class CardInfo : MonoBehaviour
{ 
    GameObject choose;
    private void Awake()
    {
        GameObject img = transform.Find("Img").gameObject;
        choose = transform.Find("Choose").gameObject;
        if(GameManager.instance._isDebugMode) img.SetActive(true);
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
    public void ResetCard()
    {
        Debug.Log("カードのリセット");
        GameObject img = transform.Find("Img").gameObject;
        img.SetActive(false);
    }
    public void DeletCard()
    {
       Destroy(gameObject);
    }
}
