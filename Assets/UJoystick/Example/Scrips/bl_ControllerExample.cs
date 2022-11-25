using UnityEngine;

public class bl_ControllerExample : MonoBehaviour {

    /// <summary>
    /// Step #1
    /// We need a simple reference of joystick in the script
    /// that we need add it.
    /// </summary>
	private bl_Joystick Joystick;//Joystick reference for assign in inspector
    private bl_Joystick JoystickR;
    private Transform fireposition;
    private Rigidbody rigidbody;
    public float angularSpeed;
    [SerializeField]private float Speed = 5;

    [SerializeField] public bool isTorret = false;

    public GameObject shellPrefab;
    public float shellSpeed = 10;

    public float Start_Delay;
    private float delay;

    void Start()
    {
        fireposition = transform.Find("FireTransform");
        rigidbody = GetComponent<Rigidbody>();
        Joystick = GameObject.FindGameObjectWithTag("joyleft").GetComponent<bl_Joystick>();
        JoystickR = GameObject.FindGameObjectWithTag("joyright").GetComponent<bl_Joystick>();
    }

    void Update()
    {
        
        if (isTorret == true)
        {
            float v = JoystickR.Vertical;
            rigidbody.velocity = transform.forward * v * Speed;

            float h = JoystickR.Horizontal;
            rigidbody.angularVelocity = transform.up * h * angularSpeed;

            Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
            transform.Translate(translate);

            if (delay <= 0)
            {
                if (JoystickR.Horizontal > 0 || JoystickR.Horizontal < 0)
                {
                    GameObject go = GameObject.Instantiate(shellPrefab, fireposition.position, fireposition.rotation) as GameObject;
                    go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
                    delay = Start_Delay;
                }
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }
        else
        {
            float v = Joystick.Vertical;
            rigidbody.velocity = transform.forward * v * Speed;

            float h = Joystick.Horizontal;
            rigidbody.angularVelocity = transform.up * h * angularSpeed;

            Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
            transform.Translate(translate);
        }
    }
}