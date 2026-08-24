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
    private Vector2 m_curPos = new Vector2(int.MaxValue,int.MaxValue);
    public Vector2 Position
    {
        get
        {
            if(m_curPos.x == int.MaxValue)
            {
                return transform.position;
            }
            return m_curPos;
        }
        set
        {
            m_curPos = value;
            transform.position = value;
        }
    }
    public virtual Vector2 Scale
    {
        get
        {
            return transform.localScale;
        }
        set
        {
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
            return transform.eulerAngles.z;
        }
        set
        {
            transform.eulerAngles = new Vector3(0,0,value);
        }
    }
}