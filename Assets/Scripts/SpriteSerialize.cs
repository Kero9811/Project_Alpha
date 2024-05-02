using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SpriteSerialize
{
    public static void SaveSpriteAsImage(Sprite sprite, string filePath)
    {
        // Get the texture from the sprite
        Texture2D texture = sprite.texture;

        // Encode the texture into PNG format
        byte[] bytes = texture.EncodeToPNG();

        // Write the bytes to a file
        File.WriteAllBytes(filePath, bytes);
    }
}

public static class SpriteDeserializer
{
    public static Sprite LoadSpriteFromImage(string filePath)
    {
        // Read the bytes from the file
        byte[] bytes = File.ReadAllBytes(filePath);

        // Create a new texture
        Texture2D texture = new Texture2D(2, 2);

        // Load the texture from the bytes
        texture.LoadImage(bytes);

        // Create a new sprite using the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        return sprite;
    }
}