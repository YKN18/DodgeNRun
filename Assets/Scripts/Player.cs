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

    //Based on the player's current position, the parameters used by the move
    //sideways coroutine are set and a flag ready is set to false to indicate 
    //that the player is already moving sideways.

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

        //Every 10 seconds the speed is increased by a speedup factor
        if (gameTimer > 10){
            gameTimer = 0;
            speed += speedUp;
            PowerupManager.instance.CurrentSpeedIncreased(speedUp);
        } 

        gameTimer += Time.deltaTime;

        transform.Translate(0, 0, speed * Time.deltaTime);

        //Touch and keyboard controls

        #region TouchControls
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchCoordinates = Input.GetTouch(0).position;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).position - startTouchCoordinates;
            if (touchDeltaPosition.y > touchDeltaPosition.x && touchDeltaPosition.y > 20f)
            {
                if(isGrounded)
                    Jump();
            }
            else if (touchDeltaPosition.x > 0 && ready)
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
            Jump();
        }
        #endregion
    }


    //Coroutine to move the player from one 'lane' to the other
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

    //Coroutine to move the player back to the starting position in case of collision
    //with a non-damagin cube
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

    //If the player hits an obstacle while moving sideways, it's brought back
    //to the original position smoothly
    void OnCollisionEnter(Collision c) {
        if (c.collider.tag == "GreenObstacle" && !ready) {
            StopCoroutine("MoveSideways");
            StartCoroutine("RecoverFromHit");
        }
    }

    //While the player touches the ground or the green cube, isgrounded is true and
    //the jump is possible
    void OnCollisionStay(Collision c)
    {
        if (c.collider.gameObject.layer == 8 || c.collider.tag == "GreenObstacle")
            isGrounded = true;
    }

    //As soon as the player leaves a walkable surface, isgrounded is set to false
    void OnCollisionExit(Collision c) {
        if(c.collider.gameObject.layer == 8 || c.collider.tag == "GreenObstacle")
            isGrounded = false;
    }

    //If the player enters a coin or a damaging obstacle, the suitable action is taken
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
