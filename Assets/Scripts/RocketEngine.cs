using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RocketEngine : MonoBehaviour
{
    [HideInInspector] public bool isFirstJumpTouched;
    float xFirstTouchValue;
    float xSecondTouchValue;
    [HideInInspector] public bool isLanding;
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] float maxSpeed;
    [SerializeField] float castDistance;
    [SerializeField] float radius;
    [SerializeField] LayerMask detectLayer;
    [SerializeField] Vector2 firstJumpDir;
    [SerializeField] Vector2 leftFlyingDir;
    [SerializeField] Vector2 upFlyingDir;
    [SerializeField] Vector2 rightFlyingDir;
    [SerializeField] float maxFuel;
    [SerializeField] float _currentFuel;
    public float Fuel { get => _currentFuel; }
    [SerializeField] float fuelCost;
    [SerializeField] UnityEvent<float> OnChangeFuelValue;
    Camera cam;

    bool isFlyingLeft;
    bool isFlyingRight;
    bool isFlyingUp;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentFuel = maxFuel;
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
        }

        isLanding = CheckLanding();

        if (isLanding && transform.localRotation != Quaternion.Euler(0, 0, 0))
        {
            LeanBack();
        }

        if (Input.touchCount > 0 && _currentFuel > 0)
        {
            Touch first = Input.GetTouch(0);

            if (first.phase == TouchPhase.Began)
            {
                xFirstTouchValue = cam.ScreenToWorldPoint(first.position).x - cam.transform.position.x;

                if (!isFirstJumpTouched && !isLanding)
                {
                    LeanPlayer(xFirstTouchValue);
                }
            }

            if (!isFirstJumpTouched)
            {
                if (isLanding && first.phase == TouchPhase.Began)
                {
                    rb.AddForce(firstJumpDir, ForceMode2D.Impulse);
                    isFirstJumpTouched = true;
                }
                else if (!isLanding)
                {
                    if (Input.touchCount > 1)
                    {
                        Touch second = Input.GetTouch(1);

                        if (second.phase == TouchPhase.Began)
                        {
                            xSecondTouchValue = Camera.main.ScreenToWorldPoint(second.position).x;
                            if (xSecondTouchValue * xFirstTouchValue <= 0)
                            {
                                LeanBack();
                            }
                        }

                        if (second.phase == TouchPhase.Stationary || second.phase == TouchPhase.Moved)
                        {
                            if (xSecondTouchValue * xFirstTouchValue <= 0)
                            {
                                isFlyingUp = true;
                                ChargeFuel(fuelCost * Time.deltaTime);
                            }
                        }
                    }
                    else if (first.phase == TouchPhase.Stationary || first.phase == TouchPhase.Moved)
                    {
                        if (xFirstTouchValue <= 0)
                        {
                            isFlyingLeft = true;
                        }
                        else
                        {
                            isFlyingRight = true;

                        }
                        ChargeFuel(fuelCost * Time.deltaTime);
                        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
                    }
                }
            }
            else if (isFirstJumpTouched && first.phase == TouchPhase.Ended)
            {
                isFirstJumpTouched = false;
            }
        }
        if (Input.touchCount == 0 || _currentFuel <= 0)
        {
            LeanBack();
            AudioManager.Instance.DisableSound("JetpackLaunch");

            if (isFlyingLeft)
                isFlyingLeft = false;
            if (isFlyingRight)
                isFlyingRight = false;
            if (isFlyingUp)
                isFlyingUp = false;
        }
    }

    private void FixedUpdate()
    {
        if (isFlyingLeft || isFlyingRight || isFlyingUp)
        {
            AudioManager.Instance.EnableSound("JetpackLaunch");
            if (isFlyingLeft)
            {
                rb.AddForce(leftFlyingDir);
                LeanPlayer(-1);
            }
            if (isFlyingRight)
            {
                rb.AddForce(rightFlyingDir);
                LeanPlayer(1);
            }
            if (isFlyingUp)
            {
                rb.AddForce(upFlyingDir);
                LeanBack();
            }
        }
    }

    void LeanPlayer(float directionNum)
    {
        if (directionNum > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 3);
        else if (directionNum < 0)
            transform.localRotation =Quaternion.Euler(0, 0, -3);
    }

    void LeanBack()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }


    bool CheckLanding()
    {
        var LandingCheckCollider = Physics2D.CircleCast(transform.position, radius, -transform.up, castDistance, detectLayer);
        if (LandingCheckCollider)
        {
            if (LandingCheckCollider.collider.CompareTag("SafeZone"))
            {
                ChargeFuel(-maxFuel + _currentFuel);
                //string eventName = $"{LevelManager.Instance.currentLevel}_{LandingCheckCollider.transform.name.Trim()}";
                //FirebaseManager.LogEvent(eventName);
            }
            return true;
        }
        return LandingCheckCollider;
    }

    public RaycastHit2D GetLandDetectionZone()
    {
        return Physics2D.CircleCast(transform.position, radius, -transform.up, castDistance, detectLayer);
    }

    public RaycastHit2D GetCeilingDetectionZone()
    {
        return Physics2D.CircleCast(transform.position, radius + .09f, transform.up, castDistance + .15f, detectLayer);
    }

    void ChargeFuel(float chargedValue)
    {
        _currentFuel -= chargedValue;
        OnChangeFuelValue.Invoke(chargedValue);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - transform.up * castDistance, radius);
        Gizmos.DrawWireSphere(transform.position + transform.up * (castDistance + .15f), radius + .09f);
    }
}
