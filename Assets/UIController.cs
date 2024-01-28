using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private GameObject resultPanel;

    //[SerializeField]
    //private GameObject Warning;

    [SerializeField]
    private TextMeshProUGUI textPlaytime;

    [SerializeField]
    private TextMeshProUGUI textMoveCount;

    [SerializeField]
    private Board board;

    public void OnResultPanel()
    {
        resultPanel.SetActive(true);

        textPlaytime.text = $"Play Time: {board.Playtime / 60:D2}:{board.Playtime % 60:D2}";
        textMoveCount.text = "Move Count: " + board.MoveCount;

    }

    /*
    public void Onwarning()
    {
        Warning.SetActive(true);
    }
    */

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
