using System.Collections;
using UnityEngine;

public class ProceduralWeaponAnimator : MonoBehaviour
{
    [SerializeField] bool isAiming;
    [SerializeField] float currentRecoiled;

    float Recoiled { get {return currentRecoiled * recoilAmount;} }
    enum EWeaponType
    {
        Pistol,
        Rifle,
        Shotgun,
        Sniper
    }

    [SerializeField] EWeaponType weaponAnimationType;

    [Header("Math")]
    [SerializeField] float weight = 1f;
    [SerializeField] float recoilAmount = 10f;

    [Header("Transforms")]
    [SerializeField] Transform sightNear;
    [SerializeField] Transform sightFar;
    [SerializeField] Transform gripLeft, gripRight;
    [SerializeField] Transform magazine, slide, bolt, triggerWell;

    [Header("Offsets")]
    [SerializeField] Vector3 idlePositionOffset;
    [SerializeField] Vector3 idleRotationOffset;
    [SerializeField] Vector3 adsPositionOffset;
    [SerializeField] Vector3 adsRotationOffset;

    [Header("Limits")]
    [SerializeField] float recoilPositionZLimit;
    [SerializeField] float recoilRotationXLimit;

    [Header("Curves")]
    [SerializeField] AnimationCurve recoilPositionCurve;
    [SerializeField] AnimationCurve recoilRotationCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        currentRecoiled = Mathf.Clamp(currentRecoiled - Time.deltaTime, 0, 1);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Backspace))
        {
            currentRecoiled += 0.5f * Time.deltaTime / weight;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateRecoil();
    }

    void UpdateRecoil()
    {
        float newXRot = Mathf.Lerp(0, recoilRotationXLimit, recoilRotationCurve.Evaluate(Recoiled));
        float newZPos = Mathf.Lerp(0, recoilPositionZLimit, recoilPositionCurve.Evaluate(Recoiled));

        transform.localPosition = new Vector3(0, 0, newZPos);
        transform.localEulerAngles = new Vector3(newXRot, 0, 0);
    }
    
}
