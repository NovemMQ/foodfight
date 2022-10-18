using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI foodThrownText;
    public delegate void UIUpdate();
    public static event UIUpdate UIUpdateFood;
    // Start is called before the first frame update
    void Start ()
    {
    }


    public void setFoodThrownUI(int foodThrown)
    {
        Debug.Log("food thrown here "+ foodThrown);
        foodThrownText.text = "Food Thrown " +foodThrown.ToString();
    }


}
