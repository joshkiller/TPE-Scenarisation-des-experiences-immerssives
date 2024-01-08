using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void DriverBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void PedestrianBtn()
    {
        SceneManager.LoadScene(2);
    }

}
