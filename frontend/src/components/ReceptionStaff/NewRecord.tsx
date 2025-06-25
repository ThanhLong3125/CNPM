import React, { useState } from "react";
import { FaChevronDown } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { createPatient } from "../../service/staffService";
import type { PatientForm } from "../../types/staff.types";

const PatientRecordForm: React.FC = () => {
  const [formData, setFormData] = useState<PatientForm>({
    fullName: "",
    gender: "",
    dateOfBirth: "",
    phone: "",
    email: "",
    medicalHistory: "",
  });

  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleCancel = () => {
    navigate(-1);
  };

  const handleConfirm = async () => {
    if (!formData.fullName.trim() || !formData.email.trim() || !formData.phone.trim()) {
      alert("Vui lòng điền đầy đủ Họ tên, Email và SĐT.");
      return;
    }

    const payload = {
      ...formData,
      dateOfBirth: new Date(formData.dateOfBirth).toISOString().split("T")[0],
    };

    console.log("Payload gửi đi:", payload);

    setIsSubmitting(true);
    const success = await createPatient(payload);
    setIsSubmitting(false);

    if (success) {
      alert("Tạo hồ sơ bệnh nhân thành công!");
      navigate(-1);
    } else {
      alert("Tạo hồ sơ thất bại. Vui lòng thử lại.");
    }
  };

  return (
    <div className="max-h-max flex items-center justify-center p-4">
      <div className="bg-[#89AFE0] backdrop-blur-sm rounded-3xl relative py-8 px-20 w-full max-w-5xl shadow-2xl">
        <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
          <h2 className="text-white text-center font-semibold text-lg">Tạo hồ sơ bệnh nhân</h2>
        </div>

        <div className="space-y-6">
          <div>
            <label className="block text-[#133574] font-medium mb-2">Họ Tên</label>
            <input
              type="text"
              name="fullName"
              value={formData.fullName}
              onChange={handleInputChange}
              className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all"
              placeholder="Nhập họ và tên"
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-[#133574] font-medium mb-2">Giới tính</label>
              <div className="relative">
                <select
                  name="gender"
                  value={formData.gender}
                  onChange={handleInputChange}
                  className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 appearance-none cursor-pointer transition-all"
                >
                  <option value="">Chọn giới tính</option>
                  <option value="male">Nam</option>
                  <option value="female">Nữ</option>
                  <option value="other">Khác</option>
                </select>
                <FaChevronDown className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 w-5 h-5 pointer-events-none" />
              </div>
            </div>

            <div>
              <label className="block text-[#133574] font-medium mb-2">Ngày sinh</label>
              <input
                type="date"
                name="dateOfBirth" 
                value={formData.dateOfBirth}
                onChange={handleInputChange}
                className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 cursor-pointer transition-all"
              />
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-[#133574] font-medium mb-2">SĐT</label>
              <input
                type="text"
                name="phone"
                value={formData.phone}
                onChange={handleInputChange}
                className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all"
                placeholder="Nhập số điện thoại"
              />
            </div>
            <div>
              <label className="block text-[#133574] font-medium mb-2">Email</label>
              <input
                type="text"
                name="email"
                value={formData.email}
                onChange={handleInputChange}
                className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all"
                placeholder="Nhập email"
              />
            </div>
          </div>

          <div>
            <label className="block text-[#133574] font-medium mb-2">Bệnh sử</label>
            <textarea
              name="symptoms"
              value={formData.medicalHistory}
              onChange={handleInputChange}
              rows={6}
              className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 resize-none transition-all"
              placeholder="Nhập thông tin bệnh sử của bệnh nhân..."
            />
          </div>

          <div className="flex w-full">
            <div className="w-1/2" />
            <div className="flex w-1/2 gap-4 pt-4">
              <button
                type="button"
                onClick={handleCancel}
                className="flex-1 py-3 px-6 rounded-lg bg-white/20 text-white font-medium hover:bg-white/30 focus:outline-none focus:ring-2 focus:ring-white/50 transition-all duration-200 border border-white/30"
              >
                Hủy
              </button>
              <button
                type="button"
                onClick={handleConfirm}
                disabled={isSubmitting}
                className={`flex-1 py-3 px-6 rounded-lg ${
                  isSubmitting
                    ? "bg-gray-400 cursor-not-allowed"
                    : "bg-[#618FCA] hover:bg-blue-700"
                } text-white font-medium focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg`}
              >
                {isSubmitting ? "Đang gửi..." : "Xác nhận"}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PatientRecordForm;
