using UnityEngine;

public class CamaraFlow : MonoBehaviour
{
    public GameObject Target;  
    void Update()
    {
        Vector3 targetPosition = Target.transform.position;
        transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
    }
}
