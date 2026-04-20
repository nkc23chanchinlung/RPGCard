using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
//戦闘管理クラス
public class BattleManager : MonoBehaviour
{

/// <summary>
/// ダメージ計算関数
/// </summary>
/// <param name="target">対象</param>
/// <param name="_damage">ダメージ量</param>
/// <returns></returns>
float TakeDamage(GameObject target, float _damage)
    {
        EnemyBase enemy= target.GetComponent<EnemyBase>();

        _damage = _damage - enemy.Defense;

        return _damage;

    }
    
   

}

