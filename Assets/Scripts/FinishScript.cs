using UnityEngine;

public class FinishScript : MonoBehaviour
{
    public GameObject startLine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, startLine.transform.position.y, startLine.transform.position.z);
    }
}