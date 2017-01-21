﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour {

    public float playerSpeed;
    public float minTimeSpeed = 0.5f;
    public float maxTimeSpeed = 2f;
    private int nearBonus;
    private Vector3 newPosition;
    private bool allowNewPosition;
    private Rigidbody2D rigidBody;
    private float lastDistance = 0.0f;
    private float totalDistance = 0.0f;
    private float SIZEOFBOX_X;
    private float SIZEOFBOX_Y;
    private int numberOfClicks = 0; 
    private bool gameRunning = true;
    private AudioSource audioSource;
    [SerializeField] AudioClip speedUpMovement;
    [SerializeField] AudioClip slowMovement;
    [SerializeField] AudioClip deathSound;
    [SerializeField] UIManager uiManager;
    [SerializeField] SpawningEnemies spawningEnemies;

    // Use this for initialization
    void Start () {
        lastDistance = 0.0f;
        totalDistance = 0.0f;
        nearBonus = 0;
        numberOfClicks = 0;
        newPosition = transform.position;
        Time.timeScale = minTimeSpeed;
        allowNewPosition = true;
        gameRunning = true;
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource =  transform.GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
        audioSource.loop = false;

        Camera camera = FindObjectOfType<Camera>();
        Vector3 screenPoint1 = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        SIZEOFBOX_X = screenPoint1.x + 0.5f;
        SIZEOFBOX_Y = screenPoint1.y + 0.5f;
       
    }

    // Update is called once per frame
    void Update () {
        if (gameRunning) {
            checkClick();
            moveToPosition();
        }
        
    }

    private void moveToPosition()
    {
        //se premaknemo če še nismo dosegli cilj
        float distance = Vector3.Distance(transform.position, newPosition);
        totalDistance += distance;
        lastDistance = distance;
        if (distance > 0.5f && !allowNewPosition)
        {
            Time.timeScale = maxTimeSpeed;
            
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

        if (transform.position.x > SIZEOFBOX_X || transform.position.x < -SIZEOFBOX_X ||
            transform.position.y > SIZEOFBOX_Y || transform.position.y < -SIZEOFBOX_Y)
            reset();
    }

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
                //Debug.Log("rayCastHit");
                // Souund
                audioSource.clip = speedUpMovement;
                audioSource.loop = false;
                audioSource.volume = 0.1f;
                audioSource.Play();

                newPosition = hit.point;
                allowNewPosition = false;
                numberOfClicks++;
                //transform.position = newPosition;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            audioSource.volume = 0.1f;
            audioSource.loop = false;
            audioSource.clip = deathSound;
            audioSource.Play();

            //Debug.Log("Collision detetcted, rigidbody set to kinematic. END GAME");
            //rigidBody.isKinematic = true;
            reset();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("Near bonus");
            nearBonus += 50;
        }
    }

    private void reset()
    {
        audioSource.loop = false;
        uiManager.endGame(nearBonus + spawningEnemies.calculateScore() + (int)((totalDistance - lastDistance) / numberOfClicks));
        gameRunning = false;
        nearBonus = 0;
        GameObject[] lasers;

        lasers = GameObject.FindGameObjectsWithTag("Laser");

        foreach (GameObject laser in lasers)
        {
            Destroy(laser);
        }
    }

    void calculateAngle()
    {
        Vector3 newDir = newPosition - transform.position;
        float angle = Vector3.Angle(newDir, transform.up);  //calculate angle

        if (angle > 1)
        {
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

        }
        //Debug.Log(angle);
    }

}
