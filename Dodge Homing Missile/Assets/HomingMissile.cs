using UnityEngine;

/// <summary>
/// An object that will move towards an object marked with the tag 'targetTag'. 
/// </summary>
public class HomingMissile : MonoBehaviour
{
    /// <summary>
    /// The base movement speed of the missile, in units per frame. 
    /// </summary>
    [SerializeField]
    private float speed = 15;

    /// <summary>
    /// The base rotation speed of the missile, in radians per frame. 
    /// </summary>
    [SerializeField]
    [Range(0, 2)]
    private float rotationSpeed;

    /// <summary>
    /// The distance at which this object stops following its target and continues on its last known trajectory. 
    /// </summary>
    [SerializeField]
    private float focusDistance = 5;
    
    /// <summary>
    /// The transform of the target object.
    /// </summary>
    private Transform target;

    /// <summary>
    /// Returns true if the object should be following the target this frame. 
    /// </summary>
    private bool isFollowingTarget = true;

    /// <summary>
    /// The tag of the target object.
    /// </summary>
    [SerializeField]
    private string targetTag;

    /// <summary>
    /// Error message.
    /// </summary>
    private string enterTagPls = "Please enter the tag of the object you'd like to target, in the field 'Target Tag' in the Inspector.";

    /// <summary>
    /// Enable this if you want this object to face its target while moving toward it. 
    /// </summary>
    [SerializeField]    
    private bool faceTarget;

    private Vector3 tempVector;

    private void Start()
    {
        if(targetTag == "")
        {
            Debug.LogError(enterTagPls);
            return;
        }

        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    private void Update()
    {
        if (targetTag == "")
        {
            Debug.LogError(enterTagPls);
            return;
        }

        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isFollowingTarget = false;
        }

        Vector3 targetDirection = target.position - transform.position;

        if (faceTarget)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

            MoveForward(Time.deltaTime);

            if (isFollowingTarget)
            {
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        else
        {            
            if (isFollowingTarget)
            {
                tempVector = targetDirection.normalized;

                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(tempVector * Time.deltaTime * speed, Space.World);
            }
        }
    }

    /// <summary>
    /// Moves forward at 'speed' multiplied by 'rate', per frame. 
    /// Use Time.deltaTime as a parameter to travel forward at the same speed per second.
    /// </summary>
    /// <param name="rate"></param>
    private void MoveForward (float rate)
    {
        transform.Translate(Vector3.forward * rate * speed, Space.Self);
    }
}