using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;

public class PsicologoQR : MonoBehaviour
{
    public RawImage image;
    public RectTransform imageParent;
    public AspectRatioFitter imageFitter;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D texture = new Texture2D(500, 500);
        texture.filterMode = FilterMode.Bilinear;

        BarcodeWriterGeneric writer = new BarcodeWriterGeneric();
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options.Width = texture.width;
        writer.Options.Height = texture.height;
        writer.Options.Margin = 10;

        BitMatrix matrix = writer.Encode("Hola! Soy el senior psicologo y vengo a piscoanalizarte uwu");
        matrix.rotate180();
        ZXing.Common.BitArray row = new ZXing.Common.BitArray(matrix.RowSize);

        // get image data
        int width = texture.width;
        int height = texture.height;

        for (int y = 0; y < height; y++)
        {
            row = matrix.getRow(y, row);
            row.reverse(); // they are backwards wtf?
            int[] pixels = row.Array;

            int int_i = 0;
            int bit_i = 0;
            for (int x = 0; x < width; x++)
            {
                int bit_mask = 1 << bit_i++;
                int int_value = pixels[int_i];
                bool bit_value = (int_value & bit_mask) == bit_mask;

                if (bit_i > 31)
                {
                    bit_i = 0;
                    int_i++;
                }

                UnityEngine.Color color;
                if  (bit_value)
                {
                    color = UnityEngine.Color.black;
                } 
                else
                {
                    color = UnityEngine.Color.white;
                }

                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        image.texture = texture;
        //imageFitter.aspectRatio = 1.0f;
        //image.material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
