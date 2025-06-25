import React from "react";
import { useNavigate } from "react-router-dom";
import type { PatientHistory} from "../../types/staff.types"


const Htr_records = [
    {
        patientId: "BN0001",
        recordId: "BA00001",
        timeIn: "03/06/2025 - 14:30",
        department: "Mắt",
        doctor: "Bs Khanh",
    },
    {
        patientId: "BN0001",
        recordId: "BA00001",
        timeIn: "03/06/2025 - 14:30",
        department: "Mắt",
        doctor: "Bs Khanh",
    },
    {
        patientId: "BN0001",
        recordId: "BA00001",
        timeIn: "03/06/2025 - 14:30",
        department: "Mắt",
        doctor: "Bs Khanh",
    },
    {
        patientId: "BN0001",
        recordId: "BA00001",
        timeIn: "03/06/2025 - 14:30",
        department: "Mắt",
        doctor: "Bs Khanh",
    },
];

const History_records: React.FC = () => {
    const navigate = useNavigate();

    return (
        <div className="min-h-screen text-white">

            {/* Content */}
            <div className="bg-[#D3E2F9] rounded-xl m-10 p-6 relative text-black shadow-md">
                <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md mb-6">
                    <h2 className="text-white text-center font-semibold text-lg">Lịch sử bệnh án</h2>
                </div>

                {/* Filters */}
                <div className="flex justify-around mt-6 mb-4">
                    <button className="bg-[#82A9DC] px-4 py-2 rounded-xl">Nguyễn Văn A</button>
                </div>

                {/* Table Header */}
                <div className="grid grid-cols-5 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl">
                    <div>Mã bệnh nhân</div>
                    <div>Mã bệnh án</div>
                    <div>Mã bác sĩ</div>
                    <div>Bác sĩ phụ trách</div>
                    <div>Thời gian vào</div>
                </div>

                {Htr_records.map((item, index) => (
                    <div
                        key={index}
                        className="grid grid-cols-5 px-4 py-2 bg-[#E6EEF8] text-sm rounded-xl mt-2"
                    >
                        <div>{item.patientId}</div>
                        <div>{item.recordId}</div>
                        <div>{item.department}</div>
                        <div>{item.doctor}</div>
                        <div>{item.timeIn}</div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default History_records;