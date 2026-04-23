using Cysharp.Threading.Tasks;
using UnityEngine;


//職業:魔女クラス
public class Witch : PlayerBase
{
    PlayerController _controller;
    [SerializeField] GameObject _enemy;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hp = MaxHP;
       _controller.SelectCard();
        if (Input.GetKeyDown(KeyCode.A))
        {
           _controller.AttackProcess(_enemy.transform, 10, 1).Forget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
