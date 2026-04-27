using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
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
    

    private void Awake()
    {
        
    }
    /// <summary>
    /// ダメージ受ける関数
    /// </summary>
    /// <param name="Dmg">受けるダメージ量</param>
    public void TakeDamage(int Dmg)
    {
        Hp -= Dmg;
        
        UiManager.Instance.CreateDmg_Text(transform, Dmg).Forget();
       Shake(0.1f, 0.2f).Forget();


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
        float origin = transform.position.x; //原点

        transform.DOMoveX(target.position.x + 2f, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            target.gameObject.GetComponent<PlayerBase>().TakeDamage(atk).Forget();
        });


        await UniTask.Delay(TimeSpan.FromSeconds(1));
        transform.DOMoveX(origin, 0.5f).SetEase(Ease.OutQuad);//元の位置に戻る
        await UniTask.Yield();
    }





}
