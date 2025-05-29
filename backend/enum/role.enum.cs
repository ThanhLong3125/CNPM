using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace backend.role
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Role
    {
        [EnumMember(Value = "Admin")]
        Admin = 1,

        [EnumMember(Value = "Doctor")]
        Doctor = 2,

        [EnumMember(Value = "Staff")]
        Staff = 3,

        [EnumMember(Value = "Technician")]
        Technician = 4
    }
}
