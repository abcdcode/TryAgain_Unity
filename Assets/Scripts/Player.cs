using UnityEngine;

public class Player : ReplayMono, IHitable
{
    public FactionEnum Faction => FactionEnum.Player;

    public float HitSize => this.GetSize().x;

    public void TakeDamage(DamageInfo dmg)
    {
        
    }
    public override void Awake()
    {
        base.Awake();
        this.SetSize(new Vector2(100,100));
        Stat = new CharacterStat();
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        BulletContainer.HitCheckNew(this);
        var input = InputManager.Instance.InputInfo;
        //방향키 인풋
        var MoveDir = input.MoveDir;
        //이동
        this.Position = CalcUtils.ScreenClamp(this.Position+MoveDir.normalized*1000*Time.deltaTime,this.GetSize());
        var isAtk = input.OnAttack;
        if(isAtk)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        var b = BulletContainer.Instance.Create(BulletDB.PlayerDefaultBullet,true);
        b.InitPos(this.Position);
        b.Angle = 0;
        b.SetSize(new Vector2(100,100));
        b.Faction = FactionEnum.Player;
        b.damageInfo = new DamageInfo(){dmg = 10, faction = FactionEnum.Player};
    }
    public override void Delete()
    {
    }
    public CharacterStat Stat{get;private set;}
}