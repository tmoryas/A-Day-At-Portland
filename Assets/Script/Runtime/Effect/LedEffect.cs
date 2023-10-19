using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedEffect : MonoBehaviour
{
    private void Awake() 
    {
        Sequence s = DOTween.Sequence();

        Tween a = transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f).SetEase(Ease.InBounce);
        Tween b = transform.DOScale(new Vector3(1f, 1f, 1f), 0.6f).SetEase(Ease.InBounce);
        s.SetDelay(Random.Range(0.3f, 0.8f)).Append(a).Append(b).SetLoops(-1);
    } 
}
