import React, { useState } from "react"
import type { CreatedRecordDeclare } from "../../types/staff.types"
import { useNavigate } from "react-router-dom";

// Bệnh án đã tạo
const mockPatient: CreatedRecordDeclare[] = [
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
        timeIn: "03/06/2025 - 14:30"
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
        timeIn: "03/06/2025 - 14:30"
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
        timeIn: "03/06/2025 - 14:30"
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
        timeIn: "03/06/2025 - 14:30"
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
        timeIn: "03/06/2025 - 14:30"
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
        timeIn: "03/06/2025 - 14:30"
    },
]

const CreatedRecordList: React.FC = () => {
    const [filter, setFilter] = useState<string>('');
    const navigate = useNavigate();
    const handleClick = (patientId: string) => {
        navigate(`/staff/DetailCreatedRecord/${patientId}`);
    };
    return (
        <div className="bg-[#D5DEEF] m-6 rounded-xl relative shadow-md">
             <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
                <h2 className="text-white text-center font-semibold text-lg">Bệnh án đã tạo</h2>
            </div>
            <div className="flex flex-col-4 md:flex-row gap-16 pt-14 my-6 mx-1 p-2">
                <input
                    type="text"
                    placeholder="Tìm mã bệnh nhân "
                    value={filter}
                    onChange={(e) => setFilter(e.target.value)}
                    className="bg-[#618FCA] w-1/3  pl-9 rounded-4xl p-2 text-center"
                />

            </div>
            <div className=" text-center rounded-t-xl px-2">
                <div className="grid grid-cols-5 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
                    <div>Mã bệnh nhân</div>
                    <div>Họ tên</div>
                    <div>Giới tính</div>
                    <div>Số điện thoại</div>
                    <div>Thời gian</div>
                </div>
                <div className="space-y-2 bg-[#D3E2F9] p-2 ">
                    {mockPatient.map((CreatedRecordDeclare, index) => (
                        <div
                            key={index}
                            onClick={() => handleClick(CreatedRecordDeclare.patientID)}
                            className="grid grid-cols-5 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm"
                        >
                            <div>{CreatedRecordDeclare.patientID}</div>
                            <div>{CreatedRecordDeclare.name}</div>
                            <div>{CreatedRecordDeclare.gender}</div>
                            <div>{CreatedRecordDeclare.phone}</div>
                            <div>{CreatedRecordDeclare.timeIn}</div>

                        </div>
                    ))}
                </div>
                <div className=" bg-[#A7C5EB] px-4 py-4  mb-2 rounded-b-xl shadow-md">
                </div>
            </div>

        </div>
    )
}
export default CreatedRecordList