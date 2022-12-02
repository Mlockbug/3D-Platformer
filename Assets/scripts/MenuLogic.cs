using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(0);
        Play();
        Destroy(gameObject);
    }

    public void Play()
	{
        player.GetComponent<CharacterControl>().PressedPlay();
	}

	public void Quit()
	{
        Application.Quit();
	}

    public void GoToMenu()
	{
        SceneManager.LoadScene(0);
	}


}
