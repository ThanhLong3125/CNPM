import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { fetchPatientById, updatePatientById } from "../../service/staffService";
import type { PatientForm } from "../../types/staff.types";

const UpdatePatientRecord: React.FC = () => {
  const navigate = useNavigate();
  const { patientId: idPatient } = useParams<{ patientId: string }>();
  const [realId, setRealId] = useState<string>("");

  const [patientCode, setPatientCode] = useState<string>("");
  const [formData, setFormData] = useState<PatientForm>({
    fullName: "",
    gender: "",
    dateOfBirth: "",
    phone: "",
    email: "",
    medicalHistory: "",
  });

  useEffect(() => {
    const fetchData = async () => {
      const found = await fetchPatientById(idPatient!);
       console.log("BE trả về:", found);
      if (found) {
        setFormData({
          fullName: found.fullName || "",
          gender: found.gender || "",
          dateOfBirth: found.dateOfBirth?.slice(0, 10) || "",
          phone: found.phone || "",
          email: found.email || "",
          medicalHistory: found.medicalHistory || "",
        });

        setPatientCode(found.patientID || ""); // để hiển thị
        setRealId(found.id || ""); // Lưu ID gốc cho update
      }
    };
    fetchData();
  }, [idPatient]);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleCancel = () => navigate(-1);

  const handleUpdate = async () => {
    if (!formData.fullName.trim() || !formData.phone.trim() || !formData.email.trim()) {
      alert("Vui lòng điền đầy đủ họ tên, SĐT và email!");
      return;
    }

    const payload: PatientForm = {
      ...formData,
      dateOfBirth: formData.dateOfBirth,
    };

    const success = await updatePatientById(patientCode, payload);
    if (success) {
      alert("Cập nhật thành công!");
      navigate(-1);
    } else {
      alert("Cập nhật thất bại!");
    }
  };

  return (
    <div className="flex items-center justify-center p-4">
      <div className="bg-[#89AFE0] backdrop-blur-sm rounded-3xl p-8 w-full max-w-5xl shadow-2xl relative">
        <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
          <h2 className="text-white text-center font-semibold text-lg">Cập nhật hồ sơ</h2>
        </div>

        <div className="space-y-6 pt-8">
          <div>
            <label className="block text-white font-medium mb-2">Mã bệnh nhân</label>
            <input
              type="text"
              value={patientCode}
              disabled
              className="w-full px-3 py-2 bg-gray-200 rounded text-gray-700"
            />
          </div>

          <div>
            <label className="block text-white font-medium mb-2">Họ Tên</label>
            <input
              type="text"
              name="fullName"
              value={formData.fullName}
              onChange={handleChange}
              className="w-full px-3 py-2 bg-white rounded"
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-white font-medium mb-2">Giới tính</label>
              <select
                name="gender"
                value={formData.gender}
                onChange={handleChange}
                className="w-full px-3 py-2 bg-white rounded"
              >
                <option value="">Chọn</option>
                <option value="male">Nam</option>
                <option value="female">Nữ</option>
                <option value="other">Khác</option>
              </select>
            </div>

            <div>
              <label className="block text-white font-medium mb-2">Ngày sinh</label>
              <input
                type="date"
                name="dateOfBirth"
                value={formData.dateOfBirth}
                onChange={handleChange}
                className="w-full px-3 py-2 bg-white rounded"
              />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-white font-medium mb-2">SĐT</label>
              <input
                type="text"
                name="phone"
                value={formData.phone}
                onChange={handleChange}
                className="w-full px-3 py-2 bg-white rounded"
              />
            </div>

            <div>
              <label className="block text-white font-medium mb-2">Email</label>
              <input
                type="text"
                name="email"
                value={formData.email}
                onChange={handleChange}
                className="w-full px-3 py-2 bg-white rounded"
              />
            </div>
          </div>

          <div>
            <label className="block text-white font-medium mb-2">Bệnh sử</label>
            <textarea
              name="medicalHistory"
              value={formData.medicalHistory}
              onChange={handleChange}
              rows={5}
              className="w-full px-3 py-2 bg-white rounded resize-none"
            />
          </div>

          <div className="flex w-full pt-4">
            <div className="w-1/2" />
            <div className="flex w-1/2 gap-4">
              <button
                type="button"
                onClick={handleCancel}
                className="flex-1 py-3 px-6 rounded-lg bg-white/20 text-white font-medium hover:bg-white/30 border border-white/30"
              >
                Hủy
              </button>
              <button
                type="button"
                onClick={handleUpdate}
                className="flex-1 py-3 px-6 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-700"
              >
                Xác nhận
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UpdatePatientRecord;
