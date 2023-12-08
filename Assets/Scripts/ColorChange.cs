using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState { Dead, Alive }
public enum PlayerGround { OnGround, Falling }

public class ColorChange : MonoBehaviour
{
    //Material m_Material, material_name;
    Material m_Material;
    Renderer rend;
    public List<Material> colorList;
    private Rigidbody rBody;
    private Collider m_Collider;
    public PlayerState playerState;
    public PlayerGround playerGround;
    public float fallingTime;
    public GameObject victory, lose;
    void Start()
    {
        playerState = PlayerState.Alive;
        playerGround = PlayerGround.Falling;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = colorList[0];
        m_Collider = GetComponent<MeshCollider>();
        m_Material = GetComponent<Renderer>().material;
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Material = GetComponent<Renderer>().material;
        CheckGround();
        CheckLife();
    }

    void CheckLife()
    {
        if(playerState == PlayerState.Dead)
        {
            lose.SetActive(true);
            StartCoroutine(Lose());
        }
    }

    void CheckGround()
    {
        if (playerGround == PlayerGround.Falling)
        {
            fallingTime += Time.deltaTime;
            if (fallingTime >= 3.0f)
            {
                playerState = PlayerState.Dead;
            }
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnCollisionStay(Collision other){
        if (other.gameObject.tag == "Floor"){
            if (this.m_Material.name == other.gameObject.GetComponent<Renderer>().material.name){
                other.collider.enabled = false;
                playerGround = PlayerGround.Falling;
            }else{
                other.collider.enabled = true;
                playerGround = PlayerGround.OnGround;
                fallingTime = 0f;
            }
        }

        if (other.gameObject.tag == "Obstacle"){
            if (this.m_Material.name == other.gameObject.GetComponent<Renderer>().material.name){
                other.collider.enabled = false;
            }else{
                other.collider.enabled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            playerState = PlayerState.Dead;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Objective")
        {
            victory.SetActive(true);
            StartCoroutine(Victory());
        }
    }

    public void changeColor(string name)
    {
        switch (name)
        {
            case "Red":
                rend.sharedMaterial = colorList[1];
                break;
            case "Blue":
                rend.sharedMaterial = colorList[2];
                break;
            case "Green":
                rend.sharedMaterial = colorList[3];
                break;
            case "Yellow":
                rend.sharedMaterial = colorList[4];
                break;
            case "Orange":
                rend.sharedMaterial = colorList[5];
                break;
            default:
                break;
        }
    }
}