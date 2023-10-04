namespace Chillplay.Tools
{
    using UnityEngine;

    public static class Vector3Extensions
    {
        public static Vector3 RandomVector(this Vector3 vector, float offset)
        {
            float x = Random.Range(vector.x - offset, vector.x + offset);
            float y = Random.Range(vector.y - offset, vector.y + offset);
            float z = Random.Range(vector.z - offset, vector.z + offset);

            return new Vector3(x, y, z);
        }
    }
}