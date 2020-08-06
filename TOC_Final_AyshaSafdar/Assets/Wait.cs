using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Wait : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void Game()
    {
        SceneManager.LoadScene(1);
    }
}
