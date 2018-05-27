using UnityEngine.SceneManagement;

public static class SceneManagement {
    //Collection of methods to handle the transition between scenes
    public static UnityEngine.AsyncOperation LoadAsync(int num)
    {
        return SceneManager.LoadSceneAsync(num);
    }

    public static void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
