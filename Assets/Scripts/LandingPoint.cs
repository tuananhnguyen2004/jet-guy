using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPoint : MonoBehaviour
{
    [SerializeField] GameObject landingPoint;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!anim.GetBool("isOn"))
            anim.SetBool("isOn", true);
    }

    void OnCollisionStay2D(Collision2D collision)
    {

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("isOn", false);
    }
}
