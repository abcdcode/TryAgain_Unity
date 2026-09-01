using UnityEngine;

public class Player : ReplayMono, IHitable
{
    public FactionEnum Faction => FactionEnum.Player;
    public bool IsDead { get; private set; }
    public float HitSize => this.GetSize().x;

    public void TakeDamage(DamageInfo dmg)
    {
        ReplayDebug.Log("Player Hit!!!!");
        if (!IsDead)
        {
            IsDead = true;
            var de = EffectContainer.Instance.Create("PlayerDead",true);
            de.SetSize(new Vector2(100,100));
            de.Position = Position;
        }
    }
    public override void Awake()
    {
        base.Awake();
        this.SetSize(new Vector2(100, 100));
        Stat = new PlayerStat();
        m_CoolTimer.SetCool(AtkCool, Stat.GetAtkSpeed(), 0, true);
    }
    public override void Save(SaveData data)
    {
        base.Save(data);
        data.Write(IsDead);
        data.Write(CurActiveIndex);
    }
    public override void Load(SaveData data)
    {
        base.Load(data);
        IsDead = data;
        gameObject.SetActive(!IsDead);
        CurActiveIndex = data;
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        if (IsDead)
        {
            gameObject.SetActive(false);
            return;
        }
        BulletContainer.HitCheckNew(this);
        var input = InputManager.Instance.InputInfo;
        //방향키 인풋
        var MoveDir = input.MoveDir;
        //이동
        this.Position = CalcUtils.ScreenClamp(this.Position + MoveDir.normalized * Stat.GetMoveSpeed() * Time.deltaTime, this.GetSize());
        var isAtk = input.OnAttack;
        if (isAtk)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        if (!m_CoolTimer.IsCoolComp(AtkCool)) return;
        var b = BulletContainer.Instance.Create(BulletDB.PlayerDefaultBullet, true);
        b.InitPos(this.Position);
        b.Angle = 0;
        b.SetSize(new Vector2(60, 60));
        b.damageInfo = new DamageInfo() { dmg = Stat.GetMainDmg(), faction = FactionEnum.Player };
        m_CoolTimer.SetCool(AtkCool, Stat.GetAtkSpeed(), 0, true);
    }
    public override void Delete()
    {
    }
    public int CurActiveIndex{get;private set;}
    public PlayerStat Stat { get; private set; }
    private const int AtkCool = 1;
}