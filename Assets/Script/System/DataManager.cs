using UnityEngine;
using System.IO;

//共通のデータを管理するクラス    
public class DataManager : MonoBehaviour
{
    public static DataManager Instance; //シングルトンインスタンス

    SaveDate saveDate;
    public int _sameCardValue { get; set; } = 0; //同じカードの値を管理するプロパティ
    public int _killedMonsterValus { get; set; } //倒したモンスターの値を管理する変数
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            saveDate = new SaveDate();
            saveDate.pathName = "Save";
            saveDate.path = "/SaveData/";
            string serialisedDataJson = JsonUtility.ToJson(saveDate);
            string baseDirectory = Path.GetDirectoryName(Application.dataPath);
            string filePath = Path.Combine(baseDirectory, "Save.json");
            File.WriteAllText(filePath, SaveData());
            Debug.Log("保存した");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            saveDate.pathName = "Save";
            saveDate.path = "/SaveData/";
            //File.ReadAllText()

        }
    }
   
    string SaveData()
    {
        return "killedMonsterValus:"+_killedMonsterValus.ToString();
    }
    //string LoadData(string path)
    //{
    //    saveDate = new SaveDate();
       
    //    return JsonUtility.FromJson<SaveDate>()
    //}
}
public class SaveDate
{
    public string path;
    public string pathName;
}
