using System;

namespace Prototype
{
    [Serializable]
    public struct JsonDateTime
    {
        public long value;

        public static implicit operator DateTime(JsonDateTime jdt)
        {
            return DateTime.FromFileTimeUtc(jdt.value);
        }
        public static implicit operator JsonDateTime(DateTime dt)
        {
            JsonDateTime jdt = new JsonDateTime();
            jdt.value = dt.ToFileTimeUtc();
            return jdt;
        }

        public override string ToString()
        {
            return $"value: {value}";
        }
    }
}