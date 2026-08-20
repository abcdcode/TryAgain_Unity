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
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        var input = InputManager.Instance.InputInfo;
        //방향키 인풋
        var MoveDir = input.MoveDir;
        //이동
        this.transform.Translate(MoveDir.normalized*1000*Time.deltaTime);
        this.Position = CalcUtils.ScreenClamp(this.Position,this.GetSize());
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
    }
    public override void Delete()
    {
    }

    
}