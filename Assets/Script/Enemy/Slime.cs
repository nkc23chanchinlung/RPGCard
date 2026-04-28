using Cainos.LucidEditor;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class Slime : EnemyBase
{
    bool _isAttacking; // 攻撃中かどうかのフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.J)) 
        AttackProcess(GameObject.FindGameObjectWithTag("Player").transform, Attack).Forget();
        
    }
   
}
