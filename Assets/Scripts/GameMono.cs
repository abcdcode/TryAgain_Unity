using Unity.VisualScripting;
using UnityEngine;

public class GameMono : MonoBehaviour
{
    public virtual void FixedUpdate()
    {
    }
    public virtual void GameUpdate()
    {
        
    }
    public virtual void LateGameUpdate()
    {
        
    }
    private Vector2 m_curPos = new Vector2(int.MaxValue,int.MaxValue);
    private Vector2 m_Scale = new Vector2(int.MaxValue,int.MaxValue);
    private float m_angle = 99999;
    public Vector2 Position
    {
        get
        {
            if(m_curPos.x == int.MaxValue)
            {
                Position = transform.position;
                return transform.position;
            }
            return m_curPos;
        }
        set
        {
            if(m_curPos == value) return;
            m_curPos = value;
            transform.position = value;
        }
    }
    public virtual Vector2 Scale
    {
        get
        {
            if(m_Scale.x == int.MaxValue)
            {
                Scale = transform.localScale;
                return transform.localScale;
            }
            return m_Scale;
        }
        set
        {
            if(m_Scale == value) return;
            m_Scale = value;
            transform.localScale = value;
        }
    }
    /// <summary>
    /// 각도. 0~360. 우측을 0도로 둠
    /// </summary>
    public virtual float Angle
    {
        get
        {
            if(m_angle == 99999)
            {
                m_angle = transform.eulerAngles.z;
                return transform.eulerAngles.z;
            }
            return m_angle;
        }
        set
        {
            if(m_angle == value) return;
            m_angle = value;
            transform.eulerAngles = new Vector3(0,0,value);
        }
    }
}