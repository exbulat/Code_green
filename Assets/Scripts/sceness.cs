using UnityEngine;
using UnityEngine.SceneManagement;
public class sceness : MonoBehaviour
{
    public void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);

    }




}
