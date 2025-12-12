using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private void Start()
    {
        if(instance == null)
            instance = this;
    }
    /// <summary>
    /// Causes camera to shake when hit
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    /// <returns></returns>
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;//this waits until the next frame
        }

        transform.localPosition = originalPos;//returns cam position to OG position
    }
}
