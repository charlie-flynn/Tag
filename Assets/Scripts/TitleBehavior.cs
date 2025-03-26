using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq.Expressions;

public class TitleBehavior : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.DOLocalRotate(new Vector3(0, 360, 0), 0.5f);
    }
}
