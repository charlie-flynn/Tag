using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq.Expressions;

public class TitleBehavior : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.DOLocalRotate(new Vector3(0, 0, 15), 1.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
