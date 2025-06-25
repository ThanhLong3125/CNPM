import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { fetchUser } from "../../service/staffService";

const ReceptionProfile: React.FC = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState<any>(null);

  const handleLogout = () => {
    sessionStorage.removeItem("accessToken");
    sessionStorage.removeItem("role");
    navigate("/");
  };

  useEffect(() => {
    const loadUser = async () => {
      const data = await fetchUser();
      setUser(data);
    };
    loadUser();
  }, []);

  return (
    <div className="flex justify-center pt-10">
      <div className="flex justify-center items-center flex-grow">
        <div className="bg-[#83a8dd] rounded-2xl relative p-10 w-[600px] shadow-md">
          <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-fit px-6 py-2 rounded-xl shadow-md">
            <h2 className="text-white text-center font-semibold text-lg">Hồ sơ người dùng</h2>
          </div>

          <form className="space-y-5">
            <div>
              <label className="block text-white mb-1">Mã lễ tân</label>
              <input
                type="text"
                value={user?.receptionistId || ""}
                className="w-full px-3 py-2 rounded bg-white text-black"
                disabled
              />
            </div>

            <div>
              <label className="block text-white mb-1">Họ Tên</label>
              <input
                type="text"
                value={user?.fullName || ""}
                className="w-full px-3 py-2 rounded bg-white text-black"
                disabled
              />
            </div>

            <div className="flex space-x-4">
              <div className="w-1/2">
                <label className="block text-white mb-1">Giới tính</label>
                <select
                  value={user?.gender || ""}
                  disabled
                  className="w-full px-3 py-2 rounded bg-white text-black"
                >
                  <option value="Nam">Nam</option>
                  <option value="Nữ">Nữ</option>
                </select>
              </div>
              <div className="w-1/2">
                <label className="block text-white mb-1">Ngày sinh</label>
                <input
                  type="date"
                  value={user?.dateOfBirth?.slice(0, 10) || ""}
                  disabled
                  className="w-full px-3 py-2 rounded bg-white text-black"
                />
              </div>
            </div>

            <div>
              <label className="block text-white mb-1">SDT</label>
              <input
                type="text"
                value={user?.phone || ""}
                className="w-full px-3 py-2 rounded bg-white text-black"
                disabled
              />
            </div>

            <div className="flex justify-end pt-4">
              <button
                type="button"
                onClick={handleLogout}
                className="bg-[#5e8fc5] text-white px-6 py-2 rounded-xl hover:bg-[#4b7bb3]"
              >
                Đăng xuất
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ReceptionProfile;
