using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour {

    public float playerSpeed;
    public float minTimeSpeed = 0.5f;
    public float maxTimeSpeed = 2f;
    public int nearBonus;
    private Vector3 newPosition;
    private float distance;
    private float oldDistance;
    private float angle;
    private float oldAngle;
    private float cicleDistanceModifier;
    private bool allowNewPosition;
    //private Rigidbody2D rigidBody;
    public float lastDistance;
    public float totalDistance;
    private float SIZEOFBOX_X;
    private float SIZEOFBOX_Y;
    public int numberOfClicks = 0; 
    public bool gameRunning = true;
    public bool TutorialMode = false;
    public AudioSource audioSource;
    [SerializeField] AudioClip speedUpMovement;
    [SerializeField] AudioClip slowMovement;
    [SerializeField] AudioClip deathSound;
    [SerializeField] UIManager uiManager;
    [SerializeField] SpawningEnemies spawningEnemies;

    // Use this for initialization
    void Start () {
        //rigidBody = GetComponent<Rigidbody2D>();
        audioSource =  transform.GetComponent<AudioSource>();        
        Camera camera = FindObjectOfType<Camera>();
        Vector3 screenPoint1 = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        SIZEOFBOX_X = screenPoint1.x + 0.5f;
        SIZEOFBOX_Y = screenPoint1.y + 0.5f;
        resetPlayer();
    }

    public void resetPlayer()
    {
        lastDistance = 0.0f;
        totalDistance = 0.0f;
        nearBonus = 0;
        numberOfClicks = 0;
        cicleDistanceModifier = 100f;
        newPosition = transform.position;
        oldDistance = int.MaxValue;
        Time.timeScale = minTimeSpeed;
        allowNewPosition = true;
        gameRunning = true;
        audioSource.volume = 0.1f;
        audioSource.loop = false;
    }

    // Update is called once per frame
    void Update () {
        if (gameRunning) {
            checkClick();
            moveToPosition();
            fixCicle();
        }
        
    }

    private void fixCicle()
    {
        
        if (!allowNewPosition)
        {
            //Debug.Log("0 Distance: " + distance + ", oldDistance: " + oldDistance + ", angle: " + angle + ", oldAngle: " + oldAngle);
            if (oldDistance + cicleDistanceModifier < distance && angle < 30) //&& oldAngle < angle && angle < 90)
            {
                Debug.Log("PLAYER ZACIKALN! Distance: " + distance + ", oldDistance: " + oldDistance + ", modifier: " + cicleDistanceModifier + ", angle: " + angle + ", oldAngle: " + oldAngle);
                //Debug.Log("PLAYER ZACIKALN! Distance: " + distance + ", oldDistance: " + oldDistance + ", modifier: " + cicleDistanceModifier);
                //Debug.Log("Player zaciklan!!!");
                newPosition = transform.position;
                allowNewPosition = true;
                oldDistance = int.MaxValue;
                oldAngle = int.MaxValue;
                distance = int.MaxValue;
                angle = int.MaxValue;
                cicleDistanceModifier = 100f;
                return;
            }

            if (cicleDistanceModifier > 0)
                cicleDistanceModifier -= 2f;
            else
                cicleDistanceModifier = 0;


            oldDistance = distance;
            oldAngle = angle;
        }
    }

    private void moveToPosition()
    {
        //se premaknemo če še nismo dosegli cilj
        //Vector3 modifiedPos = transform.position - (transform.up * 0.5f);
        //float distance = Vector3.Distance(modifiedPos, newPosition);
        distance = Vector3.Distance(transform.position, newPosition);

        //Debug.Log("Pos: " + transform.position + "     New pos: " + newPosition + "    Modified pos: "+modifiedPos);

        //if (distance > 0.5f && !allowNewPosition)
        if (distance > 0.2f && !allowNewPosition)//newPosition
        {
            Time.timeScale = maxTimeSpeed;
            totalDistance += distance;
            lastDistance = distance;

            //transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            //Vector3 newDir = Vector3.RotateTowards(transform.position, newPosition, step, 0.0F);

            calculateAngle();

            //transform.Rotate(newDir);
            //transform.rotation = Quaternion.LookRotation(newDir);
        }
        else {  //drugače upočasnimo čas in dovolimo novi cilj
            Time.timeScale = minTimeSpeed;
            allowNewPosition = true;
            
            audioSource.clip = slowMovement;
            audioSource.loop = true;
            audioSource.volume = 0.1f;
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            

            //Debug.Log("No move");
        }

        float step = playerSpeed * Time.deltaTime;
        transform.position += transform.up * step;

        if ((transform.position.x > SIZEOFBOX_X || transform.position.x < -SIZEOFBOX_X ||
            transform.position.y > SIZEOFBOX_Y || transform.position.y < -SIZEOFBOX_Y) &&
            !TutorialMode) {
            reset();
        } else if ((transform.position.x > SIZEOFBOX_X || transform.position.x < -SIZEOFBOX_X ||
            transform.position.y > SIZEOFBOX_Y || transform.position.y < -SIZEOFBOX_Y) &&
            TutorialMode) {
            // If player goes out of bounds
            GameObject trail1 = transform.FindChild("Trail1").gameObject;
            trail1.SetActive(false);
            transform.position = new Vector3(0, 0, 0);
            trail1.SetActive(true);

            Tutorial tutorial = (Tutorial)FindObjectOfType(typeof(Tutorial));

            DestroyLasers();
            newPosition = new Vector3(0, 0, 0);
            //tutorial.stageOfTutorial--;
            tutorial.showInstrucitons();
        }
            
    }

    //private void checkClick()
    //{
    //    //preverimo klik za novo pozicijo
    //    if (Input.GetMouseButtonDown(0) && allowNewPosition)
    //    {
    //        //Debug.Log("klik");
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            Vector3 newDir = hit.point - transform.position + transform.up;
    //            float angle = Vector3.Angle(newDir, transform.up);
    //            //Debug.Log("Click angle: " + angle);

    //            if (angle > 30 && angle < 150)
    //            {
    //                if (Vector3.Distance(transform.position, hit.point) > 1)
    //                    newPositionConfirmed(hit.point);
    //            }
    //            else {
    //                if (Vector3.Distance(transform.position, hit.point) > 0.2)
    //                    newPositionConfirmed(hit.point);
    //            }
    //        }
    //    }
    //}

    private void checkClick()
    {
        //preverimo klik za novo pozicijo
        if (Input.GetMouseButtonDown(0) && allowNewPosition)
        {
            //Debug.Log("klik");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) > 0.2)
                {
                    newPositionConfirmed(hit.point);
                }
            }
        }
    }

    private void newPositionConfirmed(Vector3 clickedPos)
    {
        // Souund
        audioSource.clip = speedUpMovement;
        audioSource.loop = false;
        audioSource.volume = 0.1f;
        audioSource.Play();

        newPosition = clickedPos;
        allowNewPosition = false;
        numberOfClicks++;
        oldDistance = int.MaxValue;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Laser") && !TutorialMode) {
            audioSource.volume = 0.1f;
            audioSource.loop = false;
            audioSource.clip = deathSound;
            audioSource.Play();

            //Debug.Log("Collision detetcted, rigidbody set to kinematic. END GAME");
            //rigidBody.isKinematic = true;
            reset();
        } else if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Laser") && TutorialMode) {  // If player fails show him instruction again dont reset game

            GameObject trail1 = transform.FindChild("Trail1").gameObject;
            trail1.SetActive(false);
            transform.position = new Vector3(0, 0, 0);
            trail1.SetActive(true);

            Tutorial tutorial = (Tutorial)FindObjectOfType(typeof(Tutorial));

            DestroyLasers();
            newPosition = new Vector3(0, 0, 0);
            //tutorial.stageOfTutorial--;
            tutorial.showInstrucitons();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            //Debug.Log("Near bonus");
            nearBonus += 50;
            StartCoroutine(uiManager.showNearMiss());

        }
    }

    private void reset()
    {
        audioSource.loop = false;
        uiManager.endGame(spawningEnemies.calculateScore());
        //Debug.Log("Time: " + ((int)spawningEnemies.timePassed * 3).ToString());
        //Debug.Log("Waves: " + spawningEnemies.calculateScore());
        //Debug.Log("Near: " + nearBonus);
        //Debug.Log("Distance: " + (int)(totalDistance - lastDistance));
        //Debug.Log("Number of Clicks: " + numberOfClicks);

        gameRunning = false;
        nearBonus = 0;
        DestroyLasers();
    }

    void calculateAngle()
    {
        //Vector3 newDir = newPosition - transform.position;
        Vector3 newDir = newPosition - transform.position + transform.up;   //da ni trzanja ki pokvari trail je + transform.up 
        angle = Vector3.Angle(newDir, transform.up);  //calculate angle

        //if (angle > 4)//&& Vector3.Distance(newPosition, transform.position) > 0.5f)
        //{
            if (Vector3.Cross(newDir, transform.up).z < 0)
            {
                //angle = -angle;
                if (angle > 8)
                    transform.Rotate(Vector3.forward, 8f);
                else
                    transform.Rotate(Vector3.forward, angle);
            }
            else
            {
                if (angle > 8)
                    transform.Rotate(Vector3.forward, -8f);
                else
                    transform.Rotate(Vector3.forward, -angle);
            }

        //}
        //Debug.Log(angle);
    }

    private void DestroyLasers () {
        GameObject[] lasers;

        lasers = GameObject.FindGameObjectsWithTag("Laser");

        foreach (GameObject laser in lasers) {
            Destroy(laser);
        }
    }

}
