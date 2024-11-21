using System.IO;
using System.Security.Cryptography;

namespace FilesHash.Utils;

[RegisterTransient]
public class CalcHashQuery
{
    public string Get(string filePath, HashAlgorithm hashAlgorithm)
    {
        using var stream = File.OpenRead(filePath);
        var hash = hashAlgorithm.ComputeHash(stream);
        return Convert.ToHexStringLower(hash);
    }
}