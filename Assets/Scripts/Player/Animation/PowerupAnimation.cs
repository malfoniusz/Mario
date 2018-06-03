using System.Collections;
using UnityEngine;

public class PowerupAnimation : MonoBehaviour
{
    public Animator marioAnimator;
    public GameObject smallMarioModel;
    public GameObject bigMarioModel;
    public GameObject fireMarioModel;
    public BoxCollider2D playerBoxCollider;

    private GameController gameController;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private bool animationInProgress = false;
    private Transform[] playerGroundChecks;
    private Transform[] playerTopChecks;

    private void Awake()
    {
        gameController = TagNames.GetGameController().GetComponent<GameController>();
        player = TagNames.GetPlayer();
        spriteRenderer = player.GetComponent<SpriteRenderer>();

        playerGroundChecks = PrefabNames.GetChecks(player, PrefabNames.marioGroundChecks);
        playerTopChecks = PrefabNames.GetChecks(player, PrefabNames.marioTopChecks);
    }

    public void StartAnimation(bool powerup, MarioLevelEnum marioNewLevel)
    {
        StartCoroutine(StartAnimationCoroutine(powerup, marioNewLevel));
    }

    private IEnumerator StartAnimationCoroutine(bool powerup, MarioLevelEnum marioNewLevel)
    {
        gameController.StopGame(false);
        animationInProgress = true;
        marioAnimator.enabled = false;

        if (powerup)
        {
            if      (marioNewLevel == MarioLevelEnum.big) StartCoroutine(AnimationCoroutine(smallMarioModel, bigMarioModel, true));
            else if (marioNewLevel == MarioLevelEnum.fire) StartCoroutine(AnimationCoroutine(bigMarioModel, fireMarioModel, true));
        }
        else
        {
            if      (marioNewLevel == MarioLevelEnum.big) StartCoroutine(AnimationCoroutine(fireMarioModel, bigMarioModel, false));
            else if (marioNewLevel == MarioLevelEnum.small) StartCoroutine(AnimationCoroutine(bigMarioModel, smallMarioModel, false));
        }

        while (animationInProgress)
        {
            yield return new WaitForSeconds(0.01f);
        }

        UpdateModel(marioNewLevel);
        marioAnimator.enabled = true;
        gameController.ResumeGame(false);

        yield return null;
    }

    private IEnumerator AnimationCoroutine(GameObject oldMario, GameObject newMario, bool powerupAnimation)
    {
        BoxCollider2D oldCollider = oldMario.GetComponent<BoxCollider2D>();
        Sprite oldSprite = oldMario.GetComponent<SpriteRenderer>().sprite;

        BoxCollider2D newCollider = newMario.GetComponent<BoxCollider2D>();
        Sprite newSprite = newMario.GetComponent<SpriteRenderer>().sprite;

        float colliderYDifference = Mathf.Abs((oldCollider.size.y - newCollider.size.y) / 2);

        yield return UpDownAnimation(colliderYDifference, newSprite, 0.16f, powerupAnimation);
        yield return UpDownAnimation(colliderYDifference, oldSprite, 0.16f, !powerupAnimation);

        yield return UpDownAnimation(colliderYDifference, newSprite, 0.13f, powerupAnimation);
        yield return UpDownAnimation(colliderYDifference, oldSprite, 0.13f, !powerupAnimation);

        yield return UpDownAnimation(colliderYDifference, newSprite, 0.10f, powerupAnimation);
        yield return UpDownAnimation(colliderYDifference, oldSprite, 0.10f, !powerupAnimation);

        yield return UpDownAnimation(colliderYDifference, newSprite, 0.06f, powerupAnimation);
        yield return UpDownAnimation(colliderYDifference, oldSprite, 0.06f, !powerupAnimation);

        yield return UpDownAnimation(colliderYDifference, newSprite, 0.0f, powerupAnimation);

        animationInProgress = false;
        yield return null;
    }

    private WaitForSeconds UpDownAnimation(float colliderYDifference, Sprite sprite, float animationDuration, bool upAnimation)
    {
        Vector3 upDown = (upAnimation == true ? Vector3.up : Vector3.down);

        player.transform.position += upDown * colliderYDifference;
        spriteRenderer.sprite = sprite;
        return new WaitForSeconds(animationDuration);
    }

    public void UpdateModel(MarioLevelEnum marioLevel)
    {
        if      (marioLevel == MarioLevelEnum.small)    UpdateModelBehaviour(smallMarioModel);
        else if (marioLevel == MarioLevelEnum.big)      UpdateModelBehaviour(bigMarioModel);
        else if (marioLevel == MarioLevelEnum.fire)     UpdateModelBehaviour(fireMarioModel);

        SetAnimatorState(marioLevel);
    }

    private void UpdateModelBehaviour(GameObject newModel)
    {
        BoxCollider2D newCollider = newModel.GetComponent<BoxCollider2D>();
        Transform[] newGroundChecks = PrefabNames.GetChecks(newModel, PrefabNames.marioGroundChecks);
        Transform[] newTopChecks = PrefabNames.GetChecks(newModel, PrefabNames.marioTopChecks);

        playerBoxCollider.size = newCollider.size;

        for (int i = 0; i < playerGroundChecks.Length; i++)
        {
            playerGroundChecks[i].localPosition = newGroundChecks[i].localPosition;
        }

        for (int i = 0; i < playerTopChecks.Length; i++)
        {
            playerTopChecks[i].localPosition = newTopChecks[i].localPosition;
        }
    }

    private void SetAnimatorState(MarioLevelEnum marioLevel)
    {
        if      (marioLevel == MarioLevelEnum.small)    marioAnimator.SetTrigger(AnimatorPlayerNames.smallMarioIdle);
        else if (marioLevel == MarioLevelEnum.big)      marioAnimator.SetTrigger(AnimatorPlayerNames.bigMarioIdle);
        else if (marioLevel == MarioLevelEnum.fire)     marioAnimator.SetTrigger(AnimatorPlayerNames.fireMarioIdle);
    }

}
