using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public float speed;
    public float dodge;

    public Boundary boundary;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float target;
    private Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Evade());
    }

    void FixedUpdate() {
        float newManeuver = Mathf.MoveTowards(rb2d.velocity.x, target, speed);
        rb2d.velocity = new Vector2(newManeuver, rb2d.velocity.y);
        
        rb2d.position = new Vector2(
            Mathf.Clamp(rb2d.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb2d.position.y, boundary.yMin, boundary.yMax)
        );
    }

    IEnumerator Evade() {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        
        while(true) {
            target = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            target = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
}
