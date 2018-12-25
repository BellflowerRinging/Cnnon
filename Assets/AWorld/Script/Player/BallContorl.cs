using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class BallContorl : MonoBehaviour, IPointerUpHandler ,IDragHandler , IPointerDownHandler
{

    public DebugLabel _DebugLabel;

    public PlayerControl _PlayerControl;

    public GameObject Ball;

    private Vector2 VectorOffset;

    public float BallVectorClamp = 40;

    public bool X_Axis = true;
    public bool Y_Axis = true;


    int _TouchId = -1;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 vector;
        Vector3 vectorF;


        if (Input.touchCount > 1)
        {
            vector = Input.GetTouch(_TouchId).position;
        }
        else
        {
            vector = Input.mousePosition;
        }


        if (!X_Axis)
        {
            vector.x = transform.position.x;
        }

        if (!Y_Axis)
        {
            vector.y = transform.position.y;
        }

        vectorF = vector - transform.position;


        if (Mathf.Abs(vectorF.x) > BallVectorClamp || Mathf.Abs(vectorF.y) > BallVectorClamp)
        {
            Ball.transform.position = transform.position + (vector - transform.position).normalized * BallVectorClamp;
        }
        else
        {
            Ball.transform.position = transform.position + vectorF;
        }

        Vector3 v = Ball.transform.position - transform.position;
        VectorOffset = new Vector2(v.x,v.y) / BallVectorClamp;
    }

    public Vector2 GetVectorOffset()
    {
        return VectorOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_TouchId == -1 && Input.touchCount>0)
        {
            _TouchId = Input.touchCount - 1;
        }

        //_DebugLabel.AddItem(gameObject.name, _TouchId);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Ball.transform.position = transform.position;
        VectorOffset = Vector2.zero;

        if (Input.touchCount == 1)
        {
            _PlayerControl.ClearBallContorlTouchId();
        }


        //_DebugLabel.AddItem("BallContorl-Offset", VectorOffset);

        _DebugLabel.AddItem(gameObject.name, _TouchId);

    }

    void BallClampPos(Vector3 vector, float k)
    {
        vector.x = Mathf.Clamp(vector.x, transform.position.x - k, transform.position.x + k);
        vector.y = Mathf.Clamp(vector.y, transform.position.y - k, transform.position.y + k);
        Ball.transform.position = vector;
    }

    public void DisTouchId()
    {
        _TouchId = -1;
    }
}
