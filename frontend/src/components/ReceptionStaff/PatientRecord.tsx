import React, { useState } from "react"
import { useNavigate } from "react-router-dom";
import type { PatientRecordDeclare } from "../../types/staff.types";
import { useParams } from "react-router-dom";
const PatientRecordView: React.FC = () => {
    const [patientData] = useState<PatientRecordDeclare>({
        patientID: "BN0001",
        fullName: "Nguyễn Văn A",
        gender: "Nam",
        birthDate: "09/05/1980",
        phoneOrEmail: "0931490350",
        medicalHistory: "Tiểu đường",
    })

    const navigate = useNavigate();
    const { patientId } = useParams<{ patientId: string }>();


    const handleCancel = () => navigate(-1);


    const handleUpdate = (patientId: string) => navigate(`/update-patient/${patientId}`);
    const handleViewHistory = (patientId: string) => navigate(`/medical-history/${patientId}`);
    const handleCreateMedicalRecord = (patientId: string) => navigate(`/create-medical-record/${patientId}`);

    return (
        <div className=" flex items-center justify-center  p-4">
            <div className="bg-[#89AFE0] backdrop-blur-sm rounded-3xl py-8 px-20 w-full max-w-5xl shadow-2xl">
                <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
                    <h2 className="text-white text-center font-semibold text-lg">Hồ sơ bệnh nhân</h2>
                </div>


                <div className="space-y-6">
                    <div>
                        <label className="block text-white font-medium mb-2">Mã bệnh nhân</label>
                        <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800">{patientData.patientID}</div>
                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">Họ Tên</label>
                        <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800">{patientData.fullName}</div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                        <div>
                            <label className="block text-white font-medium mb-2">Giới tính</label>
                            <div className="relative">
                                <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800 flex justify-between items-center">
                                    {patientData.gender}
                                </div>
                            </div>
                        </div>

                        <div>
                            <label className="block text-white font-medium mb-2">Ngày sinh</label>
                            <div className="relative">
                                <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800 flex justify-between items-center">
                                    {patientData.birthDate}
                                </div>
                            </div>
                        </div>
                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">SĐT hoặc Email</label>
                        <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800">{patientData.phoneOrEmail}</div>
                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">Bệnh sử</label>
                        <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800 min-h-[150px]">
                            {patientData.medicalHistory}
                        </div>
                    </div>

                    <div className="grid grid-cols-4 gap-4 pt-4">
                        <button
                            type="button"
                            onClick={handleCancel}
                            className="py-3 px-4 rounded-lg bg-white/20 text-white font-medium hover:bg-white/30 focus:outline-none focus:ring-2 focus:ring-white/50 transition-all duration-200 border border-white/30"
                        >
                            Hủy
                        </button>
                        <button
                            type="button"

                            onClick={() => handleUpdate(patientData.patientID)}

                            className="py-3 px-4 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
                        >
                            Cập nhật hồ sơ
                        </button>
                        <button
                            type="button"
                            onClick={() => handleViewHistory(patientData.patientID)}
                            className="py-3 px-4 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
                        >
                            Lịch sử bệnh án
                        </button>
                        <button
                            type="button"
                            onClick={() => handleCreateMedicalRecord(patientData.patientID)}
                            className="py-3 px-4 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
                        >
                            Tạo bệnh án
                        </button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default PatientRecordView;