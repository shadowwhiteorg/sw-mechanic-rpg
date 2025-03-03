using UnityEngine;

namespace _Game.Utils
{
    public static class ExtensionMethods
    {
        public static float Map(this float f, float inMin, float inMax, float outMin, float outMax)
        {
            float t = Mathf.Clamp01((f - inMin) / (inMax - inMin));
            return Mathf.Lerp(outMax, outMin, t);
        }
    }
}