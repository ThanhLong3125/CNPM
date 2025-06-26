import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { PatientHistory } from "../../types/staff.types";
import { fetchMedicalRecordsByPatientId } from "../../service/staffService";

const History_records: React.FC = () => {
    const navigate = useNavigate();
    const { patientId } = useParams(); 

    const [Htr_records, setHtrRecords] = useState<PatientHistory[]>([]);
    
    const handleClick = (patientId: string) => {
        navigate(`/doctor/OldMedicalRecord/${patientId}`);
    };

    useEffect(() => {
        const fetchData = async () => {
            if (!patientId) return;

            const records = await fetchMedicalRecordsByPatientId(patientId);
            if (records) setHtrRecords(records);
        };

        fetchData();
    }, [patientId]);

    return (
        <div className="min-screen text-white">
            <div className="bg-[#D3E2F9] rounded-xl p-6 relative text-black shadow-md">
                <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md mb-6">
                    <h2 className="text-white text-center font-semibold text-lg">Lịch sử bệnh án</h2>
                </div>

                <div className="grid grid-cols-5 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-10">
                    <div>Mã bệnh nhân</div>
                    <div>Mã bệnh án</div>
                    <div>Mã bác sĩ</div>
                    <div>Bác sĩ phụ trách</div>
                    <div>Thời gian vào</div>
                </div>

                <div className="max-h-[400px] overflow-y-auto space-y-2 bg-[#D3E2F9] p-2">
                    {Htr_records.length === 0 ? (
                        <div className="text-center w-full py-4 text-gray-500">Không có dữ liệu bệnh án</div>
                    ) : (
                        Htr_records.map((item, index) => (
                            <div
                                key={index}
                                onClick={() => handleClick(item.patientId)}
                                className="grid grid-cols-5 px-4 py-2 bg-[#E6EEF8] text-sm rounded-xl mt-2 cursor-pointer hover:bg-[#c9dcf2]"
                            >
                                <div>{item.patientId}</div>
                                <div>{item.medicalRecordId}</div>
                                <div>{item.physicianId}</div>
                                <div>{item.doctorName}</div>
                                <div>{item.createdAt}</div>
                            </div>
                        ))
                    )}
                </div>
            </div>
        </div>
    );
};

export default History_records;
