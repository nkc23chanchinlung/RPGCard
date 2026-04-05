using UnityEngine;

//ゲーム管理クラス
public class GameManager : MonoBehaviour
{
    public bool _isDebugMode = false; //デバッグモードかどうか
    bool _isBattle = false; //戦闘中かどうか
    bool _isGameOver = false; //ゲームオーバーかどうか
    int _round = 0; //現在のラウンド数

   public static GameManager instance; //シングルトンインスタンス
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
