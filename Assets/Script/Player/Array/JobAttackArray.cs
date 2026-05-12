using System;

//職業の攻撃ルールを管理するクラス
[Serializable]
public class JobAttackArray 
{
   public  string SkillName;
    public float AttackPower;
    public enum AtkEffectIndex
    {
        Slash,
        Thunder,
        FireBall,
        Ice,
    }
    public AtkEffectIndex atkEffectIndex;

    public enum AttackPatten
    {
        ShortDistance,
        LongDistance,
        Item,
    }
    public AttackPatten patten; 


   

}
