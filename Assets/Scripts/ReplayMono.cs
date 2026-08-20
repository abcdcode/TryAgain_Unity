using UnityEngine;
/// <summary>
/// 저장 핵심 오브젝트.
/// 해당 오브젝트의 컴포넌트 중 ChildReplayMono
/// </summary>
public abstract class ReplayMono : GameMono, IReplayObj, ICoolOwner
{
    public int IndexId {get;set;}
    public string ObjId {get;set;}
    public virtual void Awake()
    {
        m_CoolTimer = new CoolTimer(this);
    }

    public virtual void GameUpdate()
    {
        m_CoolTimer?.GameUpdate();
        Animator?.GameUpdate();
    }

    public virtual void Load(SaveData data)
    {
        Position = data;
        Angle = data;
        Scale = data;
        m_CoolTimer?.Load(data);
        Animator?.Load(data);
    }

    public virtual void Save(SaveData data)
    {
        data.Write(Position);
        data.Write(Angle);
        data.Write(Scale);
        m_CoolTimer?.Save(data);
        Animator?.Save(data);
    }
    public abstract void Delete();

    public virtual void ExecuteCool(int id)
    {
    }
    public virtual void OnDrawGizmos()
    {
        if(GameManager.Instance.IsDebug)
        {
            Gizmos.DrawSphere(Position,this.GetSize().x/2);
        }
    }
    [SerializeField]private ReplayAnimator m_animator;
    public ReplayAnimator Animator => m_animator;
    public CoolTimer m_CoolTimer{get;private set;}

    
}