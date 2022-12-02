using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    GameObject player;
    bool retry;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        if (retry)
        {
            Play();
            retry = false;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()
    {
        DontDestroyOnLoad(this);
        retry = true;
        SceneManager.LoadScene(0);
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
