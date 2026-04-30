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
   
        Attack=10;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hp = MaxHP;
       _controller.SelectCard();
     
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
   
}
