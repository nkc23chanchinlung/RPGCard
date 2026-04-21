using UnityEngine;

//共通のデータを管理するクラス    
public class DataManager : MonoBehaviour
{
    public int _sameCardValue { get; set; } = 0; //同じカードの値を管理するプロパティ
    public int _killedMonsterValus { get; set; } //倒したモンスターの値を管理する変数
    public static DataManager Instance; //シングルトンインスタンス
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
        Debug.Log("DataManagerが呼び出した");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
