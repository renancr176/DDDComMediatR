﻿using System.Security.Cryptography;
using System.Text;

namespace ApiMvno.Infra.CrossCutting.Jsc.Extensions;

public static class StringExtensions
{
    public static byte[] GetHash(this string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(this string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}