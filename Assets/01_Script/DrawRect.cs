using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawRect : MonoBehaviour
{
    [SerializeField] private GameObject rectangle;
    [SerializeField] private ChangeColor changeColor;

    private LineRenderer lineRend;
    public Vector3 screen;
    public Vector3 initialMousePosition, currentMousePosition;
    public List<GameObject> correctColor = new List<GameObject>();
    public List<GameObject> currentColor = new List<GameObject>();
    public bool isWin=false;

    public UIController uIController;


    private float area;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            screen = Input.mousePosition;
            screen.z = Camera.main.nearClipPlane + 10;
            lineRend.positionCount = 4;
            initialMousePosition = Camera.main.ScreenToWorldPoint(screen);
            lineRend.SetPosition(0, new Vector3(initialMousePosition.x, initialMousePosition.y, -0.40f));    
            lineRend.SetPosition(1, new Vector3(initialMousePosition.x, initialMousePosition.y, -0.40f));
            lineRend.SetPosition(2, new Vector3(initialMousePosition.x, initialMousePosition.y, -0.40f));
            lineRend.SetPosition(3, new Vector3(initialMousePosition.x, initialMousePosition.y, -0.40f));
        }

        if (Input.GetMouseButton(0))
        {
            screen = Input.mousePosition;
            screen.z = Camera.main.nearClipPlane + 10;
            currentMousePosition = Camera.main.ScreenToWorldPoint(screen);
            
            lineRend.SetPosition(0, new Vector3(initialMousePosition.x, initialMousePosition.y, -0.40f));
            lineRend.SetPosition(1, new Vector3(initialMousePosition.x, currentMousePosition.y, -0.40f));
            lineRend.SetPosition(2, new Vector3(currentMousePosition.x, currentMousePosition.y, -0.40f));
            lineRend.SetPosition(3, new Vector3(currentMousePosition.x, initialMousePosition.y, -0.40f));
            rectangle.transform.position = new Vector3((initialMousePosition.x + currentMousePosition.x) / 2, (initialMousePosition.y + currentMousePosition.y) / 2, -0.2f);
            rectangle.transform.localScale = new Vector3(Mathf.Abs(initialMousePosition.x - currentMousePosition.x), Mathf.Abs(initialMousePosition.y - currentMousePosition.y), 0.01f);
            area = Mathf.Abs(
                (initialMousePosition.x - currentMousePosition.x) *
                (initialMousePosition.y - currentMousePosition.y));
        }
        if (Input.GetMouseButtonUp(0))
        {
            lineRend.SetPosition(0, Vector3.zero);
            lineRend.SetPosition(1, Vector3.zero);
            lineRend.SetPosition(2, Vector3.zero);
            lineRend.SetPosition(3, Vector3.zero);
            rectangle.transform.position = new Vector3(10, 10, 10);
            rectangle.transform.localScale = Vector3.one;
            if (changeColor.cubes.Count>0)
            {
                ChangeColor();
                Check();
            }
        }
        
    }
    public void ChangeColor()
    {
        Color firstColor = changeColor.cubes[0].GetComponent<MeshRenderer>().materials[0].color;
        string firstName = changeColor.cubes[0].name;
        for (int i = 1; i < changeColor.cubes.Count; i++)
        {
            changeColor.cubes[i].GetComponent<MeshRenderer>().materials[0].color = firstColor;
            changeColor.cubes[i].name = firstName;
        }
    }
    public void Check()
    {
        bool isCheck = true;
        for (int i = 0; i < currentColor.Count; i++)
        {
            if (currentColor[i].name != correctColor[i].name)
            {
                isCheck = false;
            }
        }
        if (isCheck)
        {
            StartCoroutine(LevelEndPart());
        }
    }
    public IEnumerator LevelEndPart()
    {
        yield return new WaitForSeconds(1);
        uIController.LevelComplated();
    }
}
