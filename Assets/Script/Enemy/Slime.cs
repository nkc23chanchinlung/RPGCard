using Cainos.LucidEditor;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;


//制作中
public class Slime : EnemyBase
{
    bool _isAttacking; // 攻撃中かどうかのフラグ
    List<Card> _card;
   

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.J)) 
        AttackProcess(GameObject.FindGameObjectWithTag("Player").transform, Attack).Forget();
        
    }

    //カードを目標に向かって投げる処理
    void AttackCard(Transform Target)
    {
        _card = CardManager.Instance.GetCardList();

    }
   
}
