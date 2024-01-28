using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private Transform tilesParent;

    private List<Tile> tileList;

    private Vector2Int puzzleSize = new Vector2Int(4, 4);
    private float neighborTileDistance = 154;     //인접한 타일 사이의 거리. 별도로 계산

    public Vector3 EmptyTilePosition { set; get; }
    public int Playtime { private set; get; } = 0; //플탐
    public int MoveCount { private set; get; } = 0; //이동횟수
    public int ErrorCount { private set; get; } = 0;

    private IEnumerator Start()
    {
        tileList = new List<Tile>();

        SpawnTiles();

        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());

        //현재 프레임이 종료될 때까지 대기

        yield return new WaitForEndOfFrame();

        //tileList에 있는 모든 요소의 SetCorrectPosition()메소드 호출
        tileList.ForEach(x => x.SetCorrectPosition());

        StartCoroutine("OnSuffle");
        StartCoroutine("CalculatePlaytime");

        int[] array = new int[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            array[i] = transform.GetChild(i).GetComponent<Tile>().Numeric;
        }

    }

    private void SpawnTiles()
    {
        for (int y = 0; y < puzzleSize.y; ++y)
        {
            for (int x = 0; x < puzzleSize.x; ++x)
            {
                GameObject clone = Instantiate(tilePrefab, tilesParent);
                Tile tile = clone.GetComponent<Tile>();

                tile.Setup(this, puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);

                tileList.Add(tile);
            }
        }

    }

    private IEnumerator OnSuffle()
    {
        float current = 0;
        float percent = 0;
        float time = 1.5f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            int index = Random.Range(0, (puzzleSize.x * puzzleSize.y) - 5);
            tileList[index].transform.SetAsLastSibling();

            yield return null;

        }


        EmptyTilePosition = tileList[tileList.Count - 1].GetComponent<RectTransform>().localPosition;
    }

    public void IsMoveTile(Tile tile)
    {
        if (Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
        {
            Vector3 goalPosition = EmptyTilePosition;
            EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;

            tile.OnMoveTo(goalPosition);

            //타일 이동할 때마다 이동횟수 증가
            MoveCount++;

        }
    }

    public void IsGameOver()
    {
        List<Tile> tiles = tileList.FindAll(x => x.IsCorrected == true);

        Debug.Log("Correct Count : " + tiles.Count);

        if (tiles.Count == puzzleSize.x * puzzleSize.y - 1)
        {
            Debug.Log("GameClear");
            //게임 클리어시 시간 중지
            StopCoroutine("CalculatePlaytime");
            /*board 오브젝트에 컴포넌트 설정
             한번만 호출하기 때문에 변수 x 바로 호출*/
            GetComponent<UIController>().OnResultPanel();
        }


        List<Tile> tile = tileList.FindAll(x => x.IsCorrected == false);

        if (tiles.Count == 13)
        {
            ErrorCount++;
            Debug.Log("ErrorCount:" + ErrorCount);
        }
            if (ErrorCount == 10)
            {
                //에러카운트 열번 발생 시 더이상 게임 진행불가. 강제 게임 정지 후 재시작 유도
                Debug.Log("GameError!");
                StopCoroutine("CalculatePlaytime");
            GetComponent<UIController>().OnResultPanel();
        }

    }


    private IEnumerator CalculatePlaytime()
    {
        while (true)
        {
            Playtime++;

            yield return new WaitForSeconds(1);
        }
    }
}

