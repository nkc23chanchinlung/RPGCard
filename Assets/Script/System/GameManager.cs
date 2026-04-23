using UnityEngine;
using UnityEngine.UI;
//ゲーム管理クラス
public class GameManager : MonoBehaviour
{
    [SerializeField] Toggle _debug_Mode_Toggle; //デバッグモードのトグル
    public bool _isDebugMode = false; //デバッグモードかどうか
    bool _isBattle = false; //戦闘中かどうか
    bool _isGameOver = false; //ゲームオーバーかどうか
    int _round = 0; //現在のラウンド数

    

   public static GameManager Instance; //シングルトンインスタンス
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheakGameManagerExist();
    }
    private void FixedUpdate()
    {
        _isDebugMode = _debug_Mode_Toggle.isOn;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void CheakGameManagerExist()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    
}
