using UnityEngine;

public class BattleManager : MonoBehaviour
{

//ダメージ計算関数
float TakeDamage(GameObject target, float _damage)
    {
        EnemyBase enemy= target.GetComponent<EnemyBase>();

        _damage = _damage - enemy.Defense;

        return _damage;

    }    
}
