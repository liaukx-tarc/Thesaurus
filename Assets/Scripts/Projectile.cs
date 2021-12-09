using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public RaycastHit hit;
    public LayerMask layerMask = 1 << 10;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward *30* Time.fixedDeltaTime);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 0.4f, ~(layerMask))
                || Physics.Raycast(transform.position - new Vector3(0.4f, 0, 0), transform.forward, out hit, 0.2f, ~(layerMask))
                || Physics.Raycast(transform.position + new Vector3(0.4f, 0, 0), transform.forward, out hit, 0.2f, ~(layerMask))
                || Physics.Raycast(transform.position - new Vector3(0, 0.4f, 0), transform.forward, out hit, 0.2f, ~(layerMask))
                || Physics.Raycast(transform.position + new Vector3(0, 0.4f, 0), transform.forward, out hit, 0.2f, ~(layerMask)))
            {
            Destroy(this.gameObject);
        }

    }
}
