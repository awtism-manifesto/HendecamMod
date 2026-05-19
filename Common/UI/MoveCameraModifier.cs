using Terraria.Graphics.CameraModifiers;

namespace HendecamMod.Common.UI;

public class MoveCameraModifier : ICameraModifier
{
    public string UniqueIdentity { get; set; }
    public bool Finished { get; set; }

    Vector2 targetPosition;
    Func<bool> endCondition;

    public MoveCameraModifier(Vector2 targetPosition, Func<bool> endCondition)
    {
        this.targetPosition = targetPosition - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
        this.endCondition = endCondition;
    }

    float progress = 0f;
    float endTimer = 0f;
    float timer = 0f;
    public void Update(ref CameraInfo cameraPosition)
    {
        if (targetPosition.Distance(cameraPosition.CameraPosition) > 2500)
        {
            Finished = true;
            return;
        }
        if (endCondition.Invoke() || timer > 3600) // if not talking or veryyyy long time has passed
        {
            if (Main.gamePaused || Main.gameInactive)
            {
                return;
            }
            endTimer++;
            progress = Math.Clamp(endTimer / 60f, 0, 1);
            cameraPosition.CameraPosition = Vector2.SmoothStep(targetPosition, cameraPosition.OriginalCameraPosition, progress);
            if (progress >= 1f)
            {
                Finished = true;
            }
            return;
        }
        if (Main.gamePaused || Main.gameInactive)
        {
            return;
        }
        progress = Math.Clamp(timer / 60f, 0, 1);
        cameraPosition.CameraPosition = Vector2.SmoothStep(cameraPosition.OriginalCameraPosition, targetPosition, progress);
        timer++;
        progress = Math.Clamp(timer / 60f, 0, 1);
    }
}
