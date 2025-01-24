using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 AsXZ(this Vector3 v3) => new Vector2(v3.x, v3.z);
        public static Vector3 AsXZ(this Vector2 v2) => new Vector3(v2.x, 0f, v2.y);
        
        public static Vector3 AsXY(this Vector3 v3) => new Vector3(v3.x, v3.y, 0f);
        
        public static Vector3 ZeroY(this Vector3 v3) => new Vector3(v3.x, 0f, v3.z);
    }
}