using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (PlayerController.isDead || !GameManager.Instance.isGameStarted || GameManager.Instance.isPaused)
        {
            return;
        }

        transform.position += PlayerController.player.transform.forward * (GameManager.Instance.speed * -1 * Time.deltaTime);
    }
}