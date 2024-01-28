using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIControl2 : MonoBehaviour
{

    [SerializeField]
    private GameObject errorPanel;

    [SerializeField]
    private TextMeshProUGUI textPlaytime;

    [SerializeField]
    private TextMeshProUGUI textMoveCount;

    [SerializeField]
    private Board board;

    public void OnErrorPanel()
    {
        errorPanel.SetActive(true);

        textPlaytime.text = $"Play Time: {board.Playtime / 60:D2}:{board.Playtime % 60:D2}";
        textMoveCount.text = "Move Count: " + board.MoveCount;

    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
