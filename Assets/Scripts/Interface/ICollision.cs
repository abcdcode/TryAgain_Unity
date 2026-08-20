using UnityEngine;

public interface ICollision
{
    public void OnColEnter(ICollision col);
    public ColShape Shape{get;}
    public ColFilter ColTag{get;}
    public GameMono Obj
    {
        get
        {
            if(this is GameMono g) return g;
            return null;
        }
    }
}
public struct ColShape
{
    public ColShape(Vector2 p1, Vector2 p2, float s)
    {
        point1 = p1;
        point2 = p2;
        size = s;
    }
    public ColShape(Vector2 p, float s)
    {
        point1 = p;
        point2 = p;
        size = s;
    }
    public Vector2 point1;
    public Vector2 point2;
    public float size;
}

public enum ColFilter
{
    Player = 1 << 0, 
    Enemy = 1 << 1,
    Bullet = 1 << 2,
    Effect = 1 << 3,
    Etc1 = 1 << 4,
    Etc2 = 1 << 5,
    Etc3 = 1 << 6,
    None = 1 << 7
}