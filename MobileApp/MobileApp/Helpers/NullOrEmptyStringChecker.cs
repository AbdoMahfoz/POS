namespace MobileApp.Helpers
{
    public static class NullOrEmptyStringChecker
    {
        public static bool HasNullOrEmptyStrings<T>(T obj)
        {
            if (obj == null) return true;
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (!propertyInfo.CanRead) continue;
                if (propertyInfo.PropertyType != typeof(string)) continue;
                var val = (string) propertyInfo.GetValue(obj);
                if (string.IsNullOrWhiteSpace(val)) return true;
            }

            return false;
        }
    }
}