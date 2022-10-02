using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI foodThrownText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setFoodThrownUI(int foodThrown)
    {
        foodThrownText.text += " "+foodThrown.ToString();
    }
}
