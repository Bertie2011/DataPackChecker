namespace DataPackChecker.Shared.Util {
    static public class NullableExtensions {
        static public bool TryValue<T>(this T? nullable, out T value) where T : struct {
            value = nullable.GetValueOrDefault();
            return nullable.HasValue;
        }
    }
}
