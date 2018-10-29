using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;
using UnityEngine.SceneManagement; // needed to reset scene/game
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public Text countText;
	public Text winText;
    public Text multiplierText;
    public Text powerText;
    public static System.Random rand = new System.Random();
    public int randomSpeed;
    public AudioClip ballLaunch;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int score;
    private int multiplier;
    private bool firstLaunch;
    private bool occupyingGoal;
    private GameObject launchBarrier;

	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = ballLaunch;
        firstLaunch = true;
        occupyingGoal = false;
		// Set the count to zero 
		score = 0;
        multiplier = 0;
		// Run the SetCountText function to update the UI (see below)
		SetScoreText ();
		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
        multiplierText.text = "Multiplier: ";
        powerText.text = "Launch Power: ";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomSpeed = rand.Next(22, 90);
            if (firstLaunch)
            {
                firstLaunch = false;
                GetComponent<AudioSource>().Play();
                rb.velocity = transform.TransformDirection(Vector3.forward * randomSpeed);
                powerText.text = "Launch Power: " + randomSpeed.ToString();
            }
            if (occupyingGoal)
            {
                // if ball occupies a goal spot, reset the game when Spacebar is pressed
                SceneManager.LoadScene("PoltergeistPumplinkoball");
            }
        }
    }
    /*
	// Each physics step..
	void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed);
	}
    */
	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
        // when the ball passes through the launch barrier trigger, spawn the barrier
        if (other.gameObject.CompareTag("Launch Barrier"))
        {
            launchBarrier = Resources.Load("Launch Barrier") as GameObject;
            Instantiate(launchBarrier);
        }
        // when the ball bounces against the eyes, add to score
        else if (other.gameObject.CompareTag("Eye Bounce"))
        {
            rb.velocity = transform.TransformDirection(Vector3.forward * randomSpeed);
            score = score + 10;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Dot Bounce"))
        {
            rb.velocity = transform.TransformDirection(Vector3.forward * randomSpeed);
            score = score + 20;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Wall Bounce"))
        {
            rb.velocity = transform.TransformDirection(Vector3.forward * randomSpeed);
            score = score + 5;
            SetScoreText();
        }
        // ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
        else if (other.gameObject.CompareTag("Pick Up x2"))
        {
            // Make the other game object (the pick up) inactive, to make it disappear
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            score = score * 2;
            multiplier = 2;
            occupyingGoal = true;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Pick Up x4"))
        {
            other.gameObject.SetActive(false);
            score = score * 4;
            multiplier = 4;
            occupyingGoal = true;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Pick Up x6"))
        {
            other.gameObject.SetActive(false);
            score = score * 6;
            multiplier = 6;
            occupyingGoal = true;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Pick Up x8"))
        {
            other.gameObject.SetActive(false);
            score = score * 8;
            multiplier = 8;
            occupyingGoal = true;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Pick Up x10"))
        {
            other.gameObject.SetActive(false);
            score = score * 10;
            multiplier = 10;
            occupyingGoal = true;
            SetScoreText();
        }
    }

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetScoreText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Score: " + score.ToString ();

        multiplierText.text = "Multiplier: x" + multiplier.ToString();

        // Check if our 'count' is equal to or exceeded 12
        if (occupyingGoal) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
		}
	}
}