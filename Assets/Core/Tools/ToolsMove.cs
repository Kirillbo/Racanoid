
using UnityEngine;

namespace Core.Tools
{
    public class ToolsMove {

        public static Vector3 SmoothStart(Vector3 a, Vector3 b, float time)
        {
            Vector3 pos = a + time * (b - a);
            return pos;
        }

        public static Vector3  SmoothStart2(Vector3 from, Vector3 to, float time)
        {
            return from + time * time * (to - from);
        }

        public static Vector3 SmoothStart3(Vector3 from, Vector3 to, float time)
        {
            return from + time * time * time * (to - from);
        }


        public static Vector3 SmoothStart4(Vector3 from, Vector3 to, float time)
        {
            return from + time * time * time * time * (to - from);
        }

        public static Vector3 SmoothStart5(Vector3 from, Vector3 to, float time)
        {
            return from + time * time * time * time * time * (to - from);
        }

        public static Vector3 SmoothStop2(Vector3 a, Vector3 b, float time)
        {
            Vector3 p = a + (1 - (1 - time) * (1 - time)) * (b - a);
            return p;
        }



        public static Vector3 SmoothStop3(Vector3 a, Vector3 b, float time)
        {
            Vector3 p = a + (1 - (1 - time) * (1 - time) * (1 - time)) * (b - a);
            return p;
        }

        public static Vector3 SmoothStop4(Vector3 a, Vector3 b, float time)
        {
            Vector3 p = a + (1 - (1-time) * (1-time) * (1 - time) * (1-time)) * (b - a);
            return p;
        }

        public static Vector3 SmoothStop5(Vector3 a, Vector3 b, float time)
        {
            Vector3 p = a + (1 - (1 - time) * (1 - time) * (1 - time) * (1 - time) * (1-time)) * (b - a);
            return p;
        }
    }
}
