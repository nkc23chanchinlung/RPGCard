using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
//戦闘管理クラス
public class BattleManager : MonoBehaviour
{

    int _stageNum;
    public int ItemNum { set; get; }
    List<EnemyBase> _enemyList; //戦闘中の敵リスト
    static public BattleManager Instance;

    DataManager datemanager;

   public void AddEnemyList(EnemyBase enemy)
    {
        _enemyList.Add(enemy);
    }


}

