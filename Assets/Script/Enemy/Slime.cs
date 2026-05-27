using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


//制作中
public sealed class Slime : EnemyBase
{
    bool _isAttacking; // 攻撃中かどうかのフラグ
    [SerializeField] GameObject _waterBall_Pre; // 水の玉のプレハブ
    List<Card> _card;
    [SerializeField] Image _skill_gauge;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();

    }
    private void OnEnable()
    {
        try
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        } catch { 
        Debug.LogError("PlayerBaseコンポーネントが見つかりませんでした。PlayerオブジェクトにPlayerBaseコンポーネントがアタッチされていることを確認してください。");
        }
    }
    // Update is called once per frame
    void Update()
    {




        CardList = CardManager.Instance.GetCardList();

        if (Input.GetKeyDown(KeyCode.J))
            SkillProcess(Random.Range(0, CardList.Count));


    }

    //カードを目標に向かって投げる処理
    void AttackCard(Transform Target)
    {
        _card = CardManager.Instance.GetCardList();

    }
    public override void SkillProcess(int CardNum)
    {
      
        base.SkillProcess(CardNum);
        //水玉移動プロセス
        GameObject _waterball = Instantiate(_waterBall_Pre, transform.position, Quaternion.identity);
        WaterBall waterBallScript = _waterball.GetComponent<WaterBall>();
        waterBallScript.Target = CardList[CardNum].gameObject;



      
    }
  

}
