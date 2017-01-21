//Author: Rok Kos <kosrok97@gmail.com>
//File: EnemyCurveClass.cs
//File path: /D/Documents/Unity/GGJ2017/EnemyCurveClass.cs
//Date: 20.01.2017
//Description: Little bit advanced enemy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurveClass : EnemyBaseClass {
    protected float timeStart;
    protected Vector3 userPosition;
    protected static float sizeOfBoxX;
    protected static float sizeOfBoxY;

    // Constuructor and calling base constructor
    public EnemyCurveClass (byte _tip, Vector3 _startPos, Vector3 _endPos, float _timeStart, Vector3 _userPosition, float _sizeOfBoxX, float _sizeOfBoxY) : 
           base(_tip, _startPos, _endPos) {

        this.timeStart = 0;//_timeStart;
        this.userPosition = _userPosition;
        sizeOfBoxX = _sizeOfBoxX;
        sizeOfBoxY = _sizeOfBoxY;
    }

    public override void nextMove (Transform currentPos, float speed) {

        //float currentDistanceToEnd = Vector3.Distance(currentPos, endPos);
        //float timeAlive = Mathf.Clamp01(timeStart);//(Time.time - this.timeStart));
        //timeStart += Time.deltaTime * 0.1f;  // TODO: Tweak this speed
        // Bezeir formula quadric
        //float curveX = (((1 - timeAlive) * (1 - timeAlive)) * startPos.x) + (2 * timeAlive * (1 - timeAlive) * userPosition.x) + ((timeAlive * timeAlive) * endPos.x);
        //float curveY = (((1 - timeAlive) * (1 - timeAlive)) * startPos.y) + (2 * timeAlive * (1 - timeAlive) * userPosition.y) + ((timeAlive * timeAlive) * endPos.y);

        // Bezeir cubic formula

        // float curveX = (((1 - timeAlive) * (1 - timeAlive) * (1 - timeAlive)) * startPos.x) + 3 * (((1 - timeAlive) * (1 - timeAlive) * timeAlive) * (startPos.x - startPos.x / 2)) +
        //     3 * (((1 - timeAlive) * timeAlive * timeAlive) * endPos.x - endPos.x / 2) + ((timeAlive * timeAlive * timeAlive) * endPos.x );
        // float curveY = (((1 - timeAlive) * (1 - timeAlive) * (1 - timeAlive)) * startPos.y) + 3 * (((1 - timeAlive) * (1 - timeAlive) * timeAlive) * (startPos.y - startPos.y / 2)) +
        //     3 * (((1 - timeAlive) * timeAlive * timeAlive) * endPos.y - endPos.y / 2) + ((timeAlive * timeAlive * timeAlive) * endPos.y);

        //currentPos.x = curveX;
        //currentPos.y = curveY;
        /*Vector3 dirVector = new Vector3();
        if (startPos.x == sizeOfBoxX) {
            dirVector = -currentPos.right;
        }
        else if (startPos.x == -sizeOfBoxX) {
            dirVector = currentPos.right;
        }else if (startPos.y == sizeOfBoxY) {
            dirVector = -currentPos.up;
        } else if (startPos.y == -sizeOfBoxY) {
            dirVector = currentPos.up;
        }*/

        Vector3 newDir = userPosition - currentPos.position;
        float angle = Vector3.Angle(newDir, currentPos.up);  //calculate angle
        if (angle < 90)
        {
            if (Vector3.Cross(newDir, currentPos.up).z < 0)
            {
                if (angle > 0.1f)
                    currentPos.Rotate(Vector3.forward, 0.1f);
                else
                    currentPos.Rotate(Vector3.forward, angle);
            }
            else
            {
                if (angle > 0.1f)
                    currentPos.Rotate(Vector3.forward, -0.1f);
                else
                    currentPos.Rotate(Vector3.forward, -angle);
            }
        }

        currentPos.position += currentPos.up * speed;
    }
}
