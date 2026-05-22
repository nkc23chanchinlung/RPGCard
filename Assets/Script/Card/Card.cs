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
    [SerializeField] GameObject _debuff_Water;
    [SerializeField] GameObject _debuff_Fire;

    public enum Debuff
    {
        Null,
        Water,
        Fire,
        
    }
    public Debuff _debuff;
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
    //カードの番号を設定する
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
     public void SetCardDebuff(Debuff debuff)
    {
        SpriteRenderer spriteRenderer =GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer);
        _debuff = debuff;
        if (_debuff == Debuff.Water)
        {
            //_debuff_Water.SetActive(true);
            spriteRenderer.color = Color.blue;
            Debug.Log("Water");
        }
        else if(_debuff == Debuff.Fire)
        {
           // _debuff_Fire.SetActive(true);
        }
        else
        {
            //_debuff_Water.SetActive(false);
            //_debuff_Fire.SetActive(false);
            spriteRenderer.color = Color.white;
        }
    }    
}
