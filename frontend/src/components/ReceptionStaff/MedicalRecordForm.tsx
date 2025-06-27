import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { fetchMedicalRecordById, fetchPatientById, deleteMedicalRecord } from "../../service/staffService";

const DetailCreatedRecord = () => {
  const navigate = useNavigate();
  const { medicalRecordId } = useParams<{ medicalRecordId: string }>();

  const [record, setRecord] = useState<any>(null);
  const [patient, setPatient] = useState<any>(null);
  const [loading, setLoading] = useState(true);

  const handleCancel = () => {
    navigate(-1);
  };


  const handleDelete = async () => {
    if (!medicalRecordId) return;

    const confirmDelete = window.confirm("Bạn có chắc chắn muốn xoá bệnh án này?");
    if (!confirmDelete) return;

    const success = await deleteMedicalRecord(medicalRecordId);
    if (success) {
      alert("Xoá bệnh án thành công!");
      navigate("/staff/CreatedRecordList");
    } else {
      alert("Xoá bệnh án thất bại!");
    }
  };

  const handleEdit = () => {
  if (medicalRecordId) {
    navigate(`/staff/EditMedicalRecord/${medicalRecordId}`);
  }
};



  useEffect(() => {
    const load = async () => {
      console.log("🔍 medicalRecordId param:", medicalRecordId);

      if (!medicalRecordId) {
        console.error(" medicalRecordId không tồn tại.");
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const medicalRecord = await fetchMedicalRecordById(medicalRecordId);
        console.log("📄 Medical Record:", medicalRecord);

        setRecord(medicalRecord);

        if (medicalRecord?.patientId) {
          const patientData = await fetchPatientById(medicalRecord.patientId);
          console.log("🧍‍♂️ Patient Data:", patientData);
          setPatient(patientData);
        } else {
          console.warn("⚠️ Medical record không có patientId");
        }

      } catch (error) {
        console.error(" Lỗi khi tải dữ liệu chi tiết:", error);
      } finally {
        setLoading(false);
      }
    };

    load();
  }, [medicalRecordId]);

  if (loading) {
    return <div className="text-center p-6 text-blue-700">Đang tải dữ liệu hồ sơ bệnh án...</div>;
  }

  if (!record || !patient) {
    return <div className="text-center p-6 text-red-600">Không tìm thấy dữ liệu!</div>;
  }

  return (
    <div className="min-h-screen">
      <div className="p-4 flex justify-center">
        <div className="w-full max-w-screen relative bg-[#89AFE0] rounded-lg p-6">
          <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
            <h2 className="text-white text-center font-semibold text-lg">Hồ sơ bệnh án</h2>
          </div>

          <div className="grid grid-cols-2 mt-6 gap-x-12 gap-y-4">
            <div>
              <div className="flex flex-col sm:flex-row gap-5">
                <div>
                  <label className="block mb-1 text-sm">Mã bệnh nhân</label>
                  <input
                    type="text"
                    value={patient.idPatient || ""}
                    disabled
                    className="w-full px-6 py-1.5 bg-[#f0f0f0] rounded"
                  />
                </div>

                <div>
                  <label className="block mb-1 text-sm">Mã bệnh án</label>
                  <input
                    type="text"
                    value={record.medicalRecordId || ""}
                    disabled
                    className="w-full px-6 py-1.5 bg-[#f0f0f0] rounded"
                  />
                </div>
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">Họ Tên</label>
                <input
                  type="text"
                  value={patient.fullName || ""}
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                />
              </div>

              <div className="flex flex-col sm:flex-row gap-5">
                <div>
                  <label className="block mb-1 text-sm">Giới tính</label>
                  <select
                    value={patient.gender?.toLowerCase() === "male" ? "Nam" : "Nữ"}
                    disabled
                    className="w-full px-10 py-2 bg-gray-200 rounded-lg shadow-sm"
                  >
                    <option value="Nam">Nam</option>
                    <option value="Nữ">Nữ</option>
                  </select>
                </div>

                <div>
                  <label className="block mb-1 text-sm">Ngày sinh</label>
                  <input
                    type="date"
                    value={patient.dateOfBirth?.slice(0, 10) || ""}
                    disabled
                    className="w-full px-10 py-2 bg-gray-200 rounded-lg"
                  />
                </div>
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">SDT hoặc Email</label>
                <input
                  type="text"
                  value={patient.phone || patient.email || ""}
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                />
              </div>

              <div>
                <label className="block mb-1 text-sm">Bệnh sử</label>
                <textarea
                  rows={5}
                  value={patient.medicalHistory || ""}
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded resize-none"
                />
              </div>
            </div>

            <div>
              <div className="mb-4">
                <label className="block mb-1 text-sm">Thời gian tạo</label>
                <input
                  type="text"
                  value={new Date(record.createdDate).toLocaleString("vi-VN")}
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                />
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">Mã bác sĩ</label>
                <input
                  type="text"
                  value={record.assignedPhysicianId || ""}
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                />
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">Triệu chứng</label>
                <textarea
                  rows={7}
                  value={record.symptoms || ""}
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded resize-none"
                />
              </div>

              <div className="flex justify-end gap-4 mt-6">
                <button
                  className="border border-gray-500 hover:bg-blue-400 text-black-900 px-10 py-2 rounded"
                  onClick={handleCancel}
                >
                  Hủy
                </button>
                <button
                  className="bg-[#4977b8] hover:bg-blue-700 text-white px-6 py-2 rounded"
                  onClick={handleDelete}
                >
                  Xóa bệnh án
                </button>
                <button
                  className="bg-[#4977b8] hover:bg-blue-700 text-white px-6 py-2 rounded"
                  onClick={handleEdit}
                >
                  Sửa bệnh án
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DetailCreatedRecord;
