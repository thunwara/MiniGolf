using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GolfBall : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    public float maxPower;
    public float changeAngleSpeed;
    public float lineLength;
    public Slider powerSlider;
    public TextMeshProUGUI puttCountLabel;
    public float minHoleTime;
    public Transform startTransform;
    public LevelManeger levelManeger;


    private LineRenderer line;
    private Rigidbody ball;
    private float angle;
    private float powerUpTime;
    private float power;
    private int puttCounter;
    private float holeTime;
    private Vector3 lastPosition;


    private void Awake()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        line = GetComponent<LineRenderer>();
        startTransform.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        if (ball.velocity.magnitude < 0.01f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                changeAngle(-1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                changeAngle(1);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                putt();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                powerUp();
            }
            updateLinePosition();
        }
        else
        {
            line.enabled = false;
        }
    }

    // created Methods
    private void changeAngle(int direction)
    {
        angle += changeAngleSpeed * Time.deltaTime * direction;
    }

    private void updateLinePosition()
    {
        if (holeTime == 0)
        {
            line.enabled = true;
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * lineLength);
    }

    private void putt()
    {
        lastPosition = transform.position;
        ball.AddForce(Quaternion.Euler(0, angle, 0) * Vector3.forward * maxPower * power, ForceMode.Impulse);
        power = 0;
        powerSlider.value = 0;
        powerUpTime = 0;
        puttCounter++;
        puttCountLabel.text = puttCounter.ToString();
    }

    private void powerUp()
    {
        powerUpTime += Time.deltaTime;
        power = Mathf.PingPong(powerUpTime, 1);
        powerSlider.value = power;
        // Debug.Log("power = " + power);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "hole")
        {
            countHoleTime();
            // Debug.Log("hole trigger");
        }
    }

    private void countHoleTime()
    {
        holeTime += Time.deltaTime;
        // Debug.Log("hole time = " + holeTime + "min hole time =" + minHoleTime);
        if (holeTime > minHoleTime)
        {
            levelManeger.nextPlayer(puttCounter);
            // player has finished, move on to the next player
            Debug.Log("I'm in the hole and it took me " + puttCounter + "putt(s) to get it in");
            holeTime = 0;
            // line.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "hole")
        {
            leftHole();
        }
    }

    private void leftHole()
    {
        holeTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "out of bounds")
        {
            transform.position = lastPosition;
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
    }

    public void SetUpBall(Color color)
    {
        transform.position = startTransform.position;
        angle = startTransform.rotation.eulerAngles.y;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        line.material.SetColor("_Color", color);
        line.enabled = true;
        puttCounter = 0;
        puttCountLabel.text = "0";
    }
}
