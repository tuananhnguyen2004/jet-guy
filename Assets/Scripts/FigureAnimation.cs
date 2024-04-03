using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Awake()
    {
        MakeAppear();
    }

    void MakeAppear()
    {
        anim.SetTrigger("Appear");
    }
}
