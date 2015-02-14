using UnityEngine;
using System.Collections;

public class Ocilador : MonoBehaviour {

    public float speed;
    public float width;
    public float height;
    public float deltaX;
    public float DeltaY;
    public Color trayectoryColor;

    private float timeCounter;
    private Vector3 initialPoint;
    private float x;
    private float y;
    private float z;
    
    void Awake(){
        timeCounter = 0f;
        if(transform.parent != null){
            initialPoint = transform.localPosition;
        }else{
            initialPoint = transform.position;
        }
        
    }

    void FixedUpdate(){

        timeCounter += Time.deltaTime * speed;

        x = initialPoint.x + deltaX + (Mathf.Sin(timeCounter) * width);
        y = initialPoint.y + DeltaY + (Mathf.Cos(timeCounter) * height);
        z = initialPoint.z;

        drawTrayectory(x, y, z);

        transform.position = new Vector3(x, y, z);

		if(transform.parent != null){
            transform.localPosition = new Vector3(x, y, z);
        }else{
            transform.position = new Vector3(x, y, z);
        }
    }

    void drawTrayectory(float x, float y, float z){
        Vector3 nextPoint = new Vector3(x, y, z);
        Vector3 actualPoint = transform.position;
        Debug.DrawLine(actualPoint, nextPoint, trayectoryColor, 3f);
    }
}
