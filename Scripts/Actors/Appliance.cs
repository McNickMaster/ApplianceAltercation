using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Appliance : MonoBehaviour
{
    public static Appliance instance;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;

    public ParticleSystem OnShootParticles;

    private TextMeshProUGUI healthText;

    private Vector2 input, translation, mousePos;
    
    [SerializeField]
    private float playerSpeed = 1, health = 100, maxHealth = 100;

    [SerializeField]
    private bool action = false, stopAction = false, active = true, shooting = false;


    private float time = 0, timeToShoot = 0.5f;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        instance = this;
        health = maxHealth;
        healthText.text = health + "/" + maxHealth + " HP";

        healthText = GameObject.FindGameObjectWithTag("healthText").GetComponent<TextMeshProUGUI>();

    }

    // Start is called before the first frame update
    void Start()
    {
        ResetColor();
        healthText = GameObject.FindGameObjectWithTag("healthText").GetComponent<TextMeshProUGUI>();
    }


    private void FixedUpdate()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetInput()
    {
        healthText = GameObject.FindGameObjectWithTag("healthText").GetComponent<TextMeshProUGUI>();
        healthText.text = health + "/" + maxHealth + " HP";
        if (active)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            action = (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && time <= 0;
            stopAction = Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0);

        } else
        {
            stopAction = true;
            input = Vector2.zero;
            rb.velocity = Vector2.zero;
        }

        if(time >= 0)
        {
            time -= Time.deltaTime;
        }

        

        if (action)
        {
            shooting = true;

            time = timeToShoot;
        }

        if (stopAction)
        {
            shooting = false;
        }

        if (shooting)
        {
            OnShootParticles.Play();
        }


    }

    public void Translate()
    {

        if (active)
        {
            translation = input * playerSpeed;

            rb.velocity = translation;
        }
    }

    public void Rotate()
    {
        if (active)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        SetColor(new Color(0.945098f, 0.7098039f, 0.7098039f), 0.3f);
    }

    public void SetSpeed(float speed)
    {
        playerSpeed = speed;
    }

    public bool IsActionPressed()
    {
        return action;
    }

    public bool IsStopActionPressed()
    {
        return stopAction;
    }

    public void IsActive(bool active)
    {
        this.active = active;
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    void SetColor(Color color, float time)
    {
        sprite = EvolutionSystem.instance.playerSpriteRenderer;
        sprite.color = color;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sprite.color = Color.white;
    }
}
