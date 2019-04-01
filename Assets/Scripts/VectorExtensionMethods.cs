using UnityEngine;

public static class VectorExtensionMethods
{
    public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
    {
        Vector2 result = new Vector2(x ?? original.x, y ?? original.y);
        return result;
    }
}
