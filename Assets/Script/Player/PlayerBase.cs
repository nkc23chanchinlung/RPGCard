using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// プレイヤー基底クラス
/// </summary>
public class PlayerBase : MonoBehaviour
{
    [Header("Status")]

    public float MaxHP;
    public int Attack;
    public float Hp { get; set; }
    Animator _animator;
    [SerializeField] Sprite[] _cardList; //カードリスト
    [SerializeField] GameObject[] _attackEffect;//0:斬撃 //1:サンダー
    public Image _hpBar; //HPバー
    //職業ごとの攻撃ルールを管理するリスト(読み込み専用)
    public List<JobAttackArray> _jobAttackArrays = new List<JobAttackArray>();



    public async UniTask TakeDamage(int damage)
    {
    　　float duration = 0.1f;
        float strength = 0.2f;

        Debug.Log("HP:" + Hp);
        Debug.Log("プレイヤーは" + damage + "のダメージを受けた");
        Hp -= damage;
        UiManager.Instance.CreateDmg_Text(transform, damage).Forget();
        Shake(duration, strength).Forget();
        _hpBar.fillAmount = Hp / MaxHP;
        if(Hp <= 0)
        {
            Hp = 0;
           
            Debug.Log("プレイヤーは倒れた");
            //ゲームオーバー処理
        }
        await UniTask.Yield();
       

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
    /// <summary>
    /// 攻撃アニメーション開始関数
    /// </summary>
    /// <param name="obj">ゲームオブジェクト</param>
    public void SetAttackTrue(GameObject obj)
    {
        //Animator _animator;
       _animator = obj.GetComponent<Animator>();
        _animator.SetBool("IsAttack", true);
    }
    /// <summary>
    /// 攻撃アニメーション終了関数
    /// </summary>
    public void SetAttackFalse()
    {
        Debug.Log("攻撃アニメーション終了");
        _animator = this.gameObject.GetComponent<Animator>();

        _animator.SetBool("IsAttack", false);
    }
    /// <summary>
    /// カードリストを取得する関数
    /// </summary>
    /// <returns></returns>
    public Sprite[] GetCardList()
    {
        return _cardList;
    }
    public GameObject[] GetAttackEffect()
    {
        return _attackEffect;
    }
}
