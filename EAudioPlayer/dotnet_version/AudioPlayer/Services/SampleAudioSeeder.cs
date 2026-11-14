namespace AudioPlayer.Services;

/// <summary>
/// Creates a few short synthesized WAV tracks the first time the app runs so the
/// player has something to play. Real audio dropped into wwwroot/audio/{Album}/ is used as-is.
/// </summary>
public static class SampleAudioSeeder
{
    public const string AudioFolder = "audio";
    private const int SampleRate = 22050;

    private static readonly Dictionary<string, (string Name, double[] Notes)[]> Albums = new()
    {
        ["Sample Album - Sine Scales"] =
        [
            ("01 - C Major Scale", [261.63, 293.66, 329.63, 349.23, 392.00, 440.00, 493.88, 523.25]),
            ("02 - A Minor Scale", [220.00, 246.94, 261.63, 293.66, 329.63, 349.23, 392.00, 440.00]),
            ("03 - Arpeggio", [261.63, 329.63, 392.00, 523.25, 392.00, 329.63, 261.63, 196.00]),
        ],
        ["Sample Album - Slow Tones"] =
        [
            ("01 - Low Drone", [110.00, 130.81, 146.83, 130.81, 110.00, 98.00]),
            ("02 - High Chimes", [783.99, 880.00, 987.77, 1046.50, 987.77, 880.00]),
        ],
    };

    public static void Seed(string webRoot)
    {
        var root = Path.Combine(webRoot, AudioFolder);
        if (Directory.Exists(root) && Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories).Any())
        {
            return;
        }

        foreach (var (album, tracks) in Albums)
        {
            var dir = Path.Combine(root, album);
            Directory.CreateDirectory(dir);
            foreach (var (name, notes) in tracks)
            {
                WriteWav(Path.Combine(dir, name + ".wav"), notes);
            }
        }
    }

    private static void WriteWav(string path, double[] notes, double noteSeconds = 2.5)
    {
        var samplesPerNote = (int)(SampleRate * noteSeconds);
        var total = samplesPerNote * notes.Length;

        using var fs = File.Create(path);
        using var w = new BinaryWriter(fs);
        w.Write("RIFF"u8);
        w.Write(36 + total * 2);
        w.Write("WAVEfmt "u8);
        w.Write(16);
        w.Write((short)1);          // PCM
        w.Write((short)1);          // mono
        w.Write(SampleRate);
        w.Write(SampleRate * 2);    // byte rate
        w.Write((short)2);          // block align
        w.Write((short)16);         // bits per sample
        w.Write("data"u8);
        w.Write(total * 2);

        foreach (var freq in notes)
        {
            for (var i = 0; i < samplesPerNote; i++)
            {
                var t = (double)i / SampleRate;
                // Short fade in/out per note avoids clicks.
                var envelope = Math.Min(1.0, Math.Min(i, samplesPerNote - i) / (SampleRate * 0.05));
                w.Write((short)(Math.Sin(2 * Math.PI * freq * t) * envelope * 0.4 * short.MaxValue));
            }
        }
    }
}
