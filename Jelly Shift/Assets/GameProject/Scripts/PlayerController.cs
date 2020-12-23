using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerController : MonoBehaviour
{

    public float speed = 0.5f, computerSpeed;

    private Vector3 startScale, endScale, tempScale;

    private Touch initTouch = new Touch();
    private float pcInitTouch = 0;
    private bool touching = false;

    GameManager manager;

    void Start()
    {
        manager = GameManager.instance;
        tempScale = startScale = transform.localScale;
        endScale = new Vector3(startScale.y, startScale.x, startScale.z);
    }

    void Update()
    {

        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)//Find plotform
        {
            computerController();
            //Play with W and S buttons
            if (Input.GetKey(KeyCode.W))
            {
                tempScale.x -= computerSpeed * Time.deltaTime;
                tempScale.y += computerSpeed * Time.deltaTime;
            }

            else if (Input.GetKey(KeyCode.S))
            {
                tempScale.x += computerSpeed * Time.deltaTime;
                tempScale.y -= computerSpeed * Time.deltaTime;
            }
        }
        else
        {
            phoneController();
        }

        tempScale.x = Mathf.Clamp(tempScale.x, endScale.x, startScale.x);//Restric scaling
        tempScale.y = Mathf.Clamp(tempScale.y, startScale.y, endScale.y);

        transform.localScale = tempScale;
    }
    
    void computerController() // with  mouse
    {
        if (Input.GetMouseButton(0))
        {
            float initPos = pcInitTouch;
            if (Input.GetMouseButtonDown(0) && !touching)//if pointer click the screen
            {
                touching = true;
                pcInitTouch = Input.mousePosition.y;
            }
            else if (Input.GetMouseButton(0))//if pointer moves while clicking the screen
            {
                float deltaY = initPos - Input.mousePosition.y;


                tempScale.x -= deltaY * -speed * Time.deltaTime;
                tempScale.y += deltaY * -speed * Time.deltaTime;

                tempScale.x = Mathf.Clamp(tempScale.x, endScale.x, startScale.x);
                tempScale.y = Mathf.Clamp(tempScale.y, startScale.y, endScale.y);

                transform.localScale = tempScale;

                pcInitTouch = Input.mousePosition.y;
            }
            else if (Input.GetMouseButtonUp(0))//if pointer releases the button
            {
                pcInitTouch = 0;
                touching = false;
            }
        }
    }

    void phoneController() // with touchscreen
    {
        if (Input.touchCount > 0)
        {
            float initPos = initTouch.position.y;

            if (Input.GetTouch(0).phase == TouchPhase.Began)        //if finger touches the screen
            {
                if (touching == false)
                {
                    touching = true;
                    initTouch = Input.GetTouch(0);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)       //if finger moves while touching the screen
            {
                float deltaY = initPos - Input.GetTouch(0).position.y;


                tempScale.x -= deltaY * -speed * Time.deltaTime;
                tempScale.y += deltaY * -speed * Time.deltaTime;

                tempScale.x = Mathf.Clamp(tempScale.x, endScale.x, startScale.x);
                tempScale.y = Mathf.Clamp(tempScale.y, startScale.y, endScale.y);

                transform.localScale = tempScale;

                initTouch = Input.GetTouch(0);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)       //if finger releases the screen
            {
                initTouch = new Touch();
                touching = false;
            }
        }
    }

    

}
