using UnityEngine;

public class MarioAnimator : MonoBehaviour
{
    public Animator anim;

    public void SetIsGrabbing(bool isGrabbing)
    {
        anim.SetBool(AnimatorPlayerNames.isGrabbing, isGrabbing);
    }

    // Ty który to czytasz, w imię refaktoryzacji przenieś tutaj wszystkie instukcje, które zmieniają stan animatora :)

}
