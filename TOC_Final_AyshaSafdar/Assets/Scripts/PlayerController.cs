using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Fare;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rb;
    private int count;


    public static bool getStatus(string myString)
    {
        string s, revs = "";
        s = myString.Substring(1);
        for (int i = myString.Length - 1; i >= 1; i--) //String Reverse  
        {
            revs += myString[i].ToString();
        }

        if (revs == s) // Checking whether string is palindrome or not  
        {
            return true;
        }
        else
            return false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText(count);
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        TextMesh gameObj = other.gameObject.GetComponentInChildren<TextMesh>();
        int j = 0;
        int numOfPalindrome = 0;
        bool[] arr = new bool[10];

        if (other.gameObject.CompareTag("Pick Up") && j < 10)
        {
            if (getStatus(gameObj.text))
            {
                arr[j] = true;
                other.gameObject.SetActive(false);
                count = count + 1;
                j = j + 1;
                SetCountText(count);
            }
            else
            {
                arr[j] = false;
                j = j + 1;
            }
        }
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == true)
                numOfPalindrome += 1;
        }

        if (numOfPalindrome > 0 && j>=10)
            SetCountText(numOfPalindrome);
    }


    void SetCountText(int a)
    {
        countText.text = "Count: " + count.ToString();
        if (a == GenerateSpawn.allValid)
        {
            winText.text = "The Number of Palindromes are: " + a;
        }
    }
}

//string GenerateString()
//{
//    string pattern = "^x[a2]{9,15}";
//    var xeger = new Xeger(pattern);
//    string generatedString = xeger.Generate();
//    generatedString = xeger.Generate();
//    return generatedString;
//}