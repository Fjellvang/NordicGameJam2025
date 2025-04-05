using UnityEngine;

public class CatMainMenuRotate : MonoBehaviour
{
    public float rotationSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0)); //+= rotationSpeed * Time.deltaTime;
    }
}
