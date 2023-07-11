using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System.Collections;

public class FogController : MonoBehaviour
{
    public Volume volume;
    public float transitionTime = 2f;

    private float initialWeight;
    private PlayerMedapartsController[] playerControllers;

    private void Start()
    {
        initialWeight = volume.weight;
        playerControllers = FindObjectsOfType<PlayerMedapartsController>();
        StartCoroutine(ChangeVolumeWeight());
    }

    private IEnumerator ChangeVolumeWeight()
    {
        volume.weight = 0f;

        while (true)
        {
            bool anyPlayerUsingMedaForce = false;

            foreach (PlayerMedapartsController playerController in playerControllers)
            {
                if (playerController.IsUsingMedaForce())
                {
                    anyPlayerUsingMedaForce = true;
                    break;
                }
            }

            if (anyPlayerUsingMedaForce)
            {
                yield return ChangeWeight(1f, transitionTime);
            }
            else
            {
                yield return ChangeWeight(0f, transitionTime);
            }

            yield return null;
        }
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