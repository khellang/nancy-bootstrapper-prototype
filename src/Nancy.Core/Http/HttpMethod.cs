using System;
using System.Diagnostics;

namespace Nancy.Core.Http
{
    [DebuggerDisplay("{ToString(), nq}")]
    public partial struct HttpMethod : IEquatable<HttpMethod>
    {
        public HttpMethod(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!HttpUtility.IsValidToken(value))
            {
                throw new ArgumentException(string.Format(
                    Resources.Exception_InvalidHttpMethodToken, value), nameof(value));
            }

            Value = value;
        }

        public string Value { get; }

        public bool Equals(HttpMethod other)
        {
            return string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && obj is HttpMethod && Equals((HttpMethod) obj);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }

        public static bool operator ==(HttpMethod left, HttpMethod right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HttpMethod left, HttpMethod right)
        {
            return !left.Equals(right);
        }

        public static implicit operator HttpMethod(string value)
        {
            return new HttpMethod(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}