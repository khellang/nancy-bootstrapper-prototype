namespace Nancy.Core
{
    using System;

    public class EngineCompositionException : Exception
    {
        public EngineCompositionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
