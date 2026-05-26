using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 敵攻撃:水玉クラス
/// </summary>
public class WaterBall : MonoBehaviour
{
    public GameObject Target { get; set; }
    [SerializeField] int _speed;
    [SerializeField] int rotateSpeed;   
    Rigidbody2D rb;
    Animator _animator;
    bool _isHit=false;

    private void FixedUpdate()
    {
        
        float Distance = Vector2.Distance(transform.position, Target.transform.position);
        float absDistance = Mathf.Abs(Distance);
        if (absDistance < 1f&&!_isHit)
        {
            _isHit = true;
            HitPocess();
        }
       
            
            WaterballShoot(transform.gameObject, _speed, Target.transform);
    }
    /// <summary>
    /// 当たった後の処理
    /// </summary>
    void HitPocess()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic; // 物理挙動を停止

        transform.localEulerAngles = Vector3.zero;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x-0.5f, pos.y-1.3f, pos.z); // Z軸を0に固定

        _animator = GetComponent<Animator>();
        _animator.SetBool("IsHit", _isHit);
        Card card = Target.GetComponent<Card>();
        card.SetCardDebuff(Card.Debuff.Water);
        Destroy(gameObject, 0.5f);
        
    }

    /// <summary>
    /// 水玉をターゲットに向かって放つ関数
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="speed"></param>
    /// <param name="target"></param>
    public void WaterballShoot(GameObject obj, int speed, Transform target)
    {
        if (_isHit) return;

        // 1. ターゲットへの相対ベクトルを計算
        Vector2 diff = target.transform.position - transform.position;
         rb = obj.GetComponent<Rigidbody2D>();
        Vector2 vel = rb.linearVelocity;
        if (vel.magnitude > 0.1f)
        {
            // 速度ベクトルから目標の角度（度数法）を計算
            float targetAngle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

            // クォータニオンに変換
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // なめらかに回転を補間
            Quaternion nextRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);

            // Rigidbody2D経由で回転を適用
            rb.MoveRotation(nextRotation);
        }

        float x = diff.x; // 水平距離
        float y = diff.y; // 垂直高低差

        // 2. 物理パラメータの取得
        float v = speed;
        float g = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale); // 実際の重力値

        // 3. ルート（根）の中身を計算
        float root = (v * v * v * v) - g * (g * x * x + 2 * y * v * v);

        if (root < 0)
        {
            Debug.LogWarning("届かない位置、または速度が足りません。");
            return;
        }

        // 4. 角度 θ（2つの解のうち、低い軌道の方）を計算
        float tanTheta = ((v * v) - Mathf.Sqrt(root)) / (g * x);
        float theta = Mathf.Atan(tanTheta);

        // 補正：ターゲットが左側にいる場合は角度を反転
        if (x < 0)
        {
            theta += Mathf.PI;
        }

        // 5. 初速ベクトルを計算
        Vector2 velocity = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * v;

        // 6. 力を加える (Impulseモードなら「質量 × 速度ベクトル」)
       
        rb.linearVelocity = Vector2.zero; // 事前に速度をリセット（Unity2023以降はlinearVelocity、旧バージョンはvelocity）
        rb.AddForce(rb.mass * velocity, ForceMode2D.Impulse);


    }
}
