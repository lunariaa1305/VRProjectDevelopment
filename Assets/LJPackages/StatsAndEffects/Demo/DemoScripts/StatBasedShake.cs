using UnityEngine;

namespace LJ.Stats.Demos
{
    public class StatBasedShake : MonoBehaviour
    {
        public float maxShakeAmount = 0.5f;  // maximum offset in units
        public float shakeSpeed = 20f;       // how fast the shake oscillates

        Vector3 originalPosition;
        float currentShake = 0f;

        void Start()
        {
            originalPosition = transform.localPosition;
        }

        public void ReceiveStat(float normalizedValue)
        {
            // clamp to [0,1]
            currentShake = Mathf.Clamp01(normalizedValue);
        }

        void FixedUpdate()
        {
            if (currentShake > 0f)
            {
                float shakeAmount = maxShakeAmount * currentShake;
                Vector3 shakeOffset = new Vector3(
                    Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f,
                    Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f,
                    0f
                ) * 2f * shakeAmount;

                transform.localPosition = originalPosition + shakeOffset;
            }
            else
            {
                transform.localPosition = originalPosition;
            }
        }
    }

}
