using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject missile;

    [SerializeField]
    private Transform instantiationPoint;

    private void Update()
    {
        if (missile == null)
        {
            missile = Instantiate(prefab, instantiationPoint.position, Quaternion.Euler(-90, 0, 0));
        }
    }
}
