using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTransition : MonoBehaviour
{

    public AudioSource ExplorationTrack, CombatTrack;
    public float FadeTime;
    private bool _isCombat;
    // Start is called before the first frame update
    void Start()
    {
        _isCombat = false;
    }

    public void SwapTrack(bool _combatHappening)
    {
        StopAllCoroutines();
        _isCombat = _combatHappening;
        StartCoroutine(FadeToTrack());
    }

    private IEnumerator FadeToTrack()
    {
        float timeElapsed = 0;

        if(_isCombat)
        {
            CombatTrack.Play();
            while(timeElapsed<FadeTime)
            {
                CombatTrack.volume = Mathf.Lerp(0, 1, timeElapsed / FadeTime);
                ExplorationTrack.volume = Mathf.Lerp(1, 0, timeElapsed / FadeTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            ExplorationTrack.Stop();
        }
        else
        {
            ExplorationTrack.Play();
            while (timeElapsed < FadeTime)
            {
                ExplorationTrack.volume = Mathf.Lerp(0, 1, timeElapsed / FadeTime);
                CombatTrack.volume = Mathf.Lerp(1, 0, timeElapsed / FadeTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            CombatTrack.Stop();
        }
    }
}

