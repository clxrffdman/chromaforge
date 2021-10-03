using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public bool isTooltip;
    public GameObject tooltipObject;
    public GameObject tooltipUI;
    public LayerMask layerMask;


    public void removeAllTooltips()
    {
        if (tooltipObject != null)
        {
            Destroy(tooltipObject);
            tooltipObject = null;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Core") {
            removeAllTooltips();
            var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
            newTooltip.transform.SetParent(tooltipUI.transform);
            newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = collision.transform.gameObject;
            newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);


            int colorValue = collision.transform.parent.GetComponentInChildren<LaserProfile>().color;
            //print(colorValue);
            if (colorValue % 2 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/redlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 3 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/greenlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 5 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/bluelight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 6 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/yellowlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 15 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/cyanlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 10 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/magentalight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 30 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/whitelight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }


            tooltipObject = newTooltip;
            isTooltip = true;
        }
        if (collision.gameObject.transform.parent.tag == "Mirror")
        {
            removeAllTooltips();
            var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
            newTooltip.transform.SetParent(tooltipUI.transform);
            newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = collision.transform.parent.gameObject;
            newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);
            tooltipObject = newTooltip;
            isTooltip = true;
        }
        if (collision.gameObject.transform.parent.tag == "Prism")
        {
            removeAllTooltips();
            var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
            newTooltip.transform.SetParent(tooltipUI.transform);
            newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = collision.transform.gameObject;
            newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);


            int colorValue = collision.transform.parent.GetComponentInChildren<LaserProfile>().color;
            //print(colorValue);
            if (colorValue % 2 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/redlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 3 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/greenlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 5 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/bluelight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 6 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/yellowlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 15 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/cyanlight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 10 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/magentalight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }

            if (colorValue % 30 == 0)
            {
                var newColor = Instantiate(Resources.Load<GameObject>("UIElements/ColorIcon"), transform.position, Quaternion.identity);
                newColor.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIElements/whitelight");
                newColor.transform.SetParent(newTooltip.transform.GetComponent<IndividualTooltip>().gridLayoutGroup.transform);
            }


            tooltipObject = newTooltip;
            isTooltip = true;
        }
        if (collision.gameObject.transform.parent.tag == "Tower")
        {
            removeAllTooltips();
            var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
            newTooltip.transform.SetParent(tooltipUI.transform);
            newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = collision.transform.gameObject;
            newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);
            tooltipObject = newTooltip;
            isTooltip = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position.Equals((Input.mousePosition.x, Input.mousePosition.y));
        
    }
}
