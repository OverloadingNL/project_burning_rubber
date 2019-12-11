using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject player;
    public GameObject currentPlatform;
    public Platform platformSettings;
    public static bool isDead = false;

    private Rigidbody rb;

    void OnCollisionEnter(Collision other)
    {
        if (isDead) { return; }

        if (other.gameObject.tag == "Obstacle")
        {
            isDead = true;
            rb.AddExplosionForce(GameManager.Instance.speed, other.transform.position, 1.0f, 1.0f, ForceMode.Impulse);
        }
        else
        {
            currentPlatform = other.gameObject;
            platformSettings = currentPlatform.GetComponentInParent<Platform>();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < GameManager.Instance.flatStartSegments; i++)
        {
            GenerateWorld.RunDummy(0, true);
        }
    }

    private void Update()
    {
        if (!isDead && platformSettings != null)
        {
            Move();
        }
    }

    int laneIndex = 2;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 3))
        {
            transform.up = hit.normal;
        }
    }
    private void Move()
    {

        if (MobileInput.Instance.SwipeLeft)
        {

            laneIndex--;
        }
        else if (MobileInput.Instance.SwipeRight)
        {
            laneIndex++;
        }

        laneIndex = Mathf.Clamp(laneIndex, 0, platformSettings.lanes.Length - 1);

        Vector3 targetPosition = new Vector3(platformSettings.lanes[laneIndex], transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, 2 * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider)
        {
            GenerateWorld.RunDummy(0, GameManager.Instance.tutorial);
        }
    }
}