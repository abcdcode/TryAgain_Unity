using UnityEngine;

public class Player : ReplayMono
{
    public override void GameUpdate()
    {
        base.GameUpdate();
        var input = InputManager.m_Instance.InputInfo;
        //방향키 인풋
        var MoveDir = input.MoveDir;
        
        //이동
        this.transform.Translate(MoveDir.normalized*5*Time.deltaTime);
    }
    public override void Delete()
    {
    }
}