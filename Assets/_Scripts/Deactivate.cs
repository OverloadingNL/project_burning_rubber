using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public bool destroyObject = false;
    private bool deactivationScheduled = false;

    private void OnTriggerExit(Collider other)
    {

        if (PlayerController.isDead)
        {
            return;
        }

        if (other.gameObject.tag == "Terminator")
        {
            Invoke("SetInactive", 5.0f);
            deactivationScheduled = true;
        }
    }

    void SetInactive()
    {
        if (destroyObject)
        {
            DestroyImmediate(gameObject);
        }
        gameObject.SetActive(false);
        deactivationScheduled = false;
    }
}
