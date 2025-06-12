import React from "react";

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
    return (
        <div className="min-h-screen bg-gray-900 text-white">

            {/* Content */}
            <div className="bg-[#D3E2F9] rounded-xl m-10 p-6 text-black shadow-md">
                <div className="flex justify-center">
                    <h2 className="bg-[#618FCA] text-white px-4 py-2 rounded-xl text-xl font-semibold">
                        Lịch sử bệnh án
                    </h2>
                </div>

                {/* Filters */}
                <div className="flex justify-around mt-6 mb-4">
                    <button className="bg-[#82A9DC] px-4 py-2 rounded-xl">Nguyễn Văn A</button>
                    <button className="bg-[#82A9DC] px-4 py-2 rounded-xl">Bác sĩ phụ trách</button>
                    <button className="bg-[#82A9DC] px-4 py-2 rounded-xl">20/05/2025 - 01/06/2005</button>
                    <button className="bg-[#82A9DC] px-4 py-2 rounded-xl">05:49 - 11:00</button>
                </div>

                {/* Table Header */}
                <div className="grid grid-cols-5 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl">
                    <div>Mã bệnh nhân</div>
                    <div>Mã bệnh án</div>
                    <div>Thời gian vào</div>
                    <div>Khoa khám</div>
                    <div>Bác sĩ phụ trách</div>
                </div>

                {/* Table Rows */}
                {Htr_records.map((item, index) => (
                    <div
                        key={index}
                        className="grid grid-cols-5 px-4 py-2 bg-[#E6EEF8] text-sm rounded-xl mt-2"
                    >
                        <div>{item.patientId}</div>
                        <div>{item.recordId}</div>
                        <div>{item.timeIn}</div>
                        <div>{item.department}</div>
                        <div>{item.doctor}</div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default History_records;