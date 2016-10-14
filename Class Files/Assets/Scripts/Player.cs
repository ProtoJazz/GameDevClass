using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : MonoBehaviour {
    public Rigidbody ourRb;
    public float moveSpeed;
    public int score = 0;

    public float timeLimit;

    public Text scoreText;
    public Text timerText;
    public Text gameOverText;
    public Text winText;
    public bool isGameRunning = true;
    public int scoreToWin;
	// Use this for initialization
	void Start () {
        StartCoroutine(timer());
	}
	
	// Update is called once per frame
	void Update () {
        if (isGameRunning)
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(moveH, 0, moveV);
            ourRb.AddForce(moveDirection * moveSpeed);
        }
	}

    public IEnumerator timer()
    {
        float currentTime = Time.timeSinceLevelLoad;
        float endTime = currentTime + timeLimit;

        while (currentTime < endTime && isGameRunning)
        {
            timerText.text = Mathf.RoundToInt(endTime - currentTime).ToString();
            yield return new WaitForEndOfFrame();
            currentTime = Time.timeSinceLevelLoad; 
        }
        if (isGameRunning)
        {
            isGameRunning = false;
            gameOverText.gameObject.SetActive(true);
        }
        yield return null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGameRunning)
        {
            if (collision.transform.tag == "Pickup")
            {
                Pickup pickupscript = collision.gameObject.GetComponent<Pickup>();
                pickupscript.Death();
                this.score++;
                scoreText.text = "Score " + this.score;
                if (score >= scoreToWin)
                {
                    isGameRunning = false;
                    winText.gameObject.SetActive(true);
                }
            }
        }
    }
}
