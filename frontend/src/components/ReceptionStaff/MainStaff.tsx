import React, { useState, useEffect } from "react"
import type { MainStaffDeclare } from "../../types/staff.types"
import { useNavigate } from "react-router-dom";
import { IoIosSearch } from "react-icons/io";
import { fetchPatients } from "../../service/staffService"


const MainStaff: React.FC = () => {
    const [filter, setFilter] = useState<string>('');
    const navigate = useNavigate();
    const handleClick = (patientId: string) => {
        navigate(`/staff/patientRecord/${patientId}`);
    };
    const handleCreateNew = () => navigate('/staff/create-profile');
    const handleCreated = () => navigate('/staff/CreatedRecordList');


    const [patients, setPatients] = useState<MainStaffDeclare[]>([]);

    useEffect(() => {
        const load = async () => {
            const data = await fetchPatients();
            setPatients(data);
        };
        load();
    }, []);

    return (
        <div className="bg-[#D5DEEF] relative m-6 rounded-xl shadow-md">
            <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
                <h2 className="text-white text-center font-semibold text-lg">Danh sách bệnh nhân</h2>
            </div>

            <div className="flex flex-col md:flex-row justify-between items-center gap-4 my-6 mx-1 pt-14 pb-3 px-12">
                <div className="flex-1">
                    <div className="relative w-full">
                        < IoIosSearch className="absolute left-6 top-1/2 transform -translate-y-1/2 w-6 h-6 text-white" />
                        <input
                            type="text"
                            placeholder="Tìm kiếm mã bệnh nhân"
                            value={filter}
                            onChange={(e) => setFilter(e.target.value)}
                            className="bg-[#618FCA] w-full max-w-2xl pl-14 pr-3 rounded-4xl p-2 text-white placeholder-white "
                        />
                    </div>

                </div>

                <div className="flex gap-8">
                    <button
                        className="bg-[#618FCA] text-white w-fit rounded-xl py-3 px-10 text-center"
                        onClick={handleCreateNew}
                    >
                        Tạo hồ sơ mới
                    </button>
                    <button
                        className="bg-[#D5DEEF] border border-primary w-fit rounded-xl py-3 px-10 text-[#3A5985] text-center"
                        onClick={handleCreated}
                    >
                        Bệnh án đã tạo
                    </button>
                </div>



            </div>
            <div className="text-center relative rounded-t-xl px-2">
                <div className="grid grid-cols-4 sticky top-24 z-10 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl shadow-md">
                    <div>Mã bệnh nhân</div>
                    <div>Họ tên</div>
                    <div>Giới tính</div>
                    <div>Số điện thoại</div>
                </div>

                <div className="max-h-[400px] overflow-y-auto space-y-2 bg-[#D3E2F9] p-2">
                    {patients.map((patient, index) => (
                        <div
                            key={index}
                            onClick={() => handleClick(patient.patientID)}
                            className="grid grid-cols-4 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm"
                        >
                            <div>{patient.patientID}</div>
                            <div>{patient.fullName}</div>
                            <div>{patient.gender}</div>
                            <div>{patient.phone}</div>
                        </div>
                    ))}
                </div>


                <div className="sticky bottom-0 bg-[#A7C5EB] px-4 py-4 mt-2 rounded-b-xl shadow-md z-10">
                </div>
            </div>

        </div>
    )
}
export default MainStaff;