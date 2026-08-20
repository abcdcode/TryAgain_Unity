using UnityEngine;

public class GameMono : MonoBehaviour
{
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
    public Vector2 Scale
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