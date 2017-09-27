using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour 
{
	public int LevelNumToLoad{ get; private set;}

	void OnEnable()
	{
		LevelChoise.OnLevelChoised += LoadNextLevel;
		ButtonBack.OnButtonBackPress += LoadNextLevel;
	}

	void OnDisable()
	{
		LevelChoise.OnLevelChoised -= LoadNextLevel;
		ButtonBack.OnButtonBackPress -= LoadNextLevel;
	}

	void LoadNextLevel()
	{
		if (SceneManager.GetActiveScene ().buildIndex == 0)
			SceneManager.LoadScene (1);
		else
			SceneManager.LoadScene (0);
	}
}
