using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool canMove;
    private Rigidbody2D theRB2D;

    public bool grounded;
    public LayerMask whatIsGrd;
    public Transform grdChecker;
    public float grdCheckerRad;

    public float airtime;
    public float airtimeCounter;

    private Animator theAnimator;

    public GameManager theGM;
    private LivesManager theLM;

    //quiz thing
    public bool sprung;
    public LayerMask whatIsSpr;

    public bool teleport;
    public LayerMask whatIsTel;

    // Start is called before the first frame update
    void Start()
    {
        theLM = FindObjectOfType<LivesManager>();

        theRB2D = GetComponent<Rigidbody2D>();
        airtimeCounter = airtime;
        theAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            airtimeCounter = 0;
        }
        //quiz thing
        if (sprung == true)
        {
            theRB2D.velocity = new Vector2(theRB2D.velocity.x, 50);

        }
        if (teleport == true)
        {
            Vector2 newpos = new Vector2(-7.5f, 3.5f);
            theRB2D.MovePosition(newpos);

        }
        

    }
    private void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsGrd);
        MovePlayer();
        Jump();
        //quiz thing
        sprung = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsSpr);
        teleport = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsTel);
    }
    void MovePlayer()
    {
        if (canMove)
        {
            theRB2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed,
           theRB2D.velocity.y);

            theAnimator.SetFloat("Speed", Mathf.Abs(theRB2D.velocity.x));

            if (theRB2D.velocity.x > 0)
                transform.localScale = new Vector2(1f, 1f);
            else if (theRB2D.velocity.x < 0)
                transform.localScale = new Vector2(-1f, 1f);
        }
    }

    void Jump()
    {
        if (grounded == true)
        {

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
            }

            
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (airtimeCounter > 0)
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                airtimeCounter -= Time.deltaTime;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            airtimeCounter = 0;
        }

        

            if (grounded)
        {
            airtimeCounter = airtime;
        }

        theAnimator.SetBool("Grounded", grounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.tag == "Spike")
        {
            Debug.Log("Ouch!");
            //theGM.GameOver();
            theLM.TakeLife();
        }

    }

}