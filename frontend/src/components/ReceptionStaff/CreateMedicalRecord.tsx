import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { createMedicalRecord, fetchPatientById } from "../../service/staffService";
import type { CreateRecord, PatientRecordDeclare } from "../../types/staff.types";

const CreateMedicalRecord: React.FC = () => {
  const navigate = useNavigate();
  const { patientId } = useParams<{ patientId: string }>();

  const [patient, setPatient] = useState<PatientRecordDeclare | null>(null);
  const [assignedPhysicianId, setAssignedPhysicianId] = useState("");
  const [symptoms, setSymptoms] = useState("");
  const [isPriority, setIsPriority] = useState(false);

  useEffect(() => {
    console.log("patientId param:", patientId);
    const load = async () => {
      if (!patientId) return;

      const found = await fetchPatientById(patientId);
      console.log("Dữ liệu nhận được từ API:", found);
      if (found) {
        setPatient({
          id: found.id,
          patientID: found.idPatient ?? "", 
          fullName: found.fullName || "",
          gender: found.gender || "",
          dateOfBirth: found.dateOfBirth?.slice(0, 10) || "",
          phone: found.phone || "",
          email: found.email || "",
          medicalHistory: found.medicalHistory || "",
        });

      }
    };
    load();
  }, [patientId]);



  const handleCancel = () => navigate(-1);
  const togglePriority = () => setIsPriority(prev => !prev);

  const handleCreate = async () => {
    if (!patient) return;

    const payload: CreateRecord = {
      patientID:patient.patientID,
      symptoms,
      assignedPhysicianId,
      isPriority,
    };

    const success = await createMedicalRecord(payload);
    if (success) {
      alert("Tạo bệnh án thành công!");
      navigate("/staff/CreatedRecordList");
    } else {
      alert("Tạo bệnh án thất bại!");
    }
  };

  return (
    <div className="p-4 flex justify-center">
      <div className="w-full max-w-screen bg-[#89AFE0] relative rounded-lg p-6">
        <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
          <h2 className="text-white text-center font-semibold text-lg">Tạo bệnh án</h2>
        </div>

        <div className="grid grid-cols-2 gap-x-12 mt-6 mx-3 gap-y-4">
          <div>
            <div className="mb-4">
              <label className="block mb-1 text-sm">Mã bệnh nhân</label>
              <input type="text" value={patient?.patientID || ""} disabled className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
            </div>

            <div className="mb-4">
              <label className="block mb-1 text-sm">Họ Tên</label>
              <input type="text" value={patient?.fullName || ""} disabled className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
            </div>

            <div className="flex flex-col sm:flex-row gap-5">
              <div>
                <label className="block font-medium text-sm text-gray-700 mb-1">Giới tính</label>
                <input type="text" value={patient?.gender === "male" ? "Nam" : "Nữ"} disabled className="w-full px-10 py-2 bg-gray-200 rounded-lg" />
              </div>

              <div>
                <label className="block font-medium text-sm text-gray-700 mb-1">Ngày sinh</label>
                <input type="date" value={patient?.dateOfBirth || ""} disabled className="w-full px-10 py-2 bg-gray-200 rounded-lg" />
              </div>
            </div>
            <div className="grid grid-cols-2 gap-4 mt-2">

              <div className="mb-4">
                <label className="block mb-1 text-sm">SĐT</label>
                <input type="text" value={patient?.phone || ""} disabled className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">Email</label>
                <input type="text" value={patient?.email || ""} disabled className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
              </div>
            </div>
            <div>
              <label className="block mb-1 text-sm">Bệnh sử</label>
              <textarea
                rows={5}
                value={patient?.medicalHistory || ""}
                disabled
                className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded resize-none"
              />
            </div>
          </div>

          <div>
            <div className="mb-4">
              <label className="block mb-1 text-sm">Mã bác sĩ</label>
              <input
                type="text"
                value={assignedPhysicianId}
                onChange={(e) => setAssignedPhysicianId(e.target.value)}
                className="w-full px-3 py-1.5 bg-white rounded"
              />
            </div>

            <div className="mb-4">
              <label className="block mb-1 text-sm">Triệu chứng</label>
              <textarea
                rows={10}
                value={symptoms}
                onChange={(e) => setSymptoms(e.target.value)}
                className="w-full px-3 py-1.5 bg-white rounded resize-none"
              />
            </div>

            <div className="flex justify-center gap-4 mt-6">
              <button
                type="button"
                className={`px-10 py-2 rounded ${isPriority
                  ? "bg-[#618FCA] text-white hover:bg-blue-500"
                  : "bg-[#618FCA] text-black hover:bg-blue-500"
                  }`}
                onClick={togglePriority}
              >
                Ưu tiên
              </button>

              <button
                className="border border-gray-500 hover:bg-blue-400 text-black px-10 py-2 rounded"
                onClick={handleCancel}
              >
                Hủy
              </button>

              <button
                className="bg-[#618FCA] hover:bg-blue-700 text-white px-10 py-2 rounded"
                onClick={handleCreate}
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

export default CreateMedicalRecord;
