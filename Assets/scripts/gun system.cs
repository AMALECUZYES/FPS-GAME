using UnityEngine;

public class gunsystem : MonoBehaviour
{
    public Transform FiringPosition;
    public float gunRange = 100f;
    public GameObject bullet;
    public GameObject muzzleflash;
    public GameObject Bulletimpact;
    public float closeRange;
    public Transform myCameraHead;
    public bool autofire = false;
    public int magsize = 20;
    public int currentbulletsingun;
    public int bulletsingun = 0;
    public int totalbullets = 30;
    public bool canautofire = true;
    public bool shootingInput;
    // Start is called before the first frame update
    void Start()
    {
        currentbulletsingun = magsize;
    }

    // Update is called once per frame
    void Update()
    {
        shooting();
    }

    
    
    private void shooting()
    {
        if (canautofire)
        {
            shootingInput = Input.GetMouseButton(0);
        } 
         
        else
        {
            shootingInput = Input.GetMouseButtonDown(0);
        }

        
        //Debug.DrawRay(FiringPosition.position, FiringPosition.forward, color.Red);

        if (shootingInput && bulletsingun > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                if (Vector3.Distance(FiringPosition.position, hit.point) < closeRange)
                {

                    FiringPosition.LookAt(FiringPosition.transform.forward + closeRange);

                }
                else
                {
                    FiringPosition.LookAt(hit.point);
                }
                //if (!hit.collidor)
                
                
                
                Instantiate(Bulletimpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
            Instantiate(bullet, FiringPosition.position, FiringPosition.rotation);
            Instantiate(muzzleflash, FiringPosition.position, FiringPosition.rotation);
            Instantiate(Bulletimpact);
        }
    }

    private void reload()
    {

        if (Input.GetKey(KeyCode.R) && bulletsingun < magsize)
        {
            int bulletstoadd = magsize - bulletsingun;
            if (totalbullets > bulletstoadd)
            {
                totalbullets -= bulletstoadd;
                bulletsingun += bulletstoadd;
            }
            else
            {
                bulletsingun += totalbullets;
                totalbullets = 0;
            }    
                    
        }
                
    }

}