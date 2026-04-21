using Cysharp.Threading.Tasks;
using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
            Camera.main.transform.position = startPos + Random.insideUnitSphere * strength;
            await UniTask.Yield();
        }
        transform.position = startPos;
    }





}
