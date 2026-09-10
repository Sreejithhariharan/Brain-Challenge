using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class Tile : MonoBehaviour
{
    public int number;
    public bool isX;

    public Image image;

    [Header("Effects")]
    public GameObject glow;

    private Sprite frontSprite;
    public Sprite backSprite;

    private bool flipped = false;
    public bool canClick = false;

    private bool completed = false;

    // Store the parent Sequence
    private Sequence currentSequence;

    // Store glow tween separately
    private Tween glowTween;


    // =========================================
    // SETUP
    // =========================================

    public void Setup(Sprite sprite, int value)
    {
        StopCurrentAnimation();

        frontSprite = sprite;

        number = value;
        isX = value == -1;

        flipped = false;
        completed = false;
        canClick = false;

        image.sprite = backSprite;

        if (glow != null)
        {
            StopGlowAnimation();

            glow.SetActive(false);
            glow.transform.localScale = Vector3.one;
        }

        transform.localRotation = Quaternion.identity;
    }


    // =========================================
    // CLICK
    // =========================================

    public void ClickTile()
    {
        if (!canClick)
            return;

        if (flipped || completed)
            return;

        if (SoundManager.instance != null)
            SoundManager.instance.PlayClick();

        flipped = true;

        // Flip and reveal the tile
        Flip();

        // Let GameManager decide if this is correct/wrong
        GameManager.instance.CheckTile(this);
    }


    // =========================================
    // SHOW TILE
    // =========================================

    public void ShowTile()
    {
        StopCurrentAnimation();

        flipped = true;

        currentSequence = Sequence.Create()

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 90, 0),
                    0.25f,
                    Ease.InOutSine
                )
            )

            .ChainCallback(ChangeImage)

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 0, 0),
                    0.25f,
                    Ease.InOutSine
                )
            );
    }


    // =========================================
    // HIDE TILE
    // =========================================

    public void HideTile()
    {
        StopCurrentAnimation();

        flipped = false;

        currentSequence = Sequence.Create()

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 90, 0),
                    0.25f,
                    Ease.InOutSine
                )
            )

            .ChainCallback(ChangeBackImage)

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 0, 0),
                    0.25f,
                    Ease.InOutSine
                )
            );
    }


    // =========================================
    // CHANGE IMAGES
    // =========================================

    void ChangeImage()
    {
        if (this == null || image == null)
            return;

        image.sprite = frontSprite;
    }


    void ChangeBackImage()
    {
        if (this == null || image == null)
            return;

        image.sprite = backSprite;
    }


    // =========================================
    // PLAYER FLIP
    // =========================================

    void Flip()
    {
        StopCurrentAnimation();

        currentSequence = Sequence.Create()

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 90, 0),
                    0.25f,
                    Ease.InOutSine
                )
            )

            .ChainCallback(ChangeImage)

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 0, 0),
                    0.25f,
                    Ease.InOutSine
                )
            );
    }

    // =========================================
    // WRONG TILE EFFECT
    // =========================================

    public void WrongEffect()
    {
        StopCurrentAnimation();

        flipped = true;

        currentSequence = Sequence.Create()

            // Flip to reveal the number
            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 90, 0),
                    0.2f,
                    Ease.InOutSine
                )
            )

            // Change from back to front
            .ChainCallback(ChangeImage)

            // Finish the flip
            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 0, 0),
                    0.2f,
                    Ease.InOutSine
                )
            )

            // Shake after revealing the number
            .Chain(
                Tween.ShakeLocalPosition(
                    transform,
                    new Vector3(20, 0, 0),
                    0.35f
                )
            );
    }


    // =========================================
    // CORRECT NUMBER
    // =========================================

    public void CorrectEffect()
    {
        completed = true;

        if (glow != null)
        {
            StopGlowAnimation();

            glow.SetActive(true);
            glow.transform.localScale = Vector3.one;

            glowTween = Tween.Scale(
                glow.transform,
                Vector3.one * 1.2f,
                0.2f,
                Ease.OutBack
            );
        }
    }


    // =========================================
    // WRONG NUMBER / X
    // =========================================

    public void WrongShake()
    {
        StopCurrentAnimation();

        Tween.ShakeLocalPosition(
            transform,
            new Vector3(20, 0, 0),
            0.35f
        );
    }


    // =========================================
    // HIDE WRONG TILE
    // =========================================

    public void HideAgain()
    {
        StopCurrentAnimation();

        currentSequence = Sequence.Create()

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 90, 0),
                    0.25f,
                    Ease.InOutSine
                )
            )

            .ChainCallback(ChangeBackImage)

            .Chain(
                Tween.LocalRotation(
                    transform,
                    Quaternion.Euler(0, 0, 0),
                    0.25f,
                    Ease.InOutSine
                )
            )

            .OnComplete(() =>
            {
                if (this != null)
                {
                    flipped = false;
                }
            });
    }


    // =========================================
    // STOP CURRENT SEQUENCE
    // =========================================

    void StopCurrentAnimation()
    {
        // IMPORTANT:
        // Stop the parent Sequence instead of
        // trying to manipulate its nested tweens.
        if (currentSequence.isAlive)
        {
            currentSequence.Stop();
        }
    }


    // =========================================
    // STOP GLOW ANIMATION
    // =========================================

    void StopGlowAnimation()
    {
        if (glowTween.isAlive)
        {
            glowTween.Stop();
        }
    }


    // =========================================
    // CLEANUP
    // =========================================

    void OnDestroy()
    {
        StopCurrentAnimation();
        StopGlowAnimation();
    }
}