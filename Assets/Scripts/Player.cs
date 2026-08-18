using UnityEngine;

public class Player : ReplayMono
{
    public override void GameUpdate()
    {
        base.GameUpdate();
        var input = InputManager.Instance.InputInfo;
        //방향키 인풋
        var MoveDir = input.MoveDir;
        //이동
        this.transform.Translate(MoveDir.normalized*5*Time.deltaTime);
        var isAtk = input.OnAttack;
        if(isAtk)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        var b = BulletContainer.Instance.Create(BulletDB.PlayerDefaultBullet,true);
        b.Position = this.Position;
        b.Angle = 0;
    }
    public override void Delete()
    {
    }
    
}