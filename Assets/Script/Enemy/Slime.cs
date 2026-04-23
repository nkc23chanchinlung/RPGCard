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

    public async UniTask AttackProcess(Transform target, int atk)
    {
        float origin = 6f; //原点

        transform.DOMoveX(target.position.x +2f, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {


            target.gameObject.GetComponent<PlayerController>().TakeDamage(atk).Forget();
        });


        await UniTask.Delay(TimeSpan.FromSeconds(1));

        transform.DOMoveX(origin, 0.5f).SetEase(Ease.OutQuad);
        await UniTask.Yield();
    }
}
