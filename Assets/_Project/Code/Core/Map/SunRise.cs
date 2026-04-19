using Unity.VisualScripting;
using UnityEngine;

public enum SunRiseState
{
  Day, Night
}
public class SunRise : MonoBehaviour
{
    [Header("Einstellungen")]
    public SunRiseState State;
    public float Speed = 1f;
    public int day_index = 0;

    [Header("Ellipse Einstellungen")]
    public SpriteRenderer sky;
    public Transform orbit_Center;
    public float ellipseWidth = 10f;
    public float ellipseHeight = 5f;

    [Header("Anzeige (Nur lesen)")]
    public float dir_z = 0f;
    float currentAngleRad;
    SunRiseState lateState;
    Material sky_mat;
    private void Start()
    {
        sky_mat = sky.material;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        currentAngleRad += Speed * Time.fixedDeltaTime;
        if(currentAngleRad > Mathf.PI * 2) currentAngleRad -= Mathf.PI * 2;
        float raw = currentAngleRad * Mathf.Rad2Deg;
        float raw_rot = raw % 360;
        float raw_rot_360 = raw_rot < 0 ? raw_rot + 360 : raw_rot;
        dir_z = raw_rot_360;
        SunRiseState curr_State = dir_z < 180 ? SunRiseState.Day : SunRiseState.Night;

        if(lateState == SunRiseState.Night && curr_State == SunRiseState.Day) day_index++;

        lateState = curr_State;
        State = curr_State;

        MoveSunOnEllipse();
        SkyMatUpdate();
    }
    void SkyMatUpdate()
    {
        if(sky_mat == null) return;

        float shader_value = 0;
        float rawSin = Mathf.Sin(currentAngleRad);

        shader_value = (rawSin + 1) / 2;

        sky_mat.SetFloat("_LerpIndex", shader_value);

    }
    void MoveSunOnEllipse()
    {
      if(orbit_Center == null) return;

      Vector3 center = orbit_Center.position;

        float x = center.x + Mathf.Cos(currentAngleRad) * (ellipseWidth/2);
        float y = center.y + Mathf.Sin(currentAngleRad) * (ellipseHeight/2);

        transform.position = new Vector3(x, y, transform.position.z);
    }
    private void OnDrawGizmosSelected ()
    {
        if(orbit_Center == null) return;
        Gizmos.color = Color.yellow;
        Vector3 lastPoint= Vector3.zero;

        bool first = true;

        for (int i = 0;  i  <360;  i += 10)
        {
            float rad = i * Mathf.Deg2Rad;
            float x = orbit_Center.position.x + Mathf.Cos(rad) * (ellipseWidth/2);
            float y = orbit_Center.position.y + Mathf.Sin(rad) * (ellipseHeight/2);
            Vector3 point = new Vector3(x, y, transform.position.z);
            
            if(!first)
            {
                Gizmos.DrawLine(lastPoint, point);
            }
            lastPoint = point;
            first = false;

        }
    }
}
