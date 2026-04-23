using UnityEngine;
using System.Collections.Generic;

public class CardMaker : MonoBehaviour
{
    [SerializeField] GameObject _cardPrefab; // カードのプレハブ
    Card _cardInfo; // カードの情報を管理するクラス
    
    public List<Card> CardInfoList; // カードの情報を管理するリスト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
