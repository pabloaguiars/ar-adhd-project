using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using ZXing;
using System.Collections;
using UnityEngine.SceneManagement;


// http://answers.unity.com/answers/1155328/view.html
// https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
public class PsicologoScan : MonoBehaviour
{
    public RawImage image;
    public RectTransform imageParent;
    public AspectRatioFitter imageFitter;

    public Text testText;

    private WebCamDevice frontCameraDevice, backCameraDevice;

    WebCamTexture frontCameraTexture;
    WebCamTexture backCameraTexture;
    WebCamTexture activeCameraTexture;

    Rect defaultRect = new Rect(0f, 0f, 1f, 1f);
    Rect fixedRect = new Rect(0f, 1f, 1f, -1f);

    // Image rotation
    Vector3 rotationVector = new Vector3(0f, 0f, 0f);
 
    // Image Parent's scale
    Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    Vector3 fixedScale = new Vector3(-1f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeCamera());
    }

    IEnumerator InitializeCamera()
    {
#if PLATFORM_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
            yield return new WaitUntil(() => UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera));
            // reload scene to save changes
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
        if (activeCameraTexture != null && activeCameraTexture.isPlaying)
        {
            activeCameraTexture.Stop();
        }

        // Get the device's cameras and create WebCamTextures with them
        frontCameraDevice = WebCamTexture.devices.Last();
        backCameraDevice = WebCamTexture.devices.First();

        if (WebCamTexture.devices.Length > 0)
        {
            activeCameraTexture = new WebCamTexture(backCameraDevice.name);
        }
        else
        {
            activeCameraTexture = new WebCamTexture();
        }
        activeCameraTexture.filterMode = FilterMode.Trilinear;

        image.SetNativeSize();
        //image.texture = activeCameraTexture;
        image.material.mainTexture = activeCameraTexture;
        activeCameraTexture.Play();

        yield break;
    }

    // Make adjustments to image every frame to be safe, since Unity isn't 
    // guaranteed to report correct data as soon as device camera is started
    void Update()
    {
        if (activeCameraTexture != null) 
        {
            // Skip making adjustment for incorrect camera data
            if (activeCameraTexture.width < 100)
            {
                Debug.Log("Still waiting another frame for correct info...");
                return;
            }

            // Rotate image to show correct orientation 
            rotationVector.z = -activeCameraTexture.videoRotationAngle;
            image.rectTransform.localEulerAngles = rotationVector;

            // Set AspectRatioFitter's ratio
            float videoRatio =
                (float)activeCameraTexture.width / (float)activeCameraTexture.height;
            imageFitter.aspectRatio = videoRatio;

            // Unflip if vertically flipped
            image.uvRect =
                activeCameraTexture.videoVerticallyMirrored ? fixedRect : defaultRect;

            // Mirror front-facing camera's image horizontally to look more natural
            imageParent.localScale =
                backCameraDevice.isFrontFacing ? fixedScale : defaultScale;

            AttemptDecode();
        }
    }

    void AttemptDecode()
    {
        // get image data
        int width = activeCameraTexture.width;
        int height = activeCameraTexture.height;
        byte[] rawRGB = new byte[width * height * 3];
        int i = 0;
        for (int y = 0; y < height; y++) 
        { 
            for (int x = 0; x < width; x++)
            {
                UnityEngine.Color color = activeCameraTexture.GetPixel(x, y);
                rawRGB[i++] = (byte) (color.r * 255.0);
                rawRGB[i++] = (byte) (color.g * 255.0);
                rawRGB[i++] = (byte) (color.b * 255.0);
            }
        }

        // create a barcode reader instance
        ///QRCodeReader reader = new QRCodeReader();
        BarcodeReaderGeneric reader = new BarcodeReaderGeneric();
        //var barcodeBitmap = (Bitmap)Image.LoadFrom("C:\\sample-barcode-image.png");
        // detect and decode the barcode inside the bitmap
        var result = reader.Decode(rawRGB, width, height, RGBLuminanceSource.BitmapFormat.RGB24);
        // do something with the result
        if (result != null)
        {
            Decoded(result);
        }
    }

    void Decoded(Result result)
    {
        Debug.Log(result.BarcodeFormat.ToString());
        Debug.Log(result.Text);
        testText.text = result.Text;
    }
}
