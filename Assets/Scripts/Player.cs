using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float speed = 2f;
    private float t;
    private float gameTimer;
    private float leftX = -1.5f;
    private float centerX = 0f;
    private float rightX = 1.5f;
    private string currentPos = "center";
    private string pastPos;
    private float current, target;
    private bool ready = true;
    private bool isGrounded = true;
    private Rigidbody rb;
    public float lastJumpTime = -1f;
    [SerializeField] float repositioningSpeed = 4f;
    [SerializeField] float keyTimer = 0.03f;
    [SerializeField] float hitRecoveryTime = 0.07f;
    [SerializeField] float jumpForce = 6f;
    private int damage;
    [SerializeField] float speedUp = 2;
    private Vector2 startTouchCoordinates;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void goLeft() {
        if (currentPos == "center")
        {
            current = centerX;
            target = leftX;
            ready = false;
            pastPos = currentPos;
            currentPos = "left";
            StartCoroutine("MoveSideways");
        }
        else if (currentPos == "right")
        {
            current = rightX;
            target = centerX;
            ready = false;
            pastPos = currentPos;
            currentPos = "center";
            StartCoroutine("MoveSideways");
        }
    }

    void goRight()
    {
        if (currentPos == "center")
        {
            current = centerX;
            target = rightX;
            ready = false;
            pastPos = currentPos;
            currentPos = "right";
            StartCoroutine("MoveSideways");
        }
        else if (currentPos == "left")
        {
            current = leftX;
            target = centerX;
            ready = false;
            pastPos = currentPos;
            currentPos = "center";
            StartCoroutine("MoveSideways");
        }
    }

    void Update() {
        if (gameTimer > 10){
            gameTimer = 0;
            speed += speedUp;
            PowerupManager.instance.CurrentSpeedIncreased(speedUp);
        } 

        gameTimer += Time.deltaTime;

        transform.Translate(0, 0, speed * Time.deltaTime);

        #region TouchControls
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchCoordinates = Input.GetTouch(0).position;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).position - startTouchCoordinates;
            if (touchDeltaPosition.y > touchDeltaPosition.x && touchDeltaPosition.y > 20f && isGrounded)
            {
                isGrounded = false;
                Jump();
            }
            else if (touchDeltaPosition.x > 20f && ready)
            {
                goRight();
            }
            else if (touchDeltaPosition.x < -20f && ready)
            {
                goLeft();
            }
            
        }
        #endregion

        #region KeyboardControls
        if (Input.GetAxis("Horizontal") < 0 && ready) {
            goLeft(); 
        }

        if (Input.GetAxis("Horizontal") > 0 && ready)
        {
            goRight();
        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded)
        {
            isGrounded = false;
            Jump();
        }
        #endregion
    }

    IEnumerator MoveSideways() {
        t = 0;
        while (t <= 1) {
            t += Time.deltaTime * repositioningSpeed;
            transform.position = new Vector3(Mathf.Lerp(current, target, t), transform.position.y, transform.position.z);
            yield return 0;
        }
        t = 0;
        while (t < keyTimer)
        {
            t += Time.deltaTime;
            yield return 0;
        }
        ready = true;
    }

    IEnumerator RecoverFromHit()
    {
        currentPos = pastPos;
        transform.position = new Vector3(current, transform.position.y, transform.position.z);
        t = 0;
        while (t < hitRecoveryTime)
        {
            t += Time.deltaTime;
            yield return 0;
        }
        ready = true;
    }

    private void Jump() {
        if (GameManager.instance.GetTime() - lastJumpTime > 0.2)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            lastJumpTime = GameManager.instance.GetTime();
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.layer == 8)
            isGrounded = true;
        else if (c.collider.tag == "GreenObstacle")
        {
            if (!ready)
            {
                StopCoroutine("MoveSideways");
                StartCoroutine("RecoverFromHit");
            }
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 9) {
            if (c.tag == "RedObstacle")
            {
                HealthManager.instance.Hit(c.GetComponent<Damaging>().GetDamage());
                if (c.transform.parent.gameObject.layer != 8) c.transform.parent.gameObject.SetActive(false);
                else c.gameObject.SetActive(false);
            }
        }
        else if (c.gameObject.layer == 11)
        {
            SoundManager.instance.PlayPowerup();
            c.transform.parent.gameObject.SetActive(false);
            switch (c.tag)
            {
                case "HealthPowerup":
                    PowerupManager.instance.TriggerHealthPowerup(c.gameObject);
                    break;
                case "SlowPowerup":
                    PowerupManager.instance.TriggerSlowPowerup(c.gameObject);
                    break;
                case "InvinciblePowerup":
                    PowerupManager.instance.TriggerInvinciblePowerup(c.gameObject);
                    break;
                default:
                    break;
            }
        }
        else if (c.gameObject.layer == 10) {
            SoundManager.instance.PlayCoin();
            CoinManager.instance.AddCoin(c.GetComponent<Coin>().GetBonus());
            c.gameObject.SetActive(false);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
