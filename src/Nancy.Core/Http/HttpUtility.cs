using System;
using System.Collections;

namespace Nancy.Core.Http
{
    public static class HttpUtility
    {
        private const string Token =
            "!#$%&'*+-.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ^_`abcdefghijklmnopqrstuvwxyz|~";

        private static readonly Lazy<TokenValues> Values =
            new Lazy<TokenValues>(() => GetTokenValues(Token.ToCharArray()));

        public static bool IsValidToken(string value)
        {
            var values = Values.Value;

            foreach (var @char in value)
            {
                if (!values.IsValid(@char))
                {
                    return false;
                }
            }

            return true;
        }

        private static TokenValues GetTokenValues(char[] validValues)
        {
            int minValue = validValues[0];
            int maxValue = validValues[validValues.Length - 1];

            var length = maxValue - minValue + 1;

            var bitArray = new BitArray(length);

            foreach (var value in validValues)
            {
                bitArray.Set(value - minValue, true);
            }

            return new TokenValues(bitArray, minValue);
        }

        private struct TokenValues
        {
            public TokenValues(BitArray bitArray, int offset)
            {
                BitArray = bitArray;
                Offset = offset;
            }

            private BitArray BitArray { get; }

            private int Offset { get; }

            public bool IsValid(char value)
            {
                var index = value - Offset;

                return index >= 0
                    && index < BitArray.Length
                    && BitArray[index];
            }
        }
    }
}