using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeachBtnUi : MonoBehaviour
{
    public GameObject TeachPlane;
    private bool isOpenTeachPlane = false;

    // Start is called before the first frame update
    void Start()
    {
        isOpenTeachPlane = false;
        TeachPlane.SetActive(false);
        this.GetComponent<Button>().onClick.AddListener(ShowTeachPlane);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpenTeachPlane && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            ShowTeachPlane();
        }
    }

    void ShowTeachPlane()
    {
        if (!isOpenTeachPlane)
            TeachPlane.SetActive(true);
        else
            TeachPlane.SetActive(false);

        isOpenTeachPlane = TeachPlane.activeSelf;
    }
}
