using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System.Collections;

public class FogController : MonoBehaviour
{
    public Volume volume;
    public float targetWeight = 1f;
    public float transitionTime = 2f;

    private float initialWeight;

    private void Start()
    {

        initialWeight = volume.weight;
        StartCoroutine(ChangeVolumeWeight());
    }

    private IEnumerator ChangeVolumeWeight()
    {
        yield return ChangeWeight(targetWeight, transitionTime);
        yield return ChangeWeight(0f, transitionTime);
    }

    private IEnumerator ChangeWeight(float target, float duration)
    {
        float elapsedTime = 0f;
        float initial = volume.weight;

        while (elapsedTime < duration)
        {
            float normalizedTime = elapsedTime / duration;
            float newWeight = Mathf.Lerp(initial, target, normalizedTime);

            volume.weight = newWeight;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        volume.weight = target;
    }
}