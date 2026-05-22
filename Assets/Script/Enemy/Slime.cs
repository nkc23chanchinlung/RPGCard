using Cainos.LucidEditor;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


//制作中
public sealed class Slime : EnemyBase
{
    bool _isAttacking; // 攻撃中かどうかのフラグ
    [SerializeField] GameObject _waterBall_Pre; // 水の玉のプレハブ
    List<Card> _card;
   

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {




        CardList = CardManager.Instance.GetCardList();

        if (Input.GetKeyDown(KeyCode.J))
            SkillProcess(Random.Range(0, CardList.Count));


    }

    //カードを目標に向かって投げる処理
    void AttackCard(Transform Target)
    {
        _card = CardManager.Instance.GetCardList();

    }
   
}
