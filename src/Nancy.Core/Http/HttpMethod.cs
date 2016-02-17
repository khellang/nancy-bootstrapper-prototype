namespace Nancy.Core.Http
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString(), nq}")]
    public partial struct HttpMethod : IEquatable<HttpMethod>
    {
        public HttpMethod(string value)
        {
            Check.NotNull(value, nameof(value));

            if (!HttpUtility.IsValidToken(value))
            {
                throw new ArgumentException(string.Format(
                    Resources.Exception_InvalidHttpMethodToken, value), nameof(value));
            }

            this.Value = value;
        }

        public string Value { get; }

        public bool Equals(HttpMethod other)
        {
            return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && obj is HttpMethod && this.Equals((HttpMethod)obj);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(this.Value);
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
            return this.Value;
        }
    }
}
