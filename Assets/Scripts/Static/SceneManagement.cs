using UnityEngine.SceneManagement;

public static class SceneManagement {

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
