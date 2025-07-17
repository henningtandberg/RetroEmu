using System;

namespace RetroEmu.Devices;

public static class Contributors
{
    public static byte[] Get() =>
    """
    Developers:     
    ----------------
    @henningtandberg
    @EilifTS        
    @murillio4      
    """u8
        .RemoveNewlines();
    
    private static byte[] RemoveNewlines(this ReadOnlySpan<byte> inputSpan)
    {
        Span<byte> resultSpan = stackalloc byte[inputSpan.Length];
        var resultIndex = 0;

        foreach (char c in inputSpan)
        {
            if (c is not '\n')
            {
                resultSpan[resultIndex++] = (byte)c;
            }
        }

        return resultSpan[..resultIndex].ToArray();
    }
}
