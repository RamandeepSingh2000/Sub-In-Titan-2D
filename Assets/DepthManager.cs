using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepthManager : MonoBehaviour
{
    [SerializeField] GameObject submersible;
    [SerializeField] GameObject depth_Initialreference;
    [SerializeField] GameObject depth_Finalreference;
    [SerializeField] TextMeshProUGUI depth_text;
    [SerializeField] GameObject depth_BG;
    private float submmersibleYaxis;
    private float InitialReferenceYaxis;
    private float FinalReferenceYaxis;
    private float distanceI;
    private float distanceF;
    private int depth;
    private float imageFill_value;
    private float maxDistance;
    private Transform submersible_transform;
    private Transform Initialreference_transform;
    private Transform Finalreference_transform;
    private Image depth_slider;
    public int Depth => depth;
    // Start is called before the first frame update
    void Start()
    {
        Finalreference_transform = depth_Finalreference.transform;
        depth_slider = depth_BG.GetComponent<Image>();
        FinalReferenceYaxis = Finalreference_transform.position.y;
        maxDistance = Mathf.Abs(submmersibleYaxis - FinalReferenceYaxis);
        Debug.Log(maxDistance);
    }

    private void FixedUpdate()
    {
        submersible_transform = submersible.transform;
        Initialreference_transform = depth_Initialreference.transform;
        submmersibleYaxis = submersible_transform.position.y;
        InitialReferenceYaxis = Initialreference_transform.position.y;
        distanceI = Mathf.Abs(submmersibleYaxis - InitialReferenceYaxis);
        distanceF = Mathf.Abs(submmersibleYaxis - FinalReferenceYaxis);
        depth = Mathf.RoundToInt(distanceI);
        depth_text.text = depth.ToString()+"m";
        imageFill_value = Mathf.Clamp(distanceF, 0.0f, maxDistance); 
        depth_slider.fillAmount = imageFill_value/maxDistance;
    }
}
