using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName ="UI",menuName ="UIButtons")]
public class UI : ScriptableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonA()
    {
        SceneManager.LoadScene("A");
    }
    public void ButtonB()
    {
        SceneManager.LoadScene("B");
    }
    public void ButtonC()
    {
        SceneManager.LoadScene("C");
    }
    public void ButtonD()
    {
        SceneManager.LoadScene("D");
    }
}
