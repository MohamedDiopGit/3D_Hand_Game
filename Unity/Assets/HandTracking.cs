using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] handPoints0;
    public GameObject[] handPoints1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
        data = data.Remove(0, 1);       // Remove [ ]
        data = data.Remove(data.Length-1, 1);
        print(data);

        string[] points = data.Split(',');
        

        for (int i = 0; i< 21; i++){


            float x = 14-float.Parse(points[i*3]) / 50; // x values
            float y = float.Parse(points[i*3 + 1]) / 130;  // y
            //float z = float.Parse(points[i*3 + 2]) / 100;  // z

            float dx = float.Parse(points[1*3]) - float.Parse(points[2*3]);
            float dy = float.Parse(points[1*3 + 1]) - float.Parse(points[2*3 + 1]);
            float euclidControl = (10-Mathf.Sqrt( dx*dx + dy*dy )/5 );

            float z = euclidControl + float.Parse(points[i*3 + 2]) / 50;  // z = init mov(dist euclid) + inner mov

            
            handPoints0[i].transform.localPosition = new Vector3(x,y,z);

        }
        
        for (int i = 21; i< 42; i++){


            float x = 14-float.Parse(points[i*3]) / 50; // x values
            float y = float.Parse(points[i*3 + 1]) / 130;  // y
            //float z = float.Parse(points[i*3 + 2]) / 100;  // z

            float dx = float.Parse(points[(1+21)*3]) - float.Parse(points[(2+21)*3]);
            float dy = float.Parse(points[(1+21)*3 + 1]) - float.Parse(points[(2+21)*3 + 1]);
            float euclidControl = (10-Mathf.Sqrt( dx*dx + dy*dy )/5 );

            float z = euclidControl + float.Parse(points[i*3 + 2]) / 50;  // z = init mov(dist euclid) + inner mov

            
            handPoints1[i-21].transform.localPosition = new Vector3(x,y,z);

        }
    }
}
