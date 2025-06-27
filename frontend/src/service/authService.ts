import api from "../service/apiSerive"
//Đăng nhập
export const authService = {
  login: async (email: string, password: string) => {
    const response = await api.post('/Auth/login', {
      email,
      password,
    });
    return response.data; 
  },
};
//Thông tin người dùng
export const fetchUser = async () => {
  const token = sessionStorage.getItem("accessToken");
  console.log("FE token:", token);

  try {
    const response = await api.get("/Auth/me", {
      headers: {
        Authorization: `Bearer ${token}`, 
      },
    });
    console.log("User data:", response.data);
    return response.data;
  } catch (error: any) {
    console.error("Fetch user error:", error.response?.status, error.response?.data);
    return null;
  }
};
//Đặt lại mật khẩu
export const resetPasswordByEmail = async (email: string, newPassword: string): Promise<boolean> => {
  try {
    console.log("Sending reset for:", email, newPassword);
    const response = await api.post("/api/Auth/reset-password-email", {
      email,
      newPassword,
    });

    console.log("Reset password response:", response.status, response.data);

    // Nếu backend trả success = false hoặc message lỗi
    if (response.status === 200 && response.data?.success !== false) {
      return true;
    }

    return false;
  } catch (error: any) {
    console.error("Lỗi reset mật khẩu:", error.response?.data || error.message);
    return false;
  }
};
