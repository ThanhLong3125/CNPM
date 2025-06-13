import React, { useState } from "react"
import type { MainStaffDeclare } from "../../types/staff.types"
import { useNavigate } from "react-router-dom";

const mockPatient: MainStaffDeclare[] = [
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },
    {
        patientID: "BN001",
        name: "Nguyễn Văn A",
        gender: "Nam",
        phone: "093873292",
    },

]

const MainStaff: React.FC = () => {
    const [filter, setFilter] = useState<string>('');
    const navigate = useNavigate();
    const handleClick = (patientId: string) => {
        navigate(`/CreatedRecord/${patientId}`);
    };
    const handleCreateNew = () => navigate('/create-profile');
    return (
        <div className="bg-[#D5DEEF] m-6 rounded-xl shadow-md">
            <div className="flex justify-center items-center ">
                <h2 className="bg-[#618FCA] w-fit mt-5 py-2 px-10 text-semibold text-lg rounded-xl ">Danh sách bệnh nhân</h2>
            </div>

            <div className="flex flex-col-4 md:flex-row gap-16 my-6 mx-1 p-2">
                <input
                    type="text"
                    placeholder="Tìm mã bệnh nhân "
                    value={filter}
                    onChange={(e) => setFilter(e.target.value)}
                    className="bg-[#618FCA] w-1/3  pl-9 rounded-4xl p-2 text-center"
                />
                <button className="bg-[#618FCA] w-fit rounded-xl py-3 px-10 text-center"
                 onClick={handleCreateNew}

                >
                    Tạo hồ sơ mới
                </button>
                <button className="bg-[#618FCA] w-fit rounded-xl py-3 px-10 text-center" >
                    Danh sách bệnh nhân
                </button>
                <button className="bg-[#D5DEEF] border border-primary w-fit rounded-xl py-3 px-10 text-center" >
                    Bệnh án đã tạo
                </button>

            </div>
            <div className=" text-center rounded-t-xl px-2">
                <div className="grid grid-cols-5 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
                    <div>Mã bệnh nhân</div>
                    <div>Họ tên</div>
                    <div>Giới tính</div>
                    <div>Số điện thoại</div>
                </div>
                <div className="space-y-2 bg-[#D3E2F9] p-2 ">
                    {mockPatient.map((MainStaffDeclare, index) => (
                        <div
                            key={index}
                            onClick={() => handleClick(MainStaffDeclare.patientID)}
                            className="grid grid-cols-5 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm"
                        >
                            <div>{MainStaffDeclare.patientID}</div>
                            <div>{MainStaffDeclare.name}</div>
                            <div>{MainStaffDeclare.gender}</div>
                            <div>{MainStaffDeclare.phone}</div>
                        </div>
                    ))}
                </div>
                <div className=" bg-[#A7C5EB] px-4 py-4 mb-2 rounded-b-xl shadow-md">
                </div>
            </div>

        </div>
    )
}
export default MainStaff;