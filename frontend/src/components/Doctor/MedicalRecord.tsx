import React, { useEffect, useState, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { patient } from "../../service/doctorService";
import {
  fetchWaitingPatients,
  createDiagnosis,
} from "../../service/doctorService";
import {
  fetchMedicalRecordById,
  fetchDoctor,
} from "../../service/staffService";


const MedicalRecord: React.FC = () => {
  const [patientData, setPatientData] = useState<patient | null>(null);
  const [symptoms, setSymptoms] = useState("");
  const [physicianId, setPhysicianId] = useState("");
  const [physicianName, setPhysicianName] = useState("");
  const [diagnosisNote, setDiagnosisNote] = useState("");
  const [selectedImage, setSelectedImage] = useState<string | null>(null);
  const [aiDiagnosis, setAiDiagnosis] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const fileInputRef = useRef<HTMLInputElement | null>(null);
  const previousPatientId = useRef<string | null>(null);

  const navigate = useNavigate();
  const { patient_id } = useParams<{ patient_id: string }>();

  // Hàm định dạng ngày và giờ
  const formatDateTime = (isoDate: string): string => {
    const date = new Date(isoDate);
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();
    const hours = String(date.getHours()).padStart(2, "0");
    const minutes = String(date.getMinutes()).padStart(2, "0");
    return `${day}/${month}/${year} ${hours}:${minutes}`;
  };

  // Xử lý khi file được chọn
  const handleImageUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      const imageUrl = URL.createObjectURL(file);
      setSelectedImage(imageUrl);
    }
  };

  // Cleanup khi component unmount
  useEffect(() => {
    return () => {
      if (selectedImage) {
        URL.revokeObjectURL(selectedImage);
      }
    };
  }, [selectedImage]);

  // Xóa ảnh
  const handleClearImage = () => {
    if (selectedImage) {
      URL.revokeObjectURL(selectedImage);
      setSelectedImage(null);
    }
  };

  const handleClickAdd = () => {
    fileInputRef.current?.click(); // Kích hoạt input file khi nhấn nút "Thêm"
  };

  // Hàm xử lý chuẩn đoán AI
  const handleAIDiagnosis = async () => {
    if (!selectedImage) {
      alert("Vui lòng chọn ảnh trước khi thực hiện chuẩn đoán AI");
      return;
    }

    try {
      // Giả lập API call với delay 2 giây
      setTimeout(() => {
        const mockAIResult = "Dựa trên phân tích hình ảnh, có dấu hiệu của viêm phổi nhẹ. Khuyến nghị khám thêm và xét nghiệm bổ sung.";
        setAiDiagnosis(mockAIResult);
        alert("Chuẩn đoán AI đã hoàn thành!");
      }, 2000);
    } catch (error) {
      console.error("Lỗi khi phân tích AI:", error);
      alert("Có lỗi xảy ra khi phân tích AI. Vui lòng thử lại.");
    }
  };

  // Lấy bệnh nhân cụ thể theo patient_id
  useEffect(() => {
    const loadData = async () => {
      if (!patient_id) {
        setError("Không có ID bệnh nhân");
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const patients = await fetchWaitingPatients();
        const foundPatient = patients.find(p => p.patientId === patient_id);
        
        if (foundPatient) {
          setPatientData(foundPatient);
        } else {
          setPatientData(null);
          setError("Không tìm thấy bệnh nhân");
        }
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu bệnh nhân:", error);
        setError("Không thể tải danh sách bệnh nhân");
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [patient_id]);

  // Reset states khi chuyển bệnh nhân mới
  useEffect(() => {
    // Chỉ reset khi thực sự chuyển sang bệnh nhân mới
    if (previousPatientId.current && 
        previousPatientId.current !== patient_id) {
      
      // Cleanup ảnh cũ khi chuyển bệnh nhân
      if (selectedImage) {
        URL.revokeObjectURL(selectedImage);
      }
      
      // Reset các state liên quan đến ảnh và AI
      setSelectedImage(null);
      setAiDiagnosis("");
      setDiagnosisNote("");
      
      // Reset file input
      if (fileInputRef.current) {
        fileInputRef.current.value = "";
      }
    }
    
    // Cập nhật ID hiện tại
    previousPatientId.current = patient_id || null;
  }, [patient_id, selectedImage]);

  // Lấy thông tin bệnh nhân
  useEffect(() => {
    const loadMedicalRecord = async () => {
      if (patientData?.medicalRecordId) {
        try {
          setLoading(true);
          const recordDetail = await fetchMedicalRecordById(
            patientData.medicalRecordId
          );
          setSymptoms(recordDetail.symptoms || "");
          setPhysicianId(recordDetail.assignedPhysicianId || "");

          const doctors = await fetchDoctor();
          const matchedDoctor = doctors.find(
            (doc) => doc.physicianId === recordDetail.assignedPhysicianId
          );
          setPhysicianName(matchedDoctor?.full_name || "Không rõ");
        } catch (error) {
          console.error("Lỗi khi lấy chi tiết hồ sơ bệnh án:", error);
          setError("Không thể tải chi tiết hồ sơ");
        } finally {
          setLoading(false);
        }
      }
    };

    loadMedicalRecord();
  }, [patientData?.medicalRecordId]);

  const handleCreateDiagnosis = async () => {
    if (!patientData) {
      alert("Không có dữ liệu bệnh nhân để lưu chẩn đoán.");
      return;
    }
    try {
      setLoading(true);
      await createDiagnosis({
        medicalRecordId: patientData.medicalRecordId,
        diagnosedDate: new Date().toISOString(),
        notes: diagnosisNote,
      });
      alert("Đã lưu chẩn đoán thành công!");
      
      // Chuyển về trang danh sách bệnh nhân sau khi lưu thành công
      navigate("/doctor");
      
    } catch (error) {
      alert("Lưu chẩn đoán thất bại!");
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleHistory = (patientId: string) =>
    navigate(`/doctor/medical-history/${patientId}`);
  const handleSave = () => navigate(`/doctor`);

  if (loading) {
    return <div className="text-center mt-10 text-blue-700">Đang tải...</div>;
  }
  if (error || !patientData) {
    return (
      <div className="text-center mt-10 text-red-700">
        {error || "Không có dữ liệu bệnh nhân"}
      </div>
    );
  }

  return (
    <div>
      <div className="grid grid-cols-6 grid-rows-6 gap-10">
        {/* Thông tin bệnh nhân */}
        <div className="col-span-3 row-span-3">
          <div className="space-y-4 bg-[#89AFE0] p-5 pt-0 rounded-md size-full shadow-md relative">
            <div className="absolute -top-5 left-1/3 flex justify-center">
              <h2 className="text-lg font-bold text-white bg-[#618FCA] px-10 py-1 rounded-md inline-block shadow-md">
                Thông tin bệnh nhân
              </h2>
            </div>

            <div className="grid grid-cols-8 grid-rows-5 gap-3 pt-10">
              <div className="col-span-4">
                <div className="text-[#133574] font-semibold">Mã bệnh nhân</div>
              </div>
              <div className="col-span-4 row-start-2">
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2"
                  disabled
                  value={patientData.patientId}
                />
              </div>

              <div className="col-span-4 col-start-5">
                <div className="text-[#133574] font-semibold">Mã bệnh án</div>
              </div>
              <div className="col-span-4 col-start-5 row-start-2">
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 focus:outline-none"
                  disabled
                  value={patientData.medicalRecordId}
                />
              </div>

              <div className="col-span-4 row-start-3">
                {" "}
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 focus:outline-none"
                  disabled
                  value={patientData.fullName}
                />
              </div>

              <div className="col-span-2 col-start-5 row-start-3">
                <div className="text-[#133574] font-semibold">Ngày sinh</div>
              </div>
              <div className="col-span-2 col-start-5 row-start-3">
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 focus:outline-none"
                  disabled
                  value={patientData.dateOfBirth}
                />
              </div>

              <div className="col-span-2 col-start-7 row-start-3">
                <div className="text-[#133574] font-semibold">Giới tính</div>
              </div>
              <div className="col-span-2 col-start-7 row-start-3">
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 focus:outline-none"
                  disabled
                  value={patientData.gender}
                />
              </div>

              <div className="col-span-4 row-start-4">
                <div className="text-[#133574] font-semibold">Triệu chứng</div>
              </div>
              <div className="col-span-4 row-start-5">
                <textarea
                  ref={(el) => {
                    if (el) {
                      el.style.height = "auto";
                      el.style.height = `${el.scrollHeight}px`;
                    }
                  }}
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 resize-none overflow-hidden focus:outline-none"
                  disabled
                  value={symptoms}
                  rows={1}
                />
              </div>

              <div className="col-span-4 col-start-5 row-start-4">
                <div className="text-[#133574] font-semibold">
                  Thời gian vào
                </div>
              </div>
              <div className="col-span-4 col-start-5 row-start-5">
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 focus:outline-none"
                  disabled
                  value={formatDateTime(patientData.createdAt)}
                />
              </div>
            </div>
          </div>
        </div>

        {/* Thông tin ảnh chụp */}
        <div className="col-span-3 row-span-6 col-start-4">
          <div className="space-y-4 bg-[#89AFE0] p-5 pt-0 rounded-md size-full shadow-md relative">
            <div className="absolute -top-5 left-1/3 flex justify-center">
              <h2 className="text-lg font-bold text-white bg-[#618FCA] px-10 py-1 rounded-md inline-block shadow-md">
                Thông tin ảnh chụp
              </h2>
            </div>
            <div className="flex items-center justify-center relative pt-10">
              <div className="grid grid-cols-3 grid-rows-3 gap-4">
                <div className="col-span-4 row-span-3">
                  {selectedImage ? (
                    <div className="relative">
                      <img
                        src={selectedImage}
                        alt="Uploaded image"
                        className="w-65 h-65 bg-white rounded-md flex items-center justify-center relative object-cover"
                        onDoubleClick={() =>
                          window.open(selectedImage, "_blank")
                        }
                      />
                    </div>
                  ) : (
                    <div className="w-65 h-65 bg-white rounded-md flex items-center justify-center relative">
                      <span className="text-gray-500"></span>
                    </div>
                  )}
                </div>
              </div>
            </div>

            {/* Cảnh báo ở hàng riêng */}
            <div className="bg-yellow-100 border-l-4 border-yellow-500 text-yellow-700 p-3 rounded mb-2">
              <p className="text-sm">
                <span className="font-bold">⚠️ Lưu ý:</span> Upload ảnh và phân tích AI chỉ khả dụng sau khi đã tạo chẩn đoán (chuyển sang trạng thái "Đã khám")
              </p>
            </div>

            {/* Buttons ở hàng riêng với kích thước đồng đều */}
            <div className="flex justify-center space-x-3 mt-2">
              <button
                onClick={handleClickAdd}
                className="text-white text-sm font-medium bg-gray-400 cursor-not-allowed px-4 py-2 rounded-lg shadow-md w-24 h-10 flex items-center justify-center"
                disabled={true}
                title="Không thể upload ảnh từ trạng thái 'Chờ khám'"
              >
                Chọn ảnh
              </button>
              <button
                onClick={handleClearImage}
                className="text-white text-sm font-medium bg-gray-400 cursor-not-allowed px-4 py-2 rounded-lg shadow-md w-24 h-10 flex items-center justify-center"
                disabled={true}
                title="Không khả dụng"
              >
                Xóa
              </button>
              <button
                onClick={handleAIDiagnosis}
                className="text-white text-sm font-medium bg-gray-400 cursor-not-allowed px-4 py-2 rounded-lg shadow-md w-28 h-10 flex items-center justify-center"
                disabled={true}
                title="Cần có chẩn đoán trước khi phân tích AI"
              >
                Phân tích AI
              </button>
            </div>
            <textarea
              className="w-full h-20 bg-white rounded-md pl-2 focus:outline-none resize-none"
              placeholder="Chuẩn đoán của AI"
              value={aiDiagnosis}
              onChange={(e) => setAiDiagnosis(e.target.value)}
            />

            <div className="flex justify-end space-x-2 mt-2">
              <button
                className="text-white font-bold bg-[#618FCA] hover:bg-blue-900 px-7 py-2 rounded-xl shadow-md"
                onClick={() => handleHistory(patientData.patientId)}
              >
                Xem lịch sử
              </button>
              <button
                className="text-white font-bold bg-[#618FCA] hover:bg-blue-900 px-7 py-2 rounded-xl shadow-md"
                onClick={handleSave}
              >
                Lưu
              </button>
            </div>
          </div>
        </div>

        {/* Thông tin chẩn đoán */}
        <div className="col-span-3 row-span-3 row-start-4">
          <div className="space-y-4 bg-[#89AFE0] p-5 pt-0 rounded-md size-full shadow-md relative">
            <div className="absolute -top-5 left-1/3 flex justify-center">
              <h2 className="text-lg font-bold text-white bg-[#618FCA] px-10 py-1 rounded-md inline-block shadow-md">
                Thông tin chẩn đoán
              </h2>
            </div>

            <div className="grid grid-cols-2 grid-rows-3 gap-2 pt-10">
              <div>
                <div className="text-[#133574] font-semibold mb-1">
                  Tên bác sĩ
                </div>
              </div>
              <div>
                <div className="text-[#133574] font-semibold mb-1">
                  Mã bác sĩ
                </div>
              </div>
              <div>
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 focus:outline-none"
                  disabled
                  value={physicianName}
                />
              </div>
              <div>
                <input
                  className="w-full bg-[#D5DEEF] rounded-md pl-2 focus:outline-none"
                  disabled
                  value={physicianId}
                />
              </div>
              <div className="col-span-2">
                <div className="text-[#133574] font-semibold">Chẩn đoán</div>
              </div>
            </div>
            <textarea
              className="w-full h-20 bg-white rounded-md pl-2 outline-none resize-none focus:outline-none"
              value={diagnosisNote}
              onChange={(e) => setDiagnosisNote(e.target.value)}
            />
            <div className="flex justify-end space-x-2 mt-2">
              <button
                className="text-white font-bold bg-[#618FCA] hover:bg-blue-900 px-7 py-2 rounded-xl shadow-md"
                onClick={handleCreateDiagnosis}
              >
                Lưu
              </button>
            </div>
          </div>
        </div>
      </div>
      <input
        type="file"
        accept="image/*"
        onChange={handleImageUpload}
        ref={fileInputRef}
        className="hidden"
      />
    </div>
  );
};

export default MedicalRecord;