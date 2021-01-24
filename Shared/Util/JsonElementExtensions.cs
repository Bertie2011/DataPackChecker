using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.Util {
    /// <summary>
    /// <b>TryGet VS TryAs</b><br/>
    /// The JsonElement TryGet... methods throw when the type does not match and only try to parse the data.
    /// The TryAs... extension methods also take value type into account and return false (instead of throw) when the type does not match.
    /// </summary>
    public static class JsonElementExtensions {
        public static bool IsArray(this JsonElement json) {
            return json.ValueKind == JsonValueKind.Array;
        }

        public static bool IsObject(this JsonElement json) {
            return json.ValueKind == JsonValueKind.Object;
        }

        public static bool IsNull(this JsonElement json) {
            return json.ValueKind == JsonValueKind.Null;
        }

        #region TryAs
        /// <summary>
        /// Returns true if this is a boolean. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsBool(this JsonElement json, out bool value) {
            if (json.ValueKind == JsonValueKind.False || json.ValueKind == JsonValueKind.True) {
                value = json.GetBoolean();
                return true;
            } else {
                value = default;
                return false;
            }
        }

        /// <summary>
        /// Returns true if this is a string. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsString(this JsonElement json, out string value) {
            if (json.ValueKind == JsonValueKind.String) {
                value = json.GetString();
                return true;
            } else {
                value = default;
                return false;
            }
        }

        /// <summary>
        /// Returns true if this is a byte. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsByte(this JsonElement json, out byte value) {
            value = default;
            return json.ValueKind == JsonValueKind.Number && json.TryGetByte(out value);
        }

        /// <summary>
        /// Returns true if this represents bytes from base64. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsBytesFromBase64(this JsonElement json, out byte[] value) {
            value = default;
            return json.ValueKind == JsonValueKind.String && json.TryGetBytesFromBase64(out value);
        }

        /// <summary>
        /// Returns true if this is a DateTime. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDateTime(this JsonElement json, out DateTime value) {
            value = default;
            return json.ValueKind == JsonValueKind.String && json.TryGetDateTime(out value);
        }

        /// <summary>
        /// Returns true if this is a DateTimeOffset. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDateTimeOffset(this JsonElement json, out DateTimeOffset value) {
            value = default;
            return json.ValueKind == JsonValueKind.String && json.TryGetDateTimeOffset(out value);
        }

        /// <summary>
        /// Returns true if this is a decimal. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDecimal(this JsonElement json, out decimal value) {
            value = default;
            return json.ValueKind == JsonValueKind.Number && json.TryGetDecimal(out value);
        }

        /// <summary>
        /// Returns true if this is a double. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDouble(this JsonElement json, out double value) {
            value = default;
            return json.ValueKind == JsonValueKind.Number && json.TryGetDouble(out value);
        }

        /// <summary>
        /// Returns true if this is a Guid. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsGuid(this JsonElement json, out Guid value) {
            value = default;
            return json.ValueKind == JsonValueKind.String && json.TryGetGuid(out value);
        }


        /// <summary>
        /// Returns true if this is a short. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsShort(this JsonElement json, out short value) {
            value = default;
            return json.ValueKind == JsonValueKind.Number && json.TryGetInt16(out value);
        }

        /// <summary>
        /// Returns true if this is an int. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsInt(this JsonElement json, out int value) {
            value = default;
            return json.ValueKind == JsonValueKind.Number && json.TryGetInt32(out value);
        }

        /// <summary>
        /// Returns true if this is a long. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsLong(this JsonElement json, out long value) {
            value = default;
            return json.ValueKind == JsonValueKind.Number && json.TryGetInt64(out value);
        }

        /// <summary>
        /// Returns true if this is a float. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsFloat(this JsonElement json, out float value) {
            value = default;
            return json.ValueKind == JsonValueKind.Number && json.TryGetSingle(out value);
        }
        #endregion TryAs

        #region TryAs property
        /// <summary>
        /// Returns true if the property is a boolean. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsBool(this JsonElement json, string property, out bool value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsBool(out value);
        }

        /// <summary>
        /// Returns true if the property is a string. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsString(this JsonElement json, string property, out string value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsString(out value);
        }

        /// <summary>
        /// Returns true if the property is a byte. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsByte(this JsonElement json, string property, out byte value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsByte(out value);
        }

        /// <summary>
        /// Returns true if the property represents bytes from base64. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsBytesFromBase64(this JsonElement json, string property, out byte[] value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsBytesFromBase64(out value);
        }

        /// <summary>
        /// Returns true if the property is a DateTime. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDateTime(this JsonElement json, string property, out DateTime value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsDateTime(out value);
        }

        /// <summary>
        /// Returns true if the property is a DateTimeOffset. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDateTimeOffset(this JsonElement json, string property, out DateTimeOffset value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsDateTimeOffset(out value);
        }

        /// <summary>
        /// Returns true if the property is a decimal. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDecimal(this JsonElement json, string property, out decimal value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsDecimal(out value);
        }

        /// <summary>
        /// Returns true if the property is a double. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsDouble(this JsonElement json, string property, out double value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsDouble(out value);
        }

        /// <summary>
        /// Returns true if the property is a Guid. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsGuid(this JsonElement json, string property, out Guid value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsGuid(out value);
        }


        /// <summary>
        /// Returns true if the property is a short. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsShort(this JsonElement json, string property, out short value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsShort(out value);
        }

        /// <summary>
        /// Returns true if the property is an int. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsInt(this JsonElement json, string property, out int value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsInt(out value);
        }
        /// <summary>
        /// Returns true if the property is a long. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsLong(this JsonElement json, string property, out long value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsLong(out value);
        }

        /// <summary>
        /// Returns true if the property is a float. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsFloat(this JsonElement json, string property, out float value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out JsonElement propObj) && propObj.TryAsFloat(out value);
        }

        /// <summary>
        /// Returns true if the property is an array. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsArray(this JsonElement json, string property, out JsonElement value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out value) && value.IsArray();
        }

        /// <summary>
        /// Returns true if the property is an object. The out parameter contains the value.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool TryAsObject(this JsonElement json, string property, out JsonElement value) {
            value = default;
            return json.IsObject() && json.TryGetProperty(property, out value) && value.IsObject();
        }

        /// <summary>
        /// Returns true if the property is null.
        /// <br/><br/><inheritdoc cref="JsonElementExtensions"/>
        /// </summary>
        public static bool IsNull(this JsonElement json, string property) {
            return json.IsObject() && json.TryGetProperty(property, out JsonElement value) && value.IsNull();
        }
        #endregion TryAs property
    }
}
