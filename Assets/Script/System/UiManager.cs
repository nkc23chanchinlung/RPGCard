using UnityEngine;
using UnityEngine.UI;

//20日ここまで




public class UiManager : MonoBehaviour
{
    [SerializeField] Text _debug_Text;
    [SerializeField] GameObject _dmg_Text_Prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance._isDebugMode)
        {
            _debug_Text.gameObject.SetActive(true);
        }
        else
        {
            _debug_Text.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ダメージ値のテキストを生成する
    /// </summary>
    /// <param name="targer">対象</param>
    /// <param name="dmg">ダメージ値</param>
    public void CreateDmg_Text(GameObject targer,int dmg)
    {
        float uipos = 10f;
        Vector3 worldPos = targer.transform.position + new Vector3(0, uipos, 0);
       // Text _dmg_Text= Instantiate(_dmg_Text_Prefab,targer.transform worldPos, Quaternion.identity).GetComponent<Text>();<--後でやる
       
       // _dmg_Text.text = dmg.ToString();
        
    }
}
