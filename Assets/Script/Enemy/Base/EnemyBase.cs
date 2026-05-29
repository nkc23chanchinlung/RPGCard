using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 敵の基底クラス
/// </summary>
public class EnemyBase : MonoBehaviour
{
    [Header("Status")]
    public float MaxHP;
    public float Hp;
    public int Attack;
    public float Speed;
    public int Defense;
    public int Level;
    float _skill_Timer;
    
    public PlayerBase player;

    public List<Skill> SkillList;
    public List<Card> CardList;


    
    /// <summary>
    /// ダメージ受ける関数
    /// </summary>
    /// <param name="Dmg">受けるダメージ量</param>
    public void TakeDamage(int Dmg)
    {
        //振動のパラメータ
        float duration = 0.1f;
        float strength = 0.2f;

        //処理
        DamageCale(Dmg, Defense, () =>
        {
            UiManager.Instance.CreateDmg_Text(transform, Dmg).Forget();
            Shake(duration, strength).Forget();
        });
    
   
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 画面振動エフェクト
    /// </summary>
    /// <param name="duration">振動長さ</param>
    /// <param name="strength">振動の強さ</param>
    /// <returns></returns>
    async UniTask Shake(float duration, float strength)
    {
        Vector3 startPos = Camera.main.transform.position; ;
        float elapsedTime = 0f;
        while(elapsedTime< duration)
        {
            elapsedTime += Time.deltaTime;
            Camera.main.transform.position = startPos + UnityEngine. Random.insideUnitSphere * strength;
            await UniTask.Yield();
        }
        Camera.main.transform.position = startPos;
    }


    public async UniTask AttackProcess(Transform target, int atk)
    {
        float DelayTime = 1f;
        float MoveDuration = 0.5f;

        float origin = transform.position.x; //原点

        transform.DOMoveX(target.position.x + 2f, MoveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            target.gameObject.GetComponent<PlayerBase>().TakeDamage(atk).Forget();
        });


        await UniTask.Delay(TimeSpan.FromSeconds(DelayTime));
        transform.DOMoveX(origin, MoveDuration).SetEase(Ease.OutQuad);//元の位置に戻る
        await UniTask.Yield();
    }

    public async UniTask<bool> SkillTimer(float Cooldown)
    {
        bool isSkillReady = false;
        _skill_Timer = Cooldown;
        while (_skill_Timer > 0)
        {
            _skill_Timer -= Time.deltaTime;
            await UniTask.Yield();
        }
        isSkillReady = true;
        return isSkillReady;
    }
    //制作中
    public virtual void SkillProcess(int CardNum)
    {
       

       // CardList[CardNum].SetCardDebuff(Card.Debuff.Water);
       
    }
    /// <summary>
    /// ダメージ計算関数
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="def"></param>
    /// <param name="callback"></param>
    void DamageCale(int dmg,int def, Action callback)
    {
        Hp = Mathf.Max(Hp - Mathf.Max(dmg - def, 0), 0);

       callback?.Invoke();
    }
}
