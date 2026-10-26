using System.Collections.Generic;
using UnityEngine;

// Pitidos tipo Pong generados por código, para no depender de archivos de audio.
// Si un script tiene asignado un AudioClip propio, se usa ese en su lugar.
public static class ArcadeSound
{
    private const int SampleRate = 44100;
    private const float Volume = 0.25f;

    private static readonly Dictionary<(float, float), AudioClip> cache = new Dictionary<(float, float), AudioClip>();

    // Devuelve el AudioSource del objeto, o le añade uno si no tiene
    public static AudioSource GetSource(GameObject owner)
    {
        if (!owner.TryGetComponent(out AudioSource source))
        {
            source = owner.AddComponent<AudioSource>();
            source.playOnAwake = false;
        }
        return source;
    }

    public static void Play(AudioSource source, AudioClip customClip, float frequency, float duration)
    {
        if (source == null) return;
        source.PlayOneShot(customClip != null ? customClip : GetBeep(frequency, duration));
    }

    // Onda cuadrada con un desvanecimiento al final para que no chasquee
    private static AudioClip GetBeep(float frequency, float duration)
    {
        // Unity puede descargar los clips al cambiar de escena, así que se regeneran si hace falta
        if (cache.TryGetValue((frequency, duration), out AudioClip clip) && clip != null)
            return clip;

        int samples = Mathf.Max(1, (int)(SampleRate * duration));
        float[] data = new float[samples];
        for (int i = 0; i < samples; i++)
        {
            float t = (float)i / SampleRate;
            float square = Mathf.Sign(Mathf.Sin(2f * Mathf.PI * frequency * t));
            float fade = 1f - (float)i / samples;
            data[i] = square * Volume * fade;
        }

        clip = AudioClip.Create($"Beep_{frequency}", samples, 1, SampleRate, false);
        clip.SetData(data, 0);
        cache[(frequency, duration)] = clip;
        return clip;
    }
}
