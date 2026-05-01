using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// エフェクト実装クラス
/// </summary>
public class EffectManager : MonoBehaviour
{
    //シングルトン
    static public EffectManager Instance;
    
    private void Awake()
    {
      Instance = this;
    }
    [SerializeField] GameObject FirePre;

    public async UniTask InstanceFireEffect(Transform target)
    {
        GameObject effect = Instantiate(FirePre, target.position, Quaternion.identity);
        effect.transform.SetParent(target);
        await UniTask.Delay(500);
        Destroy(effect);
    }

}
