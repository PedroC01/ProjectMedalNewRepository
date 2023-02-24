using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private Transform target; //Ponto a meio dos dois jogadores(transform)

    [SerializeField]
    private Vector3 offsetPosition;// (Valor entre 0.5/-12, testa! Work!:D)
 
     [SerializeField]
    private Space offSetSpace = Space.Self; //Obriga a que a transform seja relativa sempre ao sistema de cordenadas local(do objecto) caso esteja filho de algo, isto aldrava isso...
    private Camera camera;
    private CameraPosition cameraPosition;

    private void Start()
    {
        cameraPosition = GetComponent<CameraPosition>();
    }
    private void Update()
    {
        UpdateCameraRotationAndPos();
    }

    public void UpdateCameraRotationAndPos()
    {
        if (target == null)
        {
         //   Debug.LogWarning("Nao existe /"Ponto central entre jogadores / " ", this);

            return;
        }


        //Posicao Camera:
        if(offSetSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
         else
        {
            transform.position = target.position + offsetPosition;
        }


        //Rotacao Camera:

        //Testa primeiro este se nao der testa o de baixo!!!!! 
        // this.transform.LookAt(cameraPosition.getCenterPoint());
        // Nao pode e tar a falhar a posicao do meio ponto ou esta nao vai dar mesmo!
        //   transform.rotation = target.rotation;
        transform.rotation = Quaternion.Euler(cameraPosition.testee);

    }
}
