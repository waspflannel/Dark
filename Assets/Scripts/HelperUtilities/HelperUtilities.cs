using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static Camera mainCamera;


    public static bool ValidateCheckEmptyString(Object thisObject , string fieldName , string stringToCheck)
    {
        if(string.IsNullOrEmpty(stringToCheck))
        {
            Debug.Log(fieldName + " is empty in " + thisObject.name.ToString());
            return true;
        }
        return false;
    }
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        Vector3 mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    public static AimDirection GetAimDirection(float angleDegrees)
    {
        AimDirection aimdirection;

        if(angleDegrees >= -90f && angleDegrees <= 90f)
        {
            aimdirection = AimDirection.Right;
        }
        else
        {
            aimdirection = AimDirection.Left;
        }
        return aimdirection;

    }

    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerable)
    {
        bool error = false;
        int count = 0;
        foreach (var item in enumerable)
        {
            if (item == null)
            {
                Debug.Log(fieldName + " has a null value at index " + count + " in " + thisObject.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }
        if(count==0)
        {
            Debug.Log(fieldName + " is empty in " + thisObject.name.ToString());
            error = true;
        }
        return error;
    }
}
