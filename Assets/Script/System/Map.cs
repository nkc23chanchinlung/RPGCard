using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class Map : MonoBehaviour
{
    public int _nowPoint = 0; //現在のポイント
    enum MapPoint
    {
        Null,
        Start,
        Chest,
        Enemy,
        Boss,
        Goal
    }
    [SerializeField]
    Sprite[] point_Sprite; //0:旗 1:宝箱 2:敵 3:ボス 

    [SerializeField] GameObject[] _point;
    [SerializeField] GameObject navigation;




    void Start()
    {
        RandomMap();

      
    }

     void RandomMap()
    {
        for (int i = 0; i < _point.Length; i++)
        {
            Image img = _point[i].GetComponent<Image>();
            img.sprite = point_Sprite[Random.Range(0, point_Sprite.Length)];
        }



    }


    /// <summary>
    /// ナビゲーションのマーク移動させる関数
    /// </summary>
    /// <param name="Moveto">移動させる位置</param>
    /// <returns></returns>
    async UniTask MoveNav(int Moveto)
    {
        if (_nowPoint != Moveto)
        {
            navigation.transform.DOMove(_point[_nowPoint].transform.position, 0.5f).SetEase(Ease.InOutSine);
            await UniTask.Delay(500);
            _nowPoint++;
        }
    }
  
  
     
}
   
