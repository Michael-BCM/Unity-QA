using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10;

	private void Update ()
    {
        Vector3 movementVector = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0), 1);
        
        transform.Translate(movementVector * Time.deltaTime * movementSpeed);
	}

}
