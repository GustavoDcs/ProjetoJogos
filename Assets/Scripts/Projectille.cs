using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectille : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Rigidbody2D projectileRigidbody;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        projectileRigidbody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

     void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
