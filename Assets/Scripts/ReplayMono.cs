using UnityEngine;
/// <summary>
/// 저장 핵심 오브젝트.
/// 해당 오브젝트의 컴포넌트 중 ChildReplayMono
/// </summary>
public abstract class ReplayMono : MonoBehaviour, IReplayObj
{
    public int IndexId {get;set;}
    public string ObjId {get;set;}

    public virtual void GameUpdate()
    {
    }

    public virtual void Load(SaveData data)
    {
        LocalPosition = data;
        Angle = data;
    }

    public virtual void Save(SaveData data)
    {
        data.Write(LocalPosition);
        data.Write(Angle);
    }
    public abstract void Delete();
    public Vector2 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }
    public Vector2 LocalPosition
    {
        get
        {
            return transform.localPosition;
        }
        set
        {
            transform.localPosition = value;
        }
    }
    /// <summary>
    /// 각도. 0~360. 우측을 0도로 둠
    /// </summary>
    public float Angle
    {
        get
        {
            return transform.eulerAngles.z;
        }
        set
        {
            transform.eulerAngles = new Vector3(0,0,value);
        }
    }
}