using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// プレイヤー基底クラス
/// </summary>
public class PlayerBase : MonoBehaviour
{
    [Header("Status")]

    public float MaxHP;
    public float Hp { get; set; } 


    public async UniTask TakeDamage(int damage)
    {
        Debug.Log("HP:" + Hp);
        Debug.Log("プレイヤーは" + damage + "のダメージを受けた");
        Hp -= damage;
        UiManager.Instance.CreateDmg_Text(transform, damage).Forget();
        Shake(0.1f, 0.2f).Forget();
        await UniTask.Yield();
        // return damage;

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
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Camera.main.transform.position = startPos + UnityEngine.Random.insideUnitSphere * strength;
            await UniTask.Yield();
        }
        Camera.main.transform.position = startPos;
    }

}
