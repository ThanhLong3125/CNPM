import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  fetchMedicalRecordById,
  fetchPatientById,
  updateMedicalRecord,
} from "../../service/staffService";

const EditMedicalRecord = () => {
  const navigate = useNavigate();
  const { medicalRecordId } = useParams<{ medicalRecordId: string }>();

  const [record, setRecord] = useState<any>(null);
  const [patient, setPatient] = useState<any>(null);
  const [symptoms, setSymptoms] = useState("");
  const [assignedPhysicianId, setAssignedPhysicianId] = useState("");
  const [isPriority, setIsPriority] = useState(false);

  useEffect(() => {
    const load = async () => {
      if (!medicalRecordId) return;

      const recordData = await fetchMedicalRecordById(medicalRecordId);
      setRecord(recordData);
      setSymptoms(recordData?.symptoms || "");
      setAssignedPhysicianId(recordData?.assignedPhysicianId || "");
      setIsPriority(recordData?.isPriority || false);

      if (recordData?.patientId) {
        const patientData = await fetchPatientById(recordData.patientId);
        setPatient(patientData);
      }
    };

    load();
  }, [medicalRecordId]);

  const handleCancel = () => navigate(-1);
  const handlePriority = () => navigate("/staff/CreatedRecordList");

  const handleEdited = async () => {
    if (!medicalRecordId) return;

    const payload = {
      symptoms,
      assignedPhysicianId,
      isPriority,
    };

    const success = await updateMedicalRecord(medicalRecordId, payload);
    if (success) {
      alert("Cập nhật bệnh án thành công!");
      navigate("/staff/CreatedRecordList");
    } else {
      alert("Cập nhật bệnh án thất bại!");
    }
  };

  return (
    <div className="min-h-screen">
      <div className="p-4 flex justify-center">
        <div className="w-full max-w-screen bg-[#89AFE0] relative rounded-lg p-6">
          <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
            <h2 className="text-white text-center font-semibold text-lg">
              Hồ sơ bệnh án
            </h2>
          </div>

          <div className="grid grid-cols-2 mt-6 gap-x-12 gap-y-4">
            <div>
              <div className="flex flex-col sm:flex-row gap-5">
                <div>
                  <label className="block mb-1 text-sm">Mã bệnh nhân</label>
                  <input
                    type="text"
                    value={patient?.idPatient || ""}
                    disabled
                    className="w-full px-6 py-1.5 bg-[#f0f0f0] rounded"
                  />
                </div>

                <div>
                  <label className="block mb-1 text-sm">Mã bệnh án</label>
                  <input
                    type="text"
                    value={record?.medicalRecordId || ""}
                    disabled
                    className="w-full px-6 py-1.5 bg-[#f0f0f0] rounded"
                  />
                </div>
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">Họ Tên</label>
                <input
                  type="text"
                  value={patient?.fullName || ""}
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                />
              </div>

              <div className="flex flex-col sm:flex-row gap-5">
                <div>
                  <label className="block mb-1 text-sm">Giới tính</label>
                  <select
                    value={
                      patient?.gender?.toLowerCase() === "male" ? "Nam" : "Nữ"
                    }
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
                    value={patient?.dateOfBirth?.slice(0, 10) || ""}
                    disabled
                    className="w-full px-10 py-2 bg-gray-200 rounded-lg"
                  />
                </div>
              </div>

              <div className="grid grid-cols-2 gap-4 mt-2">
                <div className="mb-4">
                  <label className="block mb-1 text-sm">SDT</label>
                  <input
                    type="text"
                    value={patient?.phone || ""}
                    disabled
                    className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                  />
                </div>

                <div className="mb-4">
                  <label className="block mb-1 text-sm">Email</label>
                  <input
                    type="text"
                    value={patient?.email || ""}
                    disabled
                    className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                  />
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
                <label className="block mb-1 text-sm">Thời gian tạo</label>
                <input
                  type="text"
                  value={
                    record?.createdDate
                      ? new Date(record.createdDate).toLocaleString("vi-VN")
                      : ""
                  }
                  disabled
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded"
                />
              </div>

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
                  rows={7}
                  value={symptoms}
                  onChange={(e) => setSymptoms(e.target.value)}
                  className="w-full px-3 py-1.5 bg-white rounded resize-none"
                />
              </div>

              <div className="flex justify-end gap-4 mt-6">
                <button
                  className="bg-[#4977b8] hover:bg-blue-700 text-white px-10 py-2 rounded"
                  onClick={handlePriority}
                >
                  Ưu tiên
                </button>
                <button
                  className="border border-gray-500 hover:bg-blue-400 text-black-900 px-10 py-2 rounded"
                  onClick={handleCancel}
                >
                  Hủy
                </button>
                <button
                  className="bg-[#4977b8] hover:bg-blue-700 text-white px-10 py-2 rounded"
                  onClick={handleEdited}
                >
                  Xác nhận
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EditMedicalRecord;
