using UnityEngine;

public class Icespell : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    private int swapside = 1;
    private int randomshoot = 90;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ+ swapside*randomshoot);
        
        if (!canFire)
        {
            timer += Controller.Instance.skill1cd ;
            if (timer > 200)
            {
                canFire = true;
                timer = 0;
            }
        }

        else if (canFire == true)
        {
            swapside = -swapside;
            randomshoot = Random.Range(30, 100);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            canFire = false;
        }
    }
}
