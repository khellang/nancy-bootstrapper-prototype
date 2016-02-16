using System;
using System.Diagnostics;

namespace Nancy.Core.Http
{
    [DebuggerDisplay("{ToString(), nq}")]
    public partial struct HttpStatusCode : IEquatable<HttpStatusCode>
    {
        public HttpStatusCode(int value)
        {
            if (value < 100 || value > 999)
            {
                throw new ArgumentException(string.Format(
                    Resources.Exception_InvalidHttpStatusCode, value), nameof(value));
            }

            Value = value;
        }

        public int Value { get; }

        public bool Equals(HttpStatusCode other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && obj is HttpStatusCode && Equals((HttpStatusCode) obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(HttpStatusCode left, HttpStatusCode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HttpStatusCode left, HttpStatusCode right)
        {
            return !left.Equals(right);
        }

        public static implicit operator HttpStatusCode(int value)
        {
            return new HttpStatusCode(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}