using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{


    public bool isTooltip;
    public GameObject tooltipObject;
    public GameObject tooltipUI;
    public LayerMask layerMask;
    public GameObject cursor;
    public void removeAllTooltips()
    {
        if(tooltipObject != null)
        {
            Destroy(tooltipObject);
            tooltipObject = null;
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    /*
    // Update is called once per frame
    void Update()
    {

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, layerMask);
        if(cursor != null)
        {
            cursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (rayHit)
        {
            Debug.Log(rayHit.transform.name);


            if (!isTooltip && rayHit.transform.parent.name.Equals("InteractableTower"))
            {
                removeAllTooltips();
                var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
                newTooltip.transform.SetParent(tooltipUI.transform);
                newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = rayHit.transform.gameObject;
                newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);
                tooltipObject = newTooltip;
                isTooltip = true;

            }
            else if (!isTooltip && rayHit.transform.parent.name.Equals("PrismTower"))
            {
                removeAllTooltips();
                var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
                newTooltip.transform.SetParent(tooltipUI.transform);
                newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = rayHit.transform.gameObject;
                newTooltip.GetComponent<IndividualTooltip>().text.text = "Prism: Absorbs all light and spits out the sum! (Press R while selected to rotate)";
                newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);


                int colorValue = rayHit.transform.parent.GetComponentInChildren<LaserProfile>().color;
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
            else if (!isTooltip && rayHit.transform.parent.name.Equals("CoreTower"))
            {
                removeAllTooltips();
                var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
                newTooltip.transform.SetParent(tooltipUI.transform);
                newTooltip.GetComponent<IndividualTooltip>().text.text = "Core: Power with red, green and blue light to complete the level!";
                newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = rayHit.transform.gameObject;
                newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);


                int colorValue = rayHit.transform.parent.GetComponentInChildren<LaserProfile>().color;
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
            else if (!isTooltip && rayHit.transform.parent.name.Equals("MirrorTower"))
            {
                removeAllTooltips();
                var newTooltip = Instantiate(Resources.Load<GameObject>("UIElements/UIGenericTowerTooltip"), transform.position, Quaternion.identity);
                newTooltip.transform.SetParent(tooltipUI.transform);
                newTooltip.GetComponent<IndividualTooltip>().text.text = "Mirror: Reflects all colors of light at a 90 degree angle! Now comes in four flavors! (Press R while selected to rotate)";
                newTooltip.transform.GetComponent<IndividualTooltip>().tiedTower = rayHit.transform.parent.gameObject;
                newTooltip.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 200, 0);
                tooltipObject = newTooltip;
                isTooltip = true;

            }
            



        }
        else
        {
            if (isTooltip)
            {
                removeAllTooltips();
                isTooltip = false;
            }
            
        }







    }
    */
}
