using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoicePopulator : MonoBehaviour
{

    public Sprite[] sprites;

    public int currentAppliance = 0;

    public Image choice1Image, choice2Image;
    public TextMeshProUGUI choice1Title, choice2Title;
    private Sprite choice1, choice2;


    private List<string> applianceTitles = new List<string>();

    private void Awake()
    {
        applianceTitles.Add("FUZZ");
        applianceTitles.Add("MIRCOWAVE");
        applianceTitles.Add("OVEN");
        applianceTitles.Add("SPACE\nHEATER");
        applianceTitles.Add("CEILING\nFAN");
        applianceTitles.Add("FRIDGE");
        applianceTitles.Add("AIR\nCONDITIONER");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void PopChoices(int one, int two)
    {
        choice1 = sprites[one];
        choice2 = sprites[two];

        choice1Image.sprite = choice1;
        choice2Image.sprite = choice2;
    }

    public void PopChoices(int[] choices)
    {
        choice1 = sprites[choices[0]];
        choice2 = sprites[choices[1]];

        choice1Image.sprite = choice1;
        choice2Image.sprite = choice2;

        choice1Title.text = applianceTitles[choices[0]];
        choice2Title.text = applianceTitles[choices[1]];

    }
}
