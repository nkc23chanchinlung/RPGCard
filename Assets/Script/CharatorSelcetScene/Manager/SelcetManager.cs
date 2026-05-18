using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelcetManager : MonoBehaviour
{
    [SerializeField] GameObject spotlight;
    [SerializeField] float scale;
    [SerializeField] GameObject[] charactorObj;
    [SerializeField] GameObject _unMask;
    [SerializeField] GameObject _mask;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameStart == true) return;
        int sum = CharatorSelcet();
        if (sum >= 0)
        {

            spotlight.transform.position = charactorObj[sum].transform.position;
        }
    }
    Vector3 MouseCol()
    {
        Vector3 mousepos = Input.mousePosition;
        
        var worldmousepos=Camera.main.ScreenToWorldPoint(mousepos);
        worldmousepos.z = -10f;
        return worldmousepos;
    }
    int CharatorSelcet()
    {
        Vector3 mos = MouseCol();
       
       

        for (int i = 0; i < charactorObj.Length; i++)
        {
            float sizeX = Mathf.Abs(charactorObj[i].transform.localScale.x) / 2;
            float sizeY = Mathf.Abs(charactorObj[i].transform.localScale.y) / 2;
            float x1 = charactorObj[i].transform.position.x - sizeX * scale;
            float x2 = charactorObj[i].transform.position.x + sizeX * scale;
            float y1 = charactorObj[i].transform.position.y - sizeY * scale;
            float y2 = charactorObj[i].transform.position.y + sizeY * scale;
            /***************************Debug用の当たり判定表示***************************/
            Debug.DrawLine(new Vector3(x1, y2, 0), new Vector3(x2, y2, 0), Color.red);
            Debug.DrawLine(new Vector3(x1, y1, 0), new Vector3(x2, y1, 0), Color.red);
            Debug.DrawLine(new Vector3(x1, y1, 0), new Vector3(x1, y2, 0), Color.red);
            Debug.DrawLine(new Vector3(x2, y1, 0), new Vector3(x2, y2, 0), Color.red);
            if (mos.x > x1 && mos.x < x2 && mos.y > y1 && mos.y < y2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameManager.Instance.PlayingCharactorId = i;
                    GameManager.Instance.GameStart = true;
                    _mask.SetActive(true);
                    Vector3 worldpos = Camera.main.WorldToScreenPoint(charactorObj[i].transform.position);
                    worldpos.z = 0;
                    _unMask.transform.position= worldpos;
                    UnMaskEffect().Forget();
                }
                return i;
            }
           
        }
        return -1;
    }
    async UniTask UnMaskEffect()
    {
        _unMask.transform.DOScale(Vector3.zero, 2f).OnComplete(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }) 
        ;
        await UniTask.Yield();
    }
}
