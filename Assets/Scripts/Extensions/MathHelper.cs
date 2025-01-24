using UnityEngine;

namespace Extensions
{
    public static class MathHelper
    {
        // Functions that returns a vector based on a circle
        public static Vector2 PointOnCircle(int currentPoint, int totalPoints, float radius)
        {
            float angle = 360f / totalPoints * currentPoint;
            float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
            return new Vector2(x, y);
        }
    }
}