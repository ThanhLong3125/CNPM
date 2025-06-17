import React, { useState } from "react"
import { useNavigate } from "react-router-dom";
import type { PatientRecordDeclare } from "../../types/staff.types";
import { useParams } from "react-router-dom";
const UpdatePatientRecord: React.FC = () => {
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


    const handleUpdate = (patientId: string) => navigate(`/staff/update-patient/${patientId}`);

    return (
        <div className=" flex items-center justify-center p-4">
            <div className="bg-[#89AFE0] backdrop-blur-sm rounded-3xl p-8 w-full max-w-5xl shadow-2xl">
               <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
                <h2 className="text-white text-center font-semibold text-lg">Cập nhật hồ sơ</h2>
            </div>


                <div className="space-y-6">
                    <div>
                        <label className="block text-white font-medium mb-2">Mã bệnh nhân</label>
                        <input type="text" defaultValue="BN0001" className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />

                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">Họ Tên</label>
                        <input type="text" defaultValue="Nguyễn Văn A" className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />

                    </div>

                    <div className="grid grid-cols-2 gap-4">

                        <div>
                            <label className="block font-medium text-sm text-gray-700 mb-1">Giới tính</label>
                            <select
                                defaultValue="Nam"
                                className="w-full px-10 py-2 bg-gray-200 rounded-lg shadow-sm focus:ring-2 focus:ring-blue-300"
                            >
                                <option value="Nam">Nam</option>
                                <option value="Nữ">Nữ</option>
                            </select>
                        </div>


                        <div>
                            <label className="block font-medium text-sm text-gray-700 mb-1">Ngày sinh</label>
                            <input
                                type="date"
                                defaultValue="1980-05-09"
                                className="w-full px-10 py-2 bg-gray-200 rounded-lg shadow-sm focus:ring-2 focus:ring-blue-300"
                            />
                        </div>
                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">SĐT hoặc Email</label>
                        <input type="text" defaultValue="0931490350" className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />

                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">Bệnh sử</label>
                        <textarea
                            rows={5}
                            defaultValue="Tiểu đường"
                            className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded resize-none"
                        />
                    </div>

                                        <div className="flex w-full">
                        <div className="w-1/2">
                        </div>
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
                                className="flex-1 py-3 px-6 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
                            >
                                Xác nhận
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default UpdatePatientRecord;