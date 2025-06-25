import React, { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom";
import type { PatientRecordDeclare } from "../../types/staff.types";
import { useParams } from "react-router-dom";
import { fetchPatientsDetail } from "../../service/staffService";

const PatientRecordView: React.FC = () => {

    const navigate = useNavigate();
    const { patientId } = useParams<{ patientId: string }>();


    const handleCancel = () => navigate(-1);


    const handleUpdate = (patientId: string) => navigate(`/staff/update-patient/${patientId}`);
    const handleViewHistory = (patientId: string) => navigate(`/staff/medical-history/${patientId}`);
    const handleCreateMedicalRecord = (patientId: string) => navigate(`/staff/create-medical-record/${patientId}`);

    const [patient, setPatient] = useState<PatientRecordDeclare | null>(null);


    useEffect(() => {
        const load = async () => {
            const allPatients = await fetchPatientsDetail();
            let found = null;
            let foundIndex = -1;

            for (let i = 0; i < allPatients.length; i++) {
                if (allPatients[i].idPatient === patientId) {
                    found = allPatients[i];
                    break;
                }
            }

            if (found) {
                setPatient({
                    id: found.id,
                    patientID: found.idPatient,
                    fullName: found.fullName ?? 'Không rõ',
                    gender: found.gender?.toLowerCase() === 'male' ? 'Nam' : 'Nữ',
                    dateOfBirth: found.dateOfBirth ? new Date(found.dateOfBirth).toLocaleDateString('vi-VN') : 'Chưa rõ',
                    phone: found.phone ?? '',
                    email: found.email ?? '',
                    medicalHistory: found.medicalHistory ?? '',
                });

            }
        };

        load();
    }, [patientId]);


    return (
        <div className=" flex items-center justify-center  p-4">
            <div className="bg-[#89AFE0] backdrop-blur-sm rounded-3xl py-8 px-20 w-full max-w-5xl shadow-2xl">
                <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
                    <h2 className="text-white text-center font-semibold text-lg">Hồ sơ bệnh nhân</h2>
                </div>


                <div className="space-y-6">
                    <div>
                        <label className="block text-white font-medium mb-2">Mã bệnh nhân</label>
                        <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800">{patient?.patientID}</div>
                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">Họ Tên</label>
                        <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800">{patient?.fullName}</div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                        <div>
                            <label className="block text-white font-medium mb-2">Giới tính</label>
                            <div className="relative">
                                <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800 flex justify-between items-center">
                                    {patient?.gender}
                                </div>
                            </div>
                        </div>

                        <div>
                            <label className="block text-white font-medium mb-2">Ngày sinh</label>
                            <div className="relative">
                                <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800 flex justify-between items-center">
                                    {patient?.dateOfBirth}
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                        <div>
                            <label className=" text-white font-medium mb-2">SĐT</label>
                            <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800">{patient?.phone}</div>
                        </div>
                        <div>
                            <label className=" text-white font-medium mb-2">Email</label>
                            <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800">{patient?.email}</div>
                        </div>
                    </div>

                    <div>
                        <label className="block text-white font-medium mb-2">Bệnh sử</label>
                        <div className="w-full px-4 py-3 rounded-lg bg-white/90 text-gray-800 min-h-[150px]">
                            {patient?.medicalHistory}
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

                            onClick={() => patient && handleUpdate(patient.patientID)}


                            className="py-3 px-4 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
                        >
                            Cập nhật hồ sơ
                        </button>
                        <button
                            type="button"
                            onClick={() => patient && handleViewHistory(patient.patientID)}
                            className="py-3 px-4 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
                        >
                            Lịch sử bệnh án
                        </button>
                        <button
                            type="button"
                            onClick={() => patient && handleCreateMedicalRecord(patient.patientID)}
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