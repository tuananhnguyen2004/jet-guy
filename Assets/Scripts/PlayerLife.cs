using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLife : MonoBehaviour
{
    RocketEngine engine;
    [SerializeField] float safeVel;
    [SerializeField] UnityEvent OnDie;
    [SerializeField] Animator anim;
    bool isDead;
    Rigidbody2D rb;

    private void Awake()
    {
        isDead = false;
        engine = GetComponent<RocketEngine>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (engine.isLanding)
        {
            if (engine.rb.velocity.y < 0)
            {
                if (engine.rb.velocity.magnitude > safeVel)
                {
                    Die();
                }
                
            }
        }

        var landDetection = engine.GetLandDetectionZone();
        var ceilingDetection = engine.GetCeilingDetectionZone();
        if (landDetection)
        {
            if (landDetection.collider.CompareTag("DeadZone"))
                Die();
        }

        if (ceilingDetection)
        {
            if (ceilingDetection.collider.CompareTag("DeadZone"))
            {
                Die();
            }
        }
    }

    void Die()
    {
        if (isDead)
        {
            return;
        }
        Debug.Log("die");
        anim.SetTrigger("dead");
        rb.bodyType = RigidbodyType2D.Static;
        AudioManager.Instance.PlaySound("PlayerDeath");
        isDead = true;
        OnDie.Invoke();
        int lv = LevelManager.Instance.currentLevel;
        FirebaseManager.LogEvent($"Game_Over_{lv}");
    }
}
