using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolutionSystem : MonoBehaviour
{

    public static EvolutionSystem instance;

    public ChoicePopulator choicePop;

    public EvolutionAction[] evolveEffects;

    [SerializeField]
    private EvolutionAction evolveEffect;

    public GameObject playerContainer;

    public GameObject currentAppliance;
    public GameObject[] appliances;

    public TextMeshProUGUI level;

    public Canvas gameOverScreen;

    private int currentApplianceIndex = 0;

    public SimpleHealthBar xpBar;

    private bool isAbleToEvolve = true;

    public TextMeshProUGUI readyToEvolveNotice;

    public SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    private float xpAmount = 0, xpToLevelUp = 500;

    [SerializeField]
    private int playerLevel = 0, evolutionLevel = 0, evolution1Level = 1, evolution2Level = 2, winGameLevel = 3;

    private bool isIce = false;

    public bool isEvolving = false;
    /*
     * 0 = Fuzz
     * 1 = microwave
     * 2 - Oven
     * 3 - Space Heater
     * 4 - Mini fridge
     * 5 - Freezer
     * 6 - A-C
     *
     * 
     */

    private void Awake()
    {
        instance = this;
        readyToEvolveNotice.enabled = false;
        Time.timeScale = 1;
        gameOverScreen.enabled = false;

        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetPlayer(fireAppliances[0]);
    }

    private void FixedUpdate()
    {
        playerSpriteRenderer = currentAppliance.GetComponentInChildren<SpriteRenderer>();
        currentAppliance.GetComponent<Appliance>().IsActive(!isEvolving);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAppliance.GetComponent<Appliance>().GetHealth() <= 0)
        {
            GameOver();
        }

        if (currentApplianceIndex == 1 || currentApplianceIndex == 2 || currentApplianceIndex == 3)
        {
            evolveEffect = evolveEffects[0];
        } else if (currentApplianceIndex == 0)
        {

            evolveEffect = evolveEffects[2];

        } else
        {
            evolveEffect = evolveEffects[1];
        }


        if (Input.GetKeyDown(KeyCode.Alpha6) && Input.GetKeyDown(KeyCode.Alpha9))
        {
            xpAmount = xpToLevelUp;
        }

        if(xpAmount >= xpToLevelUp)
        {
            LevelUp();
        }

        if((playerLevel == evolution1Level && evolutionLevel == 0) || (playerLevel == evolution2Level && evolutionLevel == 1)){

            isAbleToEvolve = true;

        } else
        {

            isAbleToEvolve = false;

        }

        xpBar.UpdateBar(xpAmount, xpToLevelUp);

        readyToEvolveNotice.enabled = isAbleToEvolve;

        if (isAbleToEvolve)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Evolve();
            }
        }

        if (playerLevel >= evolution2Level)
        {
            xpAmount = xpToLevelUp;
            isAbleToEvolve = false;
        }

        

        if (playerLevel >= 2 && evolutionLevel == 2)
        {
            level.text = "MAX EVOLUTION";

        } else
        {
            level.text = "LEVEL: " + playerLevel;
        }
    }

    void WinGame()
    {
        print("you won");
    }
    
    void GameOver()
    {
        gameOverScreen.enabled = true;
        Time.timeScale = 0;
        print("you lost");
    }

    void LevelUp()
    {
        if(playerLevel >= evolution2Level)
        {
            //xpAmount = xpToLevelUp;
            
        } else
        {
            xpAmount = 0;
            playerLevel++;
            xpToLevelUp *= 4;
            print("level up");
        }

    }

    void Evolve()
    {
        isEvolving = true;
        evolveEffect.StartEffect();
        choicePop.gameObject.SetActive(true);
        choicePop.PopChoices(DecideEvolutionOptions());
        evolutionLevel++;
        isAbleToEvolve = false;
    }

    int[] DecideEvolutionOptions()
    {
        int[] choice = new int[2];

        switch (currentApplianceIndex)
        {
            //fuzz
            case 0:
                {
                    choice[0] = 1;
                    choice[1] = 4;
                    break;
                }
            //mw
            case 1:
                {
                    choice[0] = 2;
                    choice[1] = 3;
                    break;
                }
            //oven
            case 2:
                {

                    break;
                }
            //sh
            case 3:
                {

                    break;
                }
            //fan
            case 4:
                {
                    choice[0] = 5;
                    choice[1] = 6;
                    break;
                }
            // fridge
            case 5:
                {

                    break;
                }
            //ac
            case 6:
                {

                    break;
                }
        }

        return choice; 
    }

   

    // choice is 1 or 2
    void UpgradeAppliance(int choice)
    {
        
        SetPlayer(appliances[choice]);
        evolveEffect.StopEffect();
        isEvolving = false;
        choicePop.gameObject.SetActive(false);
    }


    public void SetPlayer(GameObject newAppliance)
    {
        GameObject temp = Instantiate(newAppliance, currentAppliance.transform.position, currentAppliance.transform.rotation, playerContainer.transform);
        Destroy(currentAppliance);
        currentAppliance = temp;
        
    }

    public Transform GetCurrentPlayerTransform()
    {
        return currentAppliance.transform;
    }

    public void AddXP(float xp)
    {
        xpAmount += xp;
    }
    
    public void Choice(Image choiceImage)
    {

        switch (choiceImage.sprite.name)
        {
            case "fuzz_0":
                {
                    currentApplianceIndex = 0;
                    break;
                }
            case "MicrowaveFull_0":
                {
                    currentApplianceIndex = 1;
                    break;
                }
            case "OvenAnimation_1":
                {
                    currentApplianceIndex = 2;
                    break;
                }
            case "SpaceHeaterAnimation_0":
                {
                    currentApplianceIndex = 3;
                    break;
                }
            case "fan0000_0":
                {
                    currentApplianceIndex = 4;
                    break;
                }
            case "miniFridgeFull_0":
                {
                    currentApplianceIndex = 5;
                    break;
                }

            case "ac_0":
                {
                    currentApplianceIndex = 6;
                    break;
                }

        }

        UpgradeAppliance(currentApplianceIndex);


        
    }
}
