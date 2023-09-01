using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction, Vector2 size, float distance = 1f)
    {
        if (rigidbody.isKinematic)
            return false;

        RaycastHit2D hit = Physics2D.BoxCast(rigidbody.position, size,0f, direction, distance, layerMask);
        return hit.collider && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
