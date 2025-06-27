import api from "../service/apiSerive";
//Danh sách bệnh nhân đang chờ
export const fetchWaitingPatients = async () => {
  try {
    const response = await api.get("/Doctor/waiting-patients");
    console.log("Waiting patients:", response.data);
    return response.data;
  } catch (error: any) {
    console.error("Lỗi khi lấy danh sách bệnh nhân đang chờ:", error.response?.data || error.message);
    throw error;
  }
};