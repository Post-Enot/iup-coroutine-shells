using System.Collections;
using UnityEngine;

namespace CoroutineShells
{
    public static class CoroutineUtils
    {
        public static IEnumerator V2LerpRoutine(Transform transform, Vector2 finalPosition, float speedUnitPerSecond)
        {
            Vector2 startPosition = transform.position;
            float factor = CalculateV2LerpTimeFactor(startPosition, finalPosition, speedUnitPerSecond);
            float time = 0;
            do
            {
                time += Time.deltaTime * factor;
                transform.position = Vector2.Lerp(startPosition, finalPosition, time);
                yield return null;
            }
            while (time < 1);
        }

        public static float CalculateV2LerpTimeFactor(Vector2 startPosition, Vector2 finalPosition, float speedUnitPerSecond)
        {
            float distance = Vector2.Distance(startPosition, finalPosition);
            float factor = distance / speedUnitPerSecond;
            return 1 / factor;
        }

        public static IEnumerator V3LerpRoutine(Transform transform, Vector3 finalPosition, float speedUnitPerSecond)
        {
            Vector3 startPosition = transform.position;
            float factor = CalculateV3LerpTimeFactor(startPosition, finalPosition, speedUnitPerSecond);
            float time = 0;
            do
            {
                time += Time.deltaTime * factor;
                transform.position = Vector3.Lerp(startPosition, finalPosition, time);
                yield return null;
            }
            while (time < 1);
        }

        public static float CalculateV3LerpTimeFactor(Vector3 startPosition, Vector3 finalPosition, float speedUnitPerSecond)
        {
            float distance = Vector3.Distance(startPosition, finalPosition);
            float factor = distance / speedUnitPerSecond;
            return 1 / factor;
        }
    }
}
