using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rb;
    private int count;


    static bool isMatchingPair(char character1, char character2)
    {
        if (character1 == '(' && character2 == ')')
            return true;
        else
            return false;
    }

    public static bool getStatus(string myString)
    {
        char[] exp = myString.ToCharArray();
        Stack<char> st = new Stack<char>();

        for (int i = 0; i < exp.Length; i++)
        {

            /*If the exp[i] is a starting  
                parenthesis then push it*/
            if (exp[i] == '(')
                st.Push(exp[i]);

            /* If exp[i] is an ending parenthesis  
                then pop from stack and check if the  
                popped parenthesis is a matching pair*/
            if (exp[i] == ')')
            {

                /* If we see an ending parenthesis without  
                    a pair then return false*/
                if (st.Count == 0)
                {
                    return false;
                }

                /* Pop the top element from stack, if  
                    it is not a pair parenthesis of character  
                    then there is a mismatch. This happens for  
                    expressions like {(}) */
                else if (!isMatchingPair(st.Pop(), exp[i]))
                {
                    return false;
                }
            }
        }

        if (st.Count == 0)
            return true;
        else
        {
            return false;
        }
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
        int validStrings = 0;
        bool[] arr = new bool[10];

        if (other.gameObject.CompareTag("Pick Up") && j <= 5)
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
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
            other.gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 255);
                arr[j] = false;
                j = j + 1;
            }
        }
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == true)
                validStrings += 1;
        }
        SetCountText(validStrings);
    }


    void SetCountText(int a)
    {

        countText.text = "Count: " + count.ToString();
        if (a == GenerateObject.allValid)
        {
            
            winText.text = "Congratulations...! All cubes relevent got collected. No. of cubes= " + a + " objects";
        }
    }
}

//string GenerateString()
//{
//    string pattern = "^x[a4]{9,15}";
//    var xeger = new Xeger(pattern);
//    string generatedString = xeger.Generate();
//    generatedString = xeger.Generate();
//    return generatedString;
//}