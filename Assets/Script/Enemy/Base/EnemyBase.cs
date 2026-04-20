using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float MaxHP;
    public float Hp;
    public int Attack;
    public float Speed;
    public int Defense;
    public int Level;

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
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
