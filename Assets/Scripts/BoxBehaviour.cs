using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    internal Transform posCam;

    internal Vector2 direction = new Vector2();
    Vector3 realDir = new Vector3();
    float force;

    Rigidbody rgbd;
    internal LevelManager lvlmanager;
    float timer;

    internal AudioManager audiomanager;
    [SerializeField] AudioSource sndHit;

    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody>();
        timer = 10.0f;

        var frc = direction.magnitude/2;
        force = (frc >= 300) ? 300 : ((frc <= 40) ? 40 : frc);

        var nrmlDir = new Vector3(direction.normalized.y, 0, -direction.normalized.x);
        var angle = Vector3.Angle(nrmlDir, Vector3.right);
        var cross = Vector3.Cross(nrmlDir,Vector3.right);
        if (cross.y < 0) { angle *= -1; }

        var camAnlg = Mathf.Atan(posCam.localPosition.z / posCam.localPosition.x);
        var dtAngle = angle*Mathf.Deg2Rad + camAnlg;

        realDir = new Vector3(Mathf.Cos(dtAngle), (force/1000), Mathf.Sin(dtAngle));

        rgbd.AddForce(realDir*(force/2), ForceMode.Impulse);
        rgbd.AddForce(TruckSpeed.speed*Vector3.right, ForceMode.Impulse);

        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), transform.rotation.w);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "house")
        {
            audiomanager.sndsDelivered[0].Play();
            audiomanager.sndsDelivered[1].Play();
            gameObject.SetActive(false);
            other.tag = "Untagged";
            other.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            other.GetComponent<HouseBehaviour>().delivered = true;
            other.transform.Find("mailbox").GetComponent<Renderer>().material.color = Color.white;
            other.isTrigger = false;
            other.GetComponent<Outline>().enabled = false;
            lvlmanager.housesToDeliver.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var fColl = (collision.impulse.magnitude)/100;
        sndHit.volume = 0.1f+fColl;
        sndHit.Play();
    }
}
