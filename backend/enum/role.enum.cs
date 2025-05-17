using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace backend.role
{
    [JsonConverter(typeof(JsonStringEnumConverter))]  // Bắt buộc nếu muốn serialize là chuỗi

    public enum Role
    {
        [EnumMember(Value = "User")]
        User = 0,

        [EnumMember(Value = "Admin")]
        Admin = 1,

        [EnumMember(Value = "Doctor")]
        Doctor = 2,

        [EnumMember(Value = "Staff")]
        Staff = 3
    }
}
