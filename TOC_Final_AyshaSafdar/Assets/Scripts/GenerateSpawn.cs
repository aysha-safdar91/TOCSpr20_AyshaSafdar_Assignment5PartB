using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GenerateSpawn : MonoBehaviour
{
    //public Text nameLabel;
    //public GameObject sph;
    public Canvas canvas;
    public GameObject sphere;
    public GameObject pickUpPrefab;
    private int xPos;
    //private int yPos = 1;
    private int zPos;
    private int spawnCount;
    private List<string> oneThird = new List<string>();
    [HideInInspector]
    public static int allValid = 0;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        while (i <= 3)
        {
            string str = RandomString();
            if (PlayerController.getStatus(str))
            {
                oneThird.Add(str);
                allValid += 1;
                i++;
            }
        }
        StartCoroutine(DropSpawn());
    }


    private static System.Random random = new System.Random();
    public static string RandomString()
    {
        int length = random.Next(9, 16);
        const string chars = "a2";
        string abc = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        return "x" + abc;
    }


    IEnumerator DropSpawn()
    {
        GameObject randomText = new GameObject("Collectible Text");
        while (spawnCount <= 9)
        {

            xPos = Random.Range(-23, 23);
            zPos = Random.Range(-31, 31);
            GameObject xyz = Instantiate(pickUpPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
            GameObject sphh = Instantiate(sphere, new Vector3(xPos, 2, zPos), Quaternion.identity);
            sphh.transform.parent = xyz.transform;
            GameObject childTxt = new GameObject();
            childTxt.transform.parent = sphh.transform;
            childTxt.name = "Text Holder";
            TextMesh randText = childTxt.AddComponent<TextMesh>();

            if (oneThird.Any())
            {
                foreach (var item in oneThird)
                {
                    randText.text = item;
                    randText.anchor = TextAnchor.MiddleCenter;
                    randText.alignment = TextAlignment.Center;
                    randText.transform.position = sphh.transform.position;
                    oneThird.Remove(item);
                    break;
                }
                yield return new WaitForSeconds(0.01f);
                spawnCount += 1;
            }
            else
            {
                string str = RandomString();
                randText.text = str;
                if (PlayerController.getStatus(str))
                {
                    allValid = allValid + 1;
                }
                //textMesh.tag = "Random Text";
                ////Set postion of the TextMesh with offset
                randText.anchor = TextAnchor.MiddleCenter;
                randText.alignment = TextAlignment.Center;
                randText.transform.position = sphh.transform.position;
                yield return new WaitForSeconds(0.01f);
                spawnCount += 1;

            }

        }
    }
}
