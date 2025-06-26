import api from "../service/apiSerive"

export const authService = {
  login: async (email: string, password: string) => {
    const response = await api.post('/Auth/login', {
      email,
      password,
    });
    return response.data; 
  },
};
export const fetchUser = async () => {
  const token = sessionStorage.getItem("accessToken");
  console.log("FE token:", token);

  try {
    const response = await api.get("/Auth/me", {
      headers: {
        Authorization: `Bearer ${token}`, // Gắn token thủ công để test
      },
    });
    console.log("User data:", response.data);
    return response.data;
  } catch (error: any) {
    console.error("Fetch user error:", error.response?.status, error.response?.data);
    return null;
  }
};
