using UnityEngine;

/// <summary>
/// Static helper for drawing gizmos to visualize casts and shapes.
/// Shapes are drawn with a solid outline and a transparent "glass" fill.
/// Call these from OnDrawGizmos / OnDrawGizmosSelected.
/// </summary>
public static class GizmoDrawer
{
    private const float FillAlpha = 0.25f;

    // ---------- RAY / LINE ----------

    public static void DrawRay(Vector3 origin, Vector3 direction, float distance, Color? color = null)
    {
        WithColor(color, () =>
        {
            Vector3 end = origin + direction.normalized * distance;
            Gizmos.DrawLine(origin, end);
            Gizmos.DrawSphere(end, 0.03f);
        });
    }

    // ---------- SPHERE CAST ----------

    public static void DrawSphereCast(Vector3 origin, float radius, Vector3 direction, float distance, Color? color = null)
    {
        WithColor(color, () =>
        {
            direction = direction.normalized;
            Vector3 end = origin + direction * distance;

            DrawFilledSphere(origin, radius);
            DrawFilledSphere(end, radius);

            // connecting lines along the cast (like a capsule silhouette)
            Vector3 right = Vector3.Cross(direction, Vector3.up);
            if (right.sqrMagnitude < 0.001f) right = Vector3.Cross(direction, Vector3.forward);
            right.Normalize();
            Vector3 up = Vector3.Cross(right, direction).normalized;

            Gizmos.color = FullAlpha(Gizmos.color);
            Gizmos.DrawLine(origin + right * radius, end + right * radius);
            Gizmos.DrawLine(origin - right * radius, end - right * radius);
            Gizmos.DrawLine(origin + up * radius, end + up * radius);
            Gizmos.DrawLine(origin - up * radius, end - up * radius);
        });
    }

    // ---------- BOX CAST ----------

    public static void DrawBoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, float distance, Quaternion orientation = default, Color? color = null)
    {
        if (orientation == default) orientation = Quaternion.identity;

        WithColor(color, () =>
        {
            direction = direction.normalized;
            Vector3 end = center + direction * distance;

            DrawFilledBox(center, halfExtents, orientation);
            DrawFilledBox(end, halfExtents, orientation);

            // connect the 8 corners
            Gizmos.color = FullAlpha(Gizmos.color);
            for (int i = 0; i < 8; i++)
            {
                Vector3 corner = GetBoxCorner(i, halfExtents);
                Vector3 startCorner = center + orientation * corner;
                Vector3 endCorner = end + orientation * corner;
                Gizmos.DrawLine(startCorner, endCorner);
            }
        });
    }

    private static Vector3 GetBoxCorner(int index, Vector3 halfExtents)
    {
        float x = (index & 1) == 0 ? -halfExtents.x : halfExtents.x;
        float y = (index & 2) == 0 ? -halfExtents.y : halfExtents.y;
        float z = (index & 4) == 0 ? -halfExtents.z : halfExtents.z;
        return new Vector3(x, y, z);
    }

    // ---------- CAPSULE CAST ----------

    public static void DrawCapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance, Color? color = null)
    {
        WithColor(color, () =>
        {
            direction = direction.normalized;
            Vector3 offset = direction * distance;

            DrawCapsule(point1, point2, radius);
            DrawCapsule(point1 + offset, point2 + offset, radius);

            Vector3 axis = (point2 - point1).normalized;
            Vector3 right = Vector3.Cross(axis, Vector3.up);
            if (right.sqrMagnitude < 0.001f) right = Vector3.Cross(axis, Vector3.forward);
            right.Normalize();

            Gizmos.color = FullAlpha(Gizmos.color);
            Gizmos.DrawLine(point1 + right * radius, point1 + offset + right * radius);
            Gizmos.DrawLine(point1 - right * radius, point1 + offset - right * radius);
            Gizmos.DrawLine(point2 + right * radius, point2 + offset + right * radius);
            Gizmos.DrawLine(point2 - right * radius, point2 + offset - right * radius);
        });
    }

    private static void DrawCapsule(Vector3 point1, Vector3 point2, float radius)
    {
        DrawFilledSphere(point1, radius);
        DrawFilledSphere(point2, radius);

        Vector3 axis = (point2 - point1).normalized;
        Vector3 right = Vector3.Cross(axis, Vector3.up);
        if (right.sqrMagnitude < 0.001f) right = Vector3.Cross(axis, Vector3.forward);
        right.Normalize();
        Vector3 up = Vector3.Cross(right, axis).normalized;

        Gizmos.color = FullAlpha(Gizmos.color);
        Gizmos.DrawLine(point1 + right * radius, point2 + right * radius);
        Gizmos.DrawLine(point1 - right * radius, point2 - right * radius);
        Gizmos.DrawLine(point1 + up * radius, point2 + up * radius);
        Gizmos.DrawLine(point1 - up * radius, point2 - up * radius);
    }

    // ---------- PLAIN SHAPES ----------

    public static void DrawSphere(Vector3 center, float radius, Color? color = null)
        => WithColor(color, () => DrawFilledSphere(center, radius));

    public static void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation = default, Color? color = null)
    {
        if (orientation == default) orientation = Quaternion.identity;
        WithColor(color, () => DrawFilledBox(center, halfExtents, orientation));
    }

    // ---------- FILL + OUTLINE HELPERS ----------

    private static void DrawFilledSphere(Vector3 center, float radius)
    {
        Color baseColor = Gizmos.color;

        Gizmos.color = FullAlpha(baseColor);
        Gizmos.DrawWireSphere(center, radius);

        Gizmos.color = LowAlpha(baseColor);
        Gizmos.DrawSphere(center, radius);

        Gizmos.color = baseColor;
    }

    private static void DrawFilledBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
    {
        Color baseColor = Gizmos.color;
        Matrix4x4 oldMatrix = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(center, orientation, Vector3.one);

        Gizmos.color = FullAlpha(baseColor);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f);

        Gizmos.color = LowAlpha(baseColor);
        Gizmos.DrawCube(Vector3.zero, halfExtents * 2f);

        Gizmos.matrix = oldMatrix;
        Gizmos.color = baseColor;
    }

    private static Color FullAlpha(Color c) => new Color(c.r, c.g, c.b, 1f);
    private static Color LowAlpha(Color c) => new Color(c.r, c.g, c.b, FillAlpha);

    // ---------- UTIL ----------

    private static void WithColor(Color? color, System.Action drawAction)
    {
        Color old = Gizmos.color;
        if (color.HasValue) Gizmos.color = color.Value;
        drawAction();
        Gizmos.color = old;
    }
}