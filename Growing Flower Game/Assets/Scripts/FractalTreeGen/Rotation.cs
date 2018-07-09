using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public float angleX = 30;
    public float angleY = 90;
    public float angleZ = 0;

    public void Generated(RecursiveBundle bundle)
    {
        this.transform.rotation *= Quaternion.Euler(angleX * ((bundle.Index * 2) - 1), angleY * ((bundle.Index * 2) - 1), angleZ * ((bundle.Index * 2) - 1));
    }
}
