using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Stack : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI HighScore;
    public GameObject endPanel;
    public AdManager ad;

    private GameObject[] stack;
    const float SIZE = 3.5f;
    private int index;
    private float pos;
    private float speed = 2.5f;
    public Color[] color;
    public int colorIndex = 0;

    private bool movingX = true;

    public Material mat;


    private int score = 0;
    
    
    private float transition = 0.0f;
    private Vector3 stackPosition;
    private Vector3 lastTilePos;
    private float stackSpeed = 5.0f;
    private float ERROR_MARGIN = 0.15f;
    private int COMBO = 0;
    private Vector2 stackSize = new Vector2(SIZE, SIZE);
    private bool gameOver = false;
    private int COMBO_INCREMENT = 3;
    private float GAIN = 0.25f;

    void Start()
    {
        stack = new GameObject[transform.childCount];
        index = transform.childCount - 1;
        score = -transform.childCount;
        for(int i = 0; i < stack.Length; i++)
        {
            stack[i] = transform.GetChild(i).gameObject;
            applyColor(stack[i].GetComponent<MeshFilter>().mesh, score);
            score++;
        }
    }

    void Update()
    {
        if (gameOver)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            if(dropTile())
            {
                newTile();
                score++;
                if(PlayerPrefs.GetInt("Music") == 1)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                }
                scoreText.text = score.ToString();
            }
            else
            {
                endGame();
            }
        }
        move();
        transform.position = Vector3.Lerp(transform.position, stackPosition, stackSpeed * Time.deltaTime);
    }

    void cutBox(Vector3 position, Vector3 scale)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localPosition = position;
        cube.transform.localScale = scale;
        cube.GetComponent<MeshRenderer>().material = mat;
        applyColor(cube.GetComponent<MeshFilter>().mesh, score - 1);
        cube.AddComponent<Rigidbody>();
    }

    void move()
    {
        transition += Time.deltaTime * speed;
        if(movingX)
        {
            stack[index].transform.localPosition = new Vector3(Mathf.Sin(transition) * SIZE, score, pos);
        }
        else
        {
            stack[index].transform.localPosition = new Vector3(pos, score, Mathf.Sin(transition) * SIZE);
        }
        
    }

    void newTile()
    {
        lastTilePos = stack[index].transform.localPosition;
        index--;
        if (index < 0)
        {
            index = transform.childCount - 1;
        }
        stackPosition = Vector3.down * score;
        stack[index].transform.localPosition = new Vector3(0, score, 0);
        stack[index].transform.localScale = new Vector3(stackSize.x, 1, stackSize.y);
        applyColor(stack[index].GetComponent<MeshFilter>().mesh, score);
    }


    bool dropTile()
    {

        if(movingX)
        {
            float delta = lastTilePos.x - stack[index].transform.position.x;
            if(Mathf.Abs(delta) > ERROR_MARGIN)
            {
                COMBO = 0;
                stackSize.x -= Mathf.Abs(delta);
                if(stackSize.x <= 0)
                {
                    return false;
                }

                float mid = lastTilePos.x + stack[index].transform.localPosition.x / 2;
                stack[index].transform.localScale = (new Vector3(stackSize.x, 1, stackSize.y));
                cutBox(new Vector3(stack[index].transform.position.x > 0 ?
                                        stack[index].transform.position.x + stack[index].transform.localScale.x / 2 :
                                        stack[index].transform.position.x - stack[index].transform.localScale.x / 2,
                                    stack[index].transform.position.y,
                                    stack[index].transform.position.z),
                       new Vector3(Mathf.Abs(delta), 1, stack[index].transform.localScale.z));
                stack[index].transform.localPosition = new Vector3(mid - (lastTilePos.x / 2), score, lastTilePos.z);
            }
            else
            {
                if (COMBO > COMBO_INCREMENT)
                {
                    stackSize.x += GAIN;
                    if(stackSize.x > SIZE)
                    {
                        stackSize.x = SIZE;
                    }
                    float mid = lastTilePos.x + stack[index].transform.localPosition.x / 2;
                    stack[index].transform.localScale = (new Vector3(stackSize.x, 1, stackSize.y));
                    stack[index].transform.localPosition = new Vector3(mid - (lastTilePos.x / 2), score, lastTilePos.z);
                }
                COMBO++;
                stack[index].transform.localPosition = new Vector3(lastTilePos.x, score, lastTilePos.z);
            }
            pos = stack[index].transform.localPosition.x;
        }

        else
        {
            float delta = lastTilePos.z - stack[index].transform.position.z;
            if (Mathf.Abs(delta) > ERROR_MARGIN)
            {
                COMBO = 0;
                stackSize.y -= Mathf.Abs(delta);
                if (stackSize.y <= 0)
                {
                    return false;
                }

                float mid = lastTilePos.z + stack[index].transform.localPosition.z / 2;
                stack[index].transform.localScale = (new Vector3(stackSize.x, 1, stackSize.y));
                cutBox(new Vector3(stack[index].transform.position.x,
                                    stack[index].transform.position.y,
                                    stack[index].transform.position.z > 0 ?
                                        stack[index].transform.position.z + stack[index].transform.localScale.z / 2 :
                                        stack[index].transform.position.z - stack[index].transform.localScale.z / 2),
                       new Vector3(stack[index].transform.localScale.x, 1, Mathf.Abs(delta)));
                stack[index].transform.localPosition = new Vector3(lastTilePos.x, score, mid - (lastTilePos.z / 2));
            }
            else
            {
                if(COMBO > COMBO_INCREMENT)
                {
                    stackSize.y += GAIN;
                    if (stackSize.y > SIZE)
                    {
                        stackSize.y = SIZE;
                    }
                    float mid = lastTilePos.z + stack[index].transform.localPosition.z / 2;
                    stack[index].transform.localScale = (new Vector3(stackSize.x, 1, stackSize.y));
                    stack[index].transform.localPosition = new Vector3(lastTilePos.x, score, mid - (lastTilePos.z / 2));
                }
                COMBO++;
                stack[index].transform.localPosition = new Vector3(lastTilePos.x, score, lastTilePos.z);
            }
            pos = stack[index].transform.localPosition.z;
        }
        
        movingX = !movingX;
        return true;
    }

    void applyColor(Mesh mesh, int s)
    {
        Vector3[] vertex = mesh.vertices;
        Color[] colors = new Color[vertex.Length];
        for(int i = 0; i < vertex.Length; i++)
        {
            colors[i] = colorChange(color, s);
        }
        mesh.colors = colors;
    }

    Color colorChange(Color[] color, int s)
    {
        return color[Mathf.Abs(s % 7)];
    }

    void endGame()
    {
        ChangeHighScore();
        gameOver = true;
        stack[index].AddComponent<Rigidbody>();
        DisplayScore();
        ad.showInterstitialAd();
        endPanel.SetActive(true);
    }

    void ChangeHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void DisplayScore()
    {
        scoreText.enabled = false;
        finalScore.text = "Score: " + score.ToString();
        HighScore.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
