using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float jumpPower = 10.0f;
    Rigidbody2D myRigidbody;
    public bool isGrounded = false;
    float posX = 0.0f;
    bool isGameOver = false;
    ChallengerController myChallengeController;
    GameController myGameController;

	// Use this for initialization
	void Start () {
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        posX = -9.5f;
        myChallengeController = FindObjectOfType<ChallengerController>();
        myGameController = FindObjectOfType<GameController>();
		
	}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)&& isGrounded && !isGameOver)
        {
            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
        }
        // hit in face check
        if(transform.position.x < posX)
        {
            GameOver();
        }
    }

    private void Update()
    {
        
    }

    void GameOver()
    {
        Debug.Log("End Game");
        isGameOver = true;
        myChallengeController.GameOver();
    }

    void AddScore()
    {
        myGameController.IncrementScore();
    } 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Ground")
        {
            isGrounded = true;
        }
        if(other.collider.tag=="Enemy")
        {
            GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gold")
        {
            myGameController.IncrementScore();
            Destroy(other.gameObject);
        }
    }

}
