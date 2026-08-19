using UnityEngine;

public static class CalcUtils
{
    public static Vector2 ScreenClamp(Vector2 position, Vector2 size)
    {
        float halfX = size.x * 0.5f;
        float halfY = size.y * 0.5f;

        position.x = Mathf.Clamp(position.x, -960 + halfX, 960 - halfX);
        position.y = Mathf.Clamp(position.y, -540 + halfY, 540 - halfY);

        return position;
    }
    public static void SetSize(this ReplayMono obj, Vector2 size)
    {
        SetSize(obj.GetComponent<SpriteRenderer>(),size);
    }
    public static void SetSize(this GameObject obj, Vector2 size)
    {
        SetSize(obj.GetComponent<SpriteRenderer>(),size);
    }
    public static void SetSize(this SpriteRenderer renderer, Vector2 size)
    {
        var max = Mathf.Max(renderer.sprite.bounds.size.x, renderer.sprite.bounds.size.y);
        Vector2 spriteSize = new Vector2(max,max);
        //Debug.Log($"spriteSize {spriteSize}");
        //float ppu = renderer.sprite.pixelsPerUnit;
        //Vector2 scale1Size = spriteSize/ppu;
        //Debug.Log($"scale1Size {scale1Size}");
        renderer.transform.localScale = new Vector2(size.x/spriteSize.x,size.y/spriteSize.y);
        //Debug.Log($"localScale {renderer.transform.localScale}");
    }
    public static Vector2 GetSize(this ReplayMono obj)
    {
        return GetSize(obj.GetComponent<SpriteRenderer>());
    }
    public static Vector2 GetSize(this GameObject obj)
    {
        return GetSize(obj.GetComponent<SpriteRenderer>());
    }
    public static Vector2 GetSize(this SpriteRenderer renderer)
    {
        Vector2 spriteSize = renderer.sprite.bounds.size;
        return new Vector2(renderer.transform.localScale.x * spriteSize.x, renderer.transform.localScale.y * spriteSize.y);
    }
}