using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードの情報を管理するクラス
/// </summary>
public class Card : MonoBehaviour
{ 
    GameObject Outline;
    Animator _animator;
    public bool IsChoose { get; set; } = false;
    private void Awake()
    {
        
        Outline = transform.Find("Choose").gameObject;
        GameObject img = transform.Find("Img").gameObject;
        _animator= GetComponent<Animator>();
        if (GameManager.Instance._isDebugMode) img.SetActive(true);
        else img.SetActive(false);
    }
    private void FixedUpdate()
    {
        Outline.SetActive(false);
    }
    //カードの効果連想配列
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
        _animator.SetBool("isShow", true);
        img.SetActive(true);
    }
    public void TouchPocess()
    {
        if (IsChoose)
        {
            Outline.SetActive(false);
        }
        else
        Outline.SetActive(true);
    }
    public void ResetCard()
    {
        Debug.Log("カードのリセット");
        GameObject img = transform.Find("Img").gameObject;
        _animator.SetBool("isShow", false);

        img.SetActive(false);
    }
    public void DeletCard()
    {
       Destroy(gameObject);
    }
}
