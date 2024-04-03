using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particles;
    RocketEngine engine;
    Camera cam;

    void Awake()
    {
        engine = transform.parent.GetComponent<RocketEngine>();
        cam = Camera.main;
    }

    void Start()
    {
        StopEffect();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch first = Input.GetTouch(0);

            if (!engine.isFirstJumpTouched && !engine.isLanding)
            {
                float xFirstPos = cam.ScreenToWorldPoint(first.position).x - cam.transform.position.x;
                if (xFirstPos <= 0)
                {
                    PlayEffect(0);
                }
                else
                    PlayEffect(1);

                if (Input.touchCount > 1)
                {
                    Touch second = Input.GetTouch(1);

                    float xSecondPos = Camera.main.ScreenToWorldPoint(second.position).x;
                    if (xFirstPos * xSecondPos <= 0)
                    {
                        for (int i = 0; i < particles.Count; ++i)
                        {
                            PlayEffect(i);
                        }
                    }
                }
            }
            if (first.phase == TouchPhase.Ended || engine.Fuel <= 0)
            {
                StopEffect();
            }
        }
    }

    public void PlayEffect(int index)
    {
            var emission = particles[index].emission;
            emission.enabled = true;
    }

    public void StopEffect()
    {
        foreach (var particle in particles)
        {
            var emission = particle.emission;
            emission.enabled = false;
        }
    }
}
