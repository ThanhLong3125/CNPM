import React, { useEffect, useState, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { patient } from "../../service/doctorService";

// Interface for API errors with response
interface ApiError {
  response?: {
    status: number;
    data?: unknown;
  };
  message?: string;
}

// Interface for diagnosis data
interface DiagnosisData {
  diagnosisId: string;
  notes: string;
  diagnosedDate: string;
  isDeleted?: boolean;
}
import {
  fetchTreatedPatients,
  updateDiagnosis,
  fetchDiagnosisByMedicalRecordId,
  type DiagnosisDetailsDto,
} from "../../service/doctorService";
import {
  fetchMedicalRecordById,
  fetchDoctor,
} from "../../service/staffService";
import { analyzeImage, uploadImage, fetchImageById, getImageDownloadUrl } from "../../service/imageService";

const OldPatientRecord: React.FC = () => {
  const [patientData, setPatientData] = useState<patient | null>(null);
  const [symptoms, setSymptoms] = useState("");
  const [physicianId, setPhysicianId] = useState("");
  const [physicianName, setPhysicianName] = useState("");
  const [diagnosisNote, setDiagnosisNote] = useState("");
  const [existingDiagnosisId, setExistingDiagnosisId] = useState<string | null>(null);
  const [selectedImage, setSelectedImage] = useState<string | null>(null);
  const [uploadedImageId, setUploadedImageId] = useState<string | null>(null);
  const [aiDiagnosis, setAiDiagnosis] = useState<string>("");
  const [originalAiDiagnosis, setOriginalAiDiagnosis] = useState<string>("");
  const [aiResultSaved, setAiResultSaved] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [aiLoading, setAiLoading] = useState<boolean>(false);
  const [uploadLoading, setUploadLoading] = useState<boolean>(false);
  const [saveLoading, setSaveLoading] = useState<boolean>(false);
  const [deleteLoading, setDeleteLoading] = useState<boolean>(false);

  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [redirecting, setRedirecting] = useState<boolean>(false);
  const [showAIModal, setShowAIModal] = useState<boolean>(false);
  const [showHistoryModal, setShowHistoryModal] = useState<boolean>(false);
  const [diagnosisDetails, setDiagnosisDetails] = useState<DiagnosisDetailsDto | null>(null);
  const fileInputRef = useRef<HTMLInputElement | null>(null);


  const navigate = useNavigate();
  const { patientId } = useParams<{ patientId: string }>();

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

  // Xử lý khi file được chọn - auto upload
  const handleImageUpload = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      const imageUrl = URL.createObjectURL(file);
      setSelectedImage(imageUrl);

      // Auto upload sau khi chọn file
      if (existingDiagnosisId) {
        try {
          setUploadLoading(true);
          const imageName = `${patientData?.fullName}_${new Date().getTime()}`;
          
          console.log("Uploading với DiagnosisId:", existingDiagnosisId); // Debug log
          
          const result = await uploadImage(existingDiagnosisId, file, imageName);
          setUploadedImageId(result.imageId);
          
          // Load thông tin ảnh để lấy aiAnalysis đã có (nếu có)
          try {
            const imageInfo = await fetchImageById(result.imageId);
            if (imageInfo.aiAnalysis) {
              setAiDiagnosis(imageInfo.aiAnalysis);
              setOriginalAiDiagnosis(imageInfo.aiAnalysis);
              setAiResultSaved(true);
            }
          } catch {
            console.log("Chưa có kết quả AI cho ảnh này");
          }
          
          alert("Upload ảnh thành công!");
        } catch (error: unknown) {
          console.error("Lỗi khi upload ảnh:", error);
          console.error("DiagnosisId hiện tại:", existingDiagnosisId); // Debug log
          if ((error as ApiError).response?.status === 404) {
            alert(`Không tìm thấy chẩn đoán với ID: ${existingDiagnosisId}. Vui lòng kiểm tra lại.`);
          } else {
            alert("Có lỗi xảy ra khi upload ảnh. Vui lòng thử lại.");
          }
        } finally {
          setUploadLoading(false);
        }
      } else {
        alert("Chưa có chẩn đoán để upload ảnh. Vui lòng tải lại trang để cập nhật thông tin.");
      }
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
    setUploadedImageId(null);
    setAiDiagnosis("");
    setOriginalAiDiagnosis("");
    setAiResultSaved(false);
    if (fileInputRef.current) {
      fileInputRef.current.value = "";
    }
  };

  const handleClickAdd = () => {
    fileInputRef.current?.click();
  };



  // Hàm xử lý chuẩn đoán AI - REAL API
  const handleAIDiagnosis = async () => {
    if (!uploadedImageId) {
      alert("Vui lòng upload ảnh trước khi thực hiện chuẩn đoán AI");
      return;
    }

    try {
      setAiLoading(true);
      const result = await analyzeImage(uploadedImageId);
      setAiDiagnosis(result.aiAnalysis);
      setOriginalAiDiagnosis(result.aiAnalysis);
      setAiResultSaved(true); // Kết quả AI đã được lưu tự động bởi backend
      alert("Chuẩn đoán AI đã hoàn thành và lưu vào database!");
    } catch (error: unknown) {
      console.error("Lỗi khi phân tích AI:", error);
      if ((error as ApiError).response?.status === 404) {
        alert("Không tìm thấy ảnh. Vui lòng upload lại.");
      } else if ((error as ApiError).response?.status === 503) {
        alert("Dịch vụ AI tạm thời không khả dụng. Vui lòng thử lại sau.");
      } else {
      alert("Có lỗi xảy ra khi phân tích AI. Vui lòng thử lại.");
      }
    } finally {
      setAiLoading(false);
    }
  };

  // Lưu kết quả AI đã chỉnh sửa
  const handleSaveAIResult = async () => {
    if (!uploadedImageId || !aiDiagnosis.trim()) {
      alert("Không có kết quả AI để lưu");
      return;
    }

    try {
      setSaveLoading(true);
      // Call API để update aiAnalysis (backend tự động lưu khi analyze, 
      // nhưng cần API để update khi user edit)
      // Tạm thời giả lập vì chưa có API update AI result
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      setOriginalAiDiagnosis(aiDiagnosis);
      setAiResultSaved(true);
      alert("Đã lưu kết quả AI thành công!");
    } catch (error: unknown) {
      console.error("Lỗi khi lưu kết quả AI:", error);
      alert("Có lỗi xảy ra khi lưu kết quả AI. Vui lòng thử lại.");
    } finally {
      setSaveLoading(false);
    }
  };

  // Lấy bệnh nhân đã khám theo patientId
  useEffect(() => {
    const loadData = async () => {
      if (!patientId) {
        setError("Không có ID bệnh nhân");
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const treatedPatients = await fetchTreatedPatients();
        const foundPatient = treatedPatients.find(p => p.patientId === patientId);
        
        if (foundPatient) {
          setPatientData(foundPatient);
        } else {
          // Bệnh nhân không còn trong danh sách "Đã khám" -> có thể đã được chuyển về "Chờ khám"
          console.log(`Bệnh nhân ${patientId} không còn trong danh sách "Đã khám"`);
          
          // Hiển thị thông báo và chuyển về trang chính
          setRedirecting(true);
          alert(
            `⚠️ BỆNH NHÂN KHÔNG CÒN TRONG DANH SÁCH "ĐÃ KHÁM"\n\n` +
            `Bệnh nhân ${patientId} có thể đã được chuyển về trạng thái "Chờ khám" \n` +
            `do chẩn đoán bị xóa.\n\n` +
            `Bạn sẽ được chuyển về trang danh sách bệnh nhân.`
          );
          
          setTimeout(() => navigate("/doctor"), 500);
          return;
        }
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu bệnh nhân:", error);
        setError("Không thể tải danh sách bệnh nhân");
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [patientId, navigate]);

  // Lấy thông tin bệnh nhân và diagnosis
  useEffect(() => {
    const loadMedicalRecord = async () => {
      if (patientData?.medicalRecordId) {
        try {
          setLoading(true);
          const recordDetail = await fetchMedicalRecordById(patientData.medicalRecordId);
          setSymptoms(recordDetail.symptoms || "");
          setPhysicianId(recordDetail.assignedPhysicianId || "");

          const doctors = await fetchDoctor();
          const matchedDoctor = doctors.find(
            (doc) => doc.physicianId === recordDetail.assignedPhysicianId
          );
          setPhysicianName(matchedDoctor?.full_name || "Không rõ");

          // Lấy diagnosis đã có
          try {
            const existingDiagnosis = await fetchDiagnosisByMedicalRecordId(patientData.medicalRecordId);
            console.log("All diagnosis for medical record:", existingDiagnosis); // Debug log
            
            if (existingDiagnosis && existingDiagnosis.length > 0) {
              // Lọc ra các diagnosis chưa bị xóa (nếu có trường isDeleted)
              // Nếu không có field isDeleted, sử dụng tất cả diagnosis
              const validDiagnosis = existingDiagnosis.filter((d: Record<string, any>) => {
                // Nếu có field isDeleted, chỉ lấy những diagnosis chưa bị xóa
                if (Object.prototype.hasOwnProperty.call(d, 'isDeleted')) {
                  return !d.isDeleted;
                }
                // Nếu không có field isDeleted, lấy tất cả
                return true;
              });
              console.log("Valid diagnosis after filtering:", validDiagnosis); // Debug log
              
              // Nếu không có diagnosis hợp lệ
              if (validDiagnosis.length === 0) {
                console.log("Tất cả diagnosis đã bị soft delete");
                setRedirecting(true);
                alert(
                  `⚠️ TẤT CẢ CHẨN ĐOÁN ĐÃ BỊ XÓA\n\n` +
                  `Bệnh nhân này không có chẩn đoán hợp lệ.\n` +
                  `Tất cả chẩn đoán đã bị xóa và bệnh nhân đã được chuyển về "Chờ khám".\n\n` +
                  `Bạn sẽ được chuyển về trang danh sách bệnh nhân.`
                );
                setTimeout(() => navigate("/doctor"), 500);
                return;
              }
              
              // Sắp xếp theo thời gian tạo giảm dần để lấy diagnosis mới nhất
              const sortedDiagnosis = validDiagnosis.sort((a: DiagnosisData, b: DiagnosisData) => 
                new Date(b.diagnosedDate).getTime() - new Date(a.diagnosedDate).getTime()
              );
              
              const latestDiagnosis = sortedDiagnosis[0];
              setDiagnosisNote(latestDiagnosis.notes || "");
              setExistingDiagnosisId(latestDiagnosis.diagnosisId);
              console.log("Latest diagnosis loaded:", latestDiagnosis.diagnosisId, "created at:", latestDiagnosis.diagnosedDate); // Debug log
              
            } else {
              console.log("Không tìm thấy diagnosis cho medical record:", patientData.medicalRecordId);
              // Không có diagnosis hợp lệ -> bệnh nhân có thể đã được chuyển về "Chờ khám"
              setRedirecting(true);
              alert(
                `⚠️ KHÔNG TÌM THẤY CHẨN ĐOÁN HỢP LỆ\n\n` +
                `Bệnh nhân này không có chẩn đoán hợp lệ.\n` +
                `Có thể chẩn đoán đã bị xóa và bệnh nhân đã được chuyển về "Chờ khám".\n\n` +
                `Bạn sẽ được chuyển về trang danh sách bệnh nhân.`
              );
              setTimeout(() => navigate("/doctor"), 500);
              return;
            }
          } catch (diagnosisError: unknown) {
            console.error("Lỗi khi lấy diagnosis:", diagnosisError);
            if ((diagnosisError as ApiError).response?.status === 404) {
              setRedirecting(true);
              alert(
                `⚠️ CHẨN ĐOÁN ĐÃ KHÔNG CÒN TỒN TẠI\n\n` +
                `Chẩn đoán của bệnh nhân này đã bị xóa.\n` +
                `Bệnh nhân có thể đã được chuyển về trạng thái "Chờ khám".\n\n` +
                `Bạn sẽ được chuyển về trang danh sách bệnh nhân.`
              );
              setTimeout(() => navigate("/doctor"), 500);
              return;
            }
          }
        } catch (error) {
          console.error("Lỗi khi lấy chi tiết hồ sơ bệnh án:", error);
          setError("Không thể tải chi tiết hồ sơ");
        } finally {
          setLoading(false);
        }
      }
    };

    loadMedicalRecord();
  }, [patientData?.medicalRecordId, navigate]);

  const handleUpdateDiagnosis = async () => {
    if (!patientData || !existingDiagnosisId) {
      alert("Không có dữ liệu diagnosis để cập nhật.");
      return;
    }
    try {
      setLoading(true);
      await updateDiagnosis(existingDiagnosisId, {
        notes: diagnosisNote,
      });
      alert("Đã cập nhật chẩn đoán thành công!");
      setIsEditing(false);
    } catch (error) {
      alert("Cập nhật chẩn đoán thất bại!");
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  

  

  const handleCancel = () => navigate(`/doctor`);
  const handleEdit = () => setIsEditing(true);
  const handleCancelEdit = () => setIsEditing(false);

  if (redirecting) {
    return (
      <div className="text-center mt-10">
        <div className="text-orange-600 text-lg font-semibold mb-2">
          🔄 Đang chuyển hướng...
        </div>
        <div className="text-gray-600">
          Bệnh nhân không còn trong danh sách "Đã khám". Đang chuyển về trang chính...
        </div>
      </div>
    );
  }

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

              <div className="flex justify-start space-x-2 mt-2">
                <button
                  onClick={handleClickAdd}
                  className={`text-white font-bold px-7 py-2 rounded-xl shadow-md ${
                    !existingDiagnosisId || uploadLoading
                      ? "bg-gray-400 cursor-not-allowed"
                      : "bg-[#618FCA] hover:bg-blue-900"
                  }`}
                  disabled={!existingDiagnosisId || uploadLoading}
                  title={
                    !existingDiagnosisId
                      ? "Không tìm thấy chẩn đoán để upload ảnh"
                      : uploadedImageId && !aiResultSaved
                      ? "Vui lòng lưu kết quả AI trước khi thêm ảnh mới"
                      : ""
                  }
                >
                  {uploadLoading ? "Đang upload..." : (uploadedImageId ? "Thêm ảnh mới" : "Chọn ảnh")}
                </button>
                <button
                  onClick={handleClearImage}
                  className="text-white font-bold bg-[#dc3545] hover:bg-red-700 px-7 py-2 rounded-xl shadow-md"
                  disabled={!selectedImage}
                >
                  Xóa
                </button>
                <button
                  onClick={handleAIDiagnosis}
                  className="text-white font-bold bg-[#28a745] hover:bg-green-700 px-7 py-2 rounded-xl shadow-md"
                  disabled={!uploadedImageId || aiLoading}
                >
                  {aiLoading ? "AI đang phân tích..." : "Phân tích AI"}
                </button>
              </div>

                          {/* Debug info cho diagnosis */}
            {!existingDiagnosisId && (
              <div className="bg-red-100 border-l-4 border-red-500 text-red-700 p-3 rounded mt-2">
                <p className="text-sm">
                  ❌ <strong>Lỗi:</strong> Không tìm thấy chẩn đoán cho bệnh nhân này. 
                  <button 
                    onClick={() => window.location.reload()} 
                    className="ml-2 underline hover:no-underline"
                  >
                    Tải lại trang
                  </button>
                </p>
              </div>
            )}

            {/* Cảnh báo workflow */}
            {uploadedImageId && !aiResultSaved && aiDiagnosis && (
              <div className="bg-yellow-100 border-l-4 border-yellow-500 text-yellow-700 p-3 rounded mt-2">
                <p className="text-sm">
                  ⚠️ <strong>Lưu ý:</strong> Bạn cần lưu kết quả AI trước khi thêm ảnh mới cho chẩn đoán này.
                </p>
              </div>
            )}
              <div className="relative bg-gradient-to-r from-blue-50 to-green-50 p-3 rounded-lg border-l-4 border-blue-500">
                <div className="text-[#133574] font-semibold mb-2 flex justify-between items-center">
                  <span className="flex items-center gap-2">
                    🤖 Kết quả phân tích AI
                    {aiLoading && <span className="text-sm text-blue-600">(đang phân tích...)</span>}
                    {aiResultSaved && <span className="text-sm text-green-600">✅ Đã lưu</span>}
                    {aiDiagnosis && !aiResultSaved && <span className="text-sm text-orange-600">⚠️ Chưa lưu</span>}
                  </span>
                  <div className="flex gap-2">
                    {aiDiagnosis && aiDiagnosis !== originalAiDiagnosis && (
                      <button
                        onClick={handleSaveAIResult}
                        className="text-sm bg-green-500 hover:bg-green-600 text-white px-3 py-1 rounded-md transition-colors"
                        disabled={saveLoading}
                        title="Lưu kết quả AI đã chỉnh sửa vào database"
                      >
                        {saveLoading ? "Đang lưu..." : "💾 Lưu kết quả"}
                      </button>
                    )}
                    {aiDiagnosis && (
                      <button
                        onClick={() => setShowAIModal(true)}
                        className="text-sm bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 rounded-md transition-colors"
                        title="Xem kết quả AI toàn màn hình với các tùy chọn copy và in"
                      >
                        🔍 Xem chi tiết
                      </button>
                    )}
                  </div>
                </div>
              <textarea
                  className="w-full bg-white rounded-md p-3 focus:outline-none border border-gray-300 text-sm leading-relaxed overflow-y-auto"
                  placeholder="Kết quả phân tích AI sẽ hiển thị ở đây..."
                value={aiDiagnosis}
                  onChange={(e) => {
                    setAiDiagnosis(e.target.value);
                    // Reset saved status khi user edit
                    if (e.target.value !== originalAiDiagnosis) {
                      setAiResultSaved(false);
                    }
                  }}
                  style={{ 
                    minHeight: '128px', 
                    maxHeight: '250px',
                    height: aiDiagnosis ? Math.min(Math.max(128, aiDiagnosis.split('\n').length * 20 + 40), 250) + 'px' : '128px'
                  }}
                />
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
                className={`w-full h-20 rounded-md pl-2 outline-none resize-none focus:outline-none ${
                  isEditing ? 'bg-white' : 'bg-[#D5DEEF]'
                }`}
                disabled={!isEditing}
                value={diagnosisNote}
                onChange={(e) => setDiagnosisNote(e.target.value)}
                placeholder="Nhập chẩn đoán..."
              />
              
              <div className="flex justify-end space-x-2 mt-2">
                {!isEditing ? (
                  <>
                    <button
                      className="text-white font-bold bg-[#618FCA] hover:bg-blue-900 px-7 py-2 rounded-xl shadow-md"
                      onClick={handleEdit}
                    >
                      Chỉnh sửa
                    </button>
                    
                  </>
                ) : (
                  <>
                    <button
                      className="text-white font-bold bg-[#6c757d] hover:bg-gray-600 px-7 py-2 rounded-xl shadow-md"
                      onClick={handleCancelEdit}
                    >
                      Hủy
                    </button>
                    <button
                      className="text-white font-bold bg-[#618FCA] hover:bg-blue-700 px-7 py-2 rounded-xl shadow-md"
                      onClick={handleUpdateDiagnosis}
                      disabled={!existingDiagnosisId}
                    >
                      Lưu
                    </button>
                  </>
                )}
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

        {/* Modal hiển thị kết quả AI toàn màn hình */}
        {showAIModal && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-lg max-w-4xl w-full max-h-[90vh] flex flex-col">
              <div className="flex justify-between items-center p-4 border-b">
                <h3 className="text-xl font-bold text-[#133574]">🤖 Kết quả phân tích AI chi tiết</h3>
                <button
                  onClick={() => setShowAIModal(false)}
                  className="text-gray-500 hover:text-gray-700 text-2xl font-bold"
                >
                  ×
                </button>
              </div>
              <div className="flex-1 p-4 overflow-y-auto">
                <div className="bg-gray-50 p-4 rounded-lg border-l-4 border-blue-500">
                  <div className="text-sm text-gray-600 mb-2">
                    📋 Bệnh nhân: <strong>{patientData?.fullName}</strong> | 
                    📅 Thời gian: <strong>{new Date().toLocaleString('vi-VN')}</strong>
                    {aiResultSaved && <span className="ml-2 text-green-600">✅ Đã lưu vào database</span>}
                    {!aiResultSaved && aiDiagnosis && <span className="ml-2 text-orange-600">⚠️ Chưa lưu</span>}
                  </div>
                  <div className="text-base leading-relaxed whitespace-pre-wrap text-gray-800">
                    {aiDiagnosis || "Chưa có kết quả phân tích AI"}
                  </div>
                </div>
              </div>
              <div className="flex justify-end gap-3 p-4 border-t">
                {!aiResultSaved && aiDiagnosis && (
                  <button
                    onClick={async () => {
                      await handleSaveAIResult();
                      // Modal sẽ tự update status sau khi lưu
                    }}
                    className="bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-md"
                    disabled={saveLoading}
                  >
                    {saveLoading ? "Đang lưu..." : "💾 Lưu vào database"}
                  </button>
                )}
                <button
                  onClick={() => {
                    navigator.clipboard.writeText(aiDiagnosis);
                    alert("Đã copy kết quả vào clipboard!");
                  }}
                  className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-md"
                >
                  📋 Copy kết quả
                </button>
                <button
                  onClick={() => {
                    const printContent = `
                      <h2>Kết quả phân tích AI</h2>
                      <p><strong>Bệnh nhân:</strong> ${patientData?.fullName}</p>
                      <p><strong>Thời gian:</strong> ${new Date().toLocaleString('vi-VN')}</p>
                      <hr>
                      <div style="white-space: pre-wrap; line-height: 1.6;">${aiDiagnosis}</div>
                    `;
                    const printWindow = window.open('', '_blank');
                    if (printWindow) {
                      printWindow.document.write(`
                        <html>
                          <head><title>Kết quả AI - ${patientData?.fullName}</title></head>
                          <body style="font-family: Arial, sans-serif; padding: 20px;">
                            ${printContent}
                          </body>
                        </html>
                      `);
                      printWindow.document.close();
                      printWindow.print();
                    }
                  }}
                  className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-md"
                >
                  🖨️ In kết quả
                </button>
                <button
                  onClick={() => setShowAIModal(false)}
                  className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-md"
                >
                  Đóng
                </button>
              </div>
            </div>
          </div>
        )}

        {/* Modal chi tiết chẩn đoán đầy đủ */}
        {showHistoryModal && diagnosisDetails && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-lg max-w-6xl w-full max-h-[95vh] flex flex-col">
              <div className="flex justify-between items-center p-4 border-b bg-gradient-to-r from-blue-600 to-green-600">
                <h3 className="text-xl font-bold text-white">
                  📋 Chi tiết chẩn đoán đầy đủ - {diagnosisDetails.patient.fullName}
                </h3>
                <button
                  onClick={() => setShowHistoryModal(false)}
                  className="text-white hover:text-gray-200 text-2xl font-bold"
                >
                  ×
                </button>
              </div>
              
              <div className="flex-1 p-6 overflow-y-auto">
                {/* Thông tin bệnh nhân */}
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
                  <div className="bg-blue-50 p-4 rounded-lg border-l-4 border-blue-500">
                    <h4 className="text-lg font-bold text-blue-800 mb-3">👤 Thông tin bệnh nhân</h4>
                    <div className="space-y-2 text-sm">
                      <p><strong>Mã BN:</strong> {diagnosisDetails.patient.patientID}</p>
                      <p><strong>Họ tên:</strong> {diagnosisDetails.patient.fullName}</p>
                      <p><strong>Ngày sinh:</strong> {new Date(diagnosisDetails.patient.dateOfBirth).toLocaleDateString('vi-VN')}</p>
                      <p><strong>Giới tính:</strong> {diagnosisDetails.patient.gender}</p>
                      <p><strong>SĐT:</strong> {diagnosisDetails.patient.phone}</p>
                    </div>
                  </div>
                  
                  <div className="bg-green-50 p-4 rounded-lg border-l-4 border-green-500">
                    <h4 className="text-lg font-bold text-green-800 mb-3">🩺 Thông tin chẩn đoán</h4>
                    <div className="space-y-2 text-sm">
                      <p><strong>Mã chẩn đoán:</strong> {diagnosisDetails.diagnosisId}</p>
                      <p><strong>Ngày chẩn đoán:</strong> {new Date(diagnosisDetails.diagnosedDate).toLocaleDateString('vi-VN')}</p>
                      <p><strong>Triệu chứng:</strong> {diagnosisDetails.symptoms}</p>
                      <p><strong>Ghi chú:</strong> {diagnosisDetails.notes}</p>
                    </div>
                  </div>
                </div>

                {/* Danh sách ảnh và AI analysis */}
                <div className="bg-gray-50 p-4 rounded-lg">
                  <h4 className="text-lg font-bold text-gray-800 mb-4">
                    🖼️ Hình ảnh và kết quả AI ({diagnosisDetails.images.length} ảnh)
                  </h4>
                  
                  {diagnosisDetails.images.length === 0 ? (
                    <div className="text-center py-8 text-gray-500">
                      <p>Chưa có ảnh nào được upload cho chẩn đoán này</p>
                    </div>
                  ) : (
                    <div className="space-y-6">
                      {diagnosisDetails.images.map((image) => (
                        <div key={image.imageId} className="bg-white p-4 rounded-lg border border-gray-200">
                          <div className="grid grid-cols-1 lg:grid-cols-3 gap-4">
                            {/* Ảnh */}
                            <div className="lg:col-span-1">
                              <div className="aspect-square bg-gray-100 rounded-lg overflow-hidden">
                                <img
                                  src={`http://localhost:5285/images/${image.path}`}
                                  alt={image.imageName}
                                  className="w-full h-full object-cover cursor-pointer hover:opacity-80"
                                  onClick={() => window.open(`http://localhost:5285/images/${image.path}`, '_blank')}
                                  onError={(e) => {
                                    // Fallback khi không load được ảnh
                                    const target = e.target as HTMLImageElement;
                                    target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMTAwJSIgaGVpZ2h0PSIxMDAlIiBmaWxsPSIjZGRkIi8+PHRleHQgeD0iNTAlIiB5PSI1MCUiIGZvbnQtZmFtaWx5PSJBcmlhbCIgZm9udC1zaXplPSIxNCIgZmlsbD0iIzk5OSIgdGV4dC1hbmNob3I9Im1pZGRsZSIgZHk9Ii4zZW0iPktow7RuZyB0aOG7gyB0YWkgaMOsbmg8L3RleHQ+PC9zdmc+';
                                    target.style.backgroundColor = '#f3f4f6';
                                    console.error('Không thể tải ảnh với path:', image.path);
                                  }}
                                />
                              </div>
                              <div className="mt-2 text-sm text-gray-600">
                                <p><strong>Tên:</strong> {image.imageName}</p>
                                <p><strong>Upload:</strong> {new Date(image.uploadDate).toLocaleString('vi-VN')}</p>
                                <p><strong>ID:</strong> {image.imageId}</p>
                                <p><strong>Path:</strong> {image.path}</p>
                              </div>
                            </div>
                            
                            {/* Kết quả AI */}
                            <div className="lg:col-span-2">
                              <div className="flex items-center gap-2 mb-2">
                                <h5 className="font-bold text-gray-800">🤖 Kết quả phân tích AI:</h5>
                                {image.hasAIAnalysis ? (
                                  <span className="text-green-600 text-sm">✅ Có kết quả</span>
                                ) : (
                                  <span className="text-orange-600 text-sm">⚠️ Chưa phân tích</span>
                                )}
                              </div>
                              <div className="bg-gray-50 p-3 rounded border-l-4 border-blue-400">
                                <div className="text-sm leading-relaxed whitespace-pre-wrap text-gray-800">
                                  {image.aiAnalysis || "Chưa có kết quả phân tích AI"}
                                </div>
                              </div>
                              
                              {/* Action buttons cho từng ảnh */}
                              <div className="flex gap-2 mt-3">
                                <button
                                  onClick={() => {
                                    navigator.clipboard.writeText(image.aiAnalysis);
                                    alert("Đã copy kết quả AI!");
                                  }}
                                  className="text-xs bg-green-500 hover:bg-green-600 text-white px-3 py-1 rounded"
                                  disabled={!image.aiAnalysis}
                                >
                                  📋 Copy AI
                                </button>
                                <button
                                  onClick={() => window.open(getImageDownloadUrl(image.imageId), '_blank')}
                                  className="text-xs bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 rounded"
                                >
                                  🔍 Xem ảnh
                                </button>
                              </div>
                            </div>
                          </div>
                        </div>
                      ))}
                    </div>
                  )}
                </div>
              </div>
              
              <div className="flex justify-between p-4 border-t bg-gray-50">
                {/* Nút xóa ở bên trái - Cảnh báo */}
                <button
                  onClick={() => {
                    // Đóng modal trước khi xóa để tránh lỗi
                    setShowHistoryModal(false);
                    // Thực hiện xóa
                  }}
                  className="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-md font-bold border-2 border-red-800 disabled:opacity-50 disabled:cursor-not-allowed"
                  disabled={deleteLoading}
                  title="Xóa chẩn đoán này và chuyển bệnh nhân về trạng thái 'Chờ khám'"
                >
                  {deleteLoading ? "Đang xóa..." : "🗑️ Xóa chẩn đoán này"}
                </button>
                
                {/* Các nút khác ở bên phải */}
                <div className="flex gap-3">
                  <button
                    onClick={() => {
                      const printContent = `
                        <h2>Chi tiết chẩn đoán - ${diagnosisDetails.patient.fullName}</h2>
                        <p><strong>Mã BN:</strong> ${diagnosisDetails.patient.patientID}</p>
                        <p><strong>Ngày chẩn đoán:</strong> ${new Date(diagnosisDetails.diagnosedDate).toLocaleDateString('vi-VN')}</p>
                        <p><strong>Triệu chứng:</strong> ${diagnosisDetails.symptoms}</p>
                        <p><strong>Chẩn đoán:</strong> ${diagnosisDetails.notes}</p>
                        <hr>
                        <h3>Kết quả AI:</h3>
                        ${diagnosisDetails.images.map(img => `
                          <div style="margin-bottom: 20px;">
                            <h4>${img.imageName}</h4>
                            <div style="white-space: pre-wrap; border-left: 4px solid #3B82F6; padding-left: 10px;">${img.aiAnalysis}</div>
                          </div>
                        `).join('')}
                      `;
                      const printWindow = window.open('', '_blank');
                      if (printWindow) {
                        printWindow.document.write(`
                          <html>
                            <head><title>Chi tiết chẩn đoán - ${diagnosisDetails.patient.fullName}</title></head>
                            <body style="font-family: Arial, sans-serif; padding: 20px; line-height: 1.6;">
                              ${printContent}
                            </body>
                          </html>
                        `);
                        printWindow.document.close();
                        printWindow.print();
                      }
                    }}
                    className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-md"
                  >
                    🖨️ In báo cáo
                  </button>
                  <button
                    onClick={() => setShowHistoryModal(false)}
                    className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-md"
                  >
                    Đóng
                  </button>
                </div>
              </div>
            </div>
          </div>
        )}
      </div>
    );
}   
export default OldPatientRecord;

