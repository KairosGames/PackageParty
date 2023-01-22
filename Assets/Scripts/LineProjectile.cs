using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineProjectile : MonoBehaviour
{
    [SerializeField] Transform posCam;

    [SerializeField] LineRenderer lineRenderer;
    internal Vector3 aimDirection;

    [SerializeField] int nbPoints = 50;
    [SerializeField] float timeBetweenPoints = 0.1f;

    [SerializeField] LayerMask collidableLayer;

    internal bool onAim;
    
    void Start()
    {
        onAim = false;
    }

    void Update()
    {
        if (onAim)
        {
            lineRenderer.positionCount = nbPoints;
            List<Vector3> points = new List<Vector3>();

            Vector3 startPos = transform.position;

            var frc = aimDirection.magnitude/2;
            var force = (frc >= 300) ? 300 : ((frc <= 40) ? 40 : frc);
            var nrmlDir = new Vector3(aimDirection.normalized.y, 0, -aimDirection.normalized.x);
            var angle = Vector3.Angle(nrmlDir, Vector3.right);
            var cross = Vector3.Cross(nrmlDir, Vector3.right);
            if (cross.y < 0) { angle *= -1; }
            var camAnlg = Mathf.Atan(posCam.localPosition.z / posCam.localPosition.x);
            var dtAngle = angle * Mathf.Deg2Rad + camAnlg;
            var realDir = new Vector3(Mathf.Cos(dtAngle), (force / 1000), Mathf.Sin(dtAngle));
            Vector3 vecForce = realDir * (force / 2);

            for (float t = 0; t < nbPoints; t += timeBetweenPoints)
            {
                Vector3 newPoint = startPos + t * vecForce;
                newPoint.y = startPos.y + vecForce.y * t + Physics.gravity.y / 2.0f * t * t;
                points.Add(newPoint);

                if (Physics.OverlapSphere(newPoint, 0.5f, collidableLayer).Length > 0)
                {
                    lineRenderer.positionCount = points.Count;
                    break;
                }
            }
            lineRenderer.SetPositions(points.ToArray());
        }
    }

}
