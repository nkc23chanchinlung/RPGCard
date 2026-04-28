using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードの情報を管理するクラス
/// </summary>
public class Card : MonoBehaviour
{
   [SerializeField] int _cardNum; //カードの番号
    GameObject _outLine;
    Animator _animator;
    public bool IsChoose { get; set; } //カードが選択されているかどうか

    
    private void Awake()
    {
        
        _outLine = transform.Find("Choose").gameObject;
        GameObject img = transform.Find("Img").gameObject;
        _animator= GetComponent<Animator>();
        if (GameManager.Instance._isDebugMode) img.SetActive(true);
        else img.SetActive(false);
    }
    private void FixedUpdate()
    {
        _outLine.SetActive(false);
    }
    //カードの効果連想配列
    public Dictionary<string, int> cardDic = new Dictionary<string, int>()
    {
        {"attack",1},
        {"FireBall",2},
        {"heal",18}
    };
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
            _outLine.SetActive(false);
        }
        else
        _outLine.SetActive(true);
    }
    public void ResetCard()
    {
        Card card= GetComponent<Card>();
        card.IsChoose = false;
        GameObject img = transform.Find("Img").gameObject;
        _animator.SetBool("isShow", false);
        PlayerController.Instance._selectedCard.Remove(gameObject);

        img.SetActive(false);
    }
    public void DeletCard()
    {
       Destroy(gameObject);
    }
}
