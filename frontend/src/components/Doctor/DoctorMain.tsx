import React, { useState } from "react"
import { FaUser } from "react-icons/fa6";
import type { patientAwait, patientExamined, } from "../../types/doctor.type";
import { useNavigate } from "react-router-dom";
import { IoSearchOutline } from "react-icons/io5";


const mockPatient: patientAwait[] = [
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/06/2025',
    },
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/06/2025',
    },
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/06/2025',
    },
]
const dataPatient: patientExamined[] = [
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/06/2025',
        attendDoctor: 'Le Van Khuong'
    },
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/06/2025',
        attendDoctor: 'Le Van Khuong'
    }, 
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/06/2025',
        attendDoctor: 'Le Van Khuong'
    },
]
const DoctorMain: React.FC = () => {
    const [filter, setFilter] = useState<string>('');
    const [dateFilter, setDateFilter] = useState<string>('');
    const [timeFilter, setTimeFilter] = useState<string>('');
     const navigate = useNavigate();

    const handleClick = (patient_id: string) => {
        navigate(`/detail/${patient_id}`);
    };

    const filteredPatients: patientAwait[] = mockPatient.filter((p) =>
        p.patient_id.toLowerCase().includes(filter.toLowerCase()) &&
        (dateFilter === '' || p.timeIn.includes(dateFilter))
    );
    return (
        <div>
            <nav className='flex items-center justify-between bg-[#133574] p-4 mb-10 shadow-md'>
                <div className='ml-10 text-white justify-start text-3xl font-bold shadow-md'>AIDIMS</div>
                <div className="mr-10">
                    <FaUser className=" w-8 h-8 text-white" />
                </div>
            </nav >
            <div className="bg-[#D3E2F9]p-4 rounded-xl m-10 text-center shadow-md ">

                <div className="flex justify-center items-center ">
                    <h2 className="bg-[#618FCA] w-fit p-2 rounded-xl ">Danh sách bệnh nhân chờ</h2>
                </div>
                <div className=" text-center rounded-t-xl  ">
                    <div className="grid grid-cols-5 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
                        <div>Mã bệnh nhân</div>
                        <div>Mã bệnh án</div>
                        <div>Họ tên</div>
                        <div>Giới tính</div>
                        <div>Thời gian vào</div>
                    </div>

                    <div className="space-y-2 bg-[#D3E2F9] p-2 ">
                        {mockPatient.map((patientAwait, index) => (
                            <div
                                key={index}
                                onClick={()=> handleClick(patientAwait.patient_id)}
                                className="grid grid-cols-5 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm"
                            >
                                <div>{patientAwait.patient_id}</div>
                                <div>{patientAwait.medicalRecord_id}</div>
                                <div>{patientAwait.name}</div>
                                <div>{patientAwait.gender}</div>
                                <div>{patientAwait.timeIn}</div>
                            </div>
                        ))}
                    </div>
                    <div className=" bg-[#A7C5EB] px-4 py-4 rounded-b-xl shadow-md">
                    </div>
                </div>
            </div>
            <div className="bg-[#D3E2F9] p-4 rounded-xl m-10 text-center shadow-md ">
                <div className="flex justify-center items-center ">
                    <h2 className="bg-[#618FCA] w-fit p-2 rounded-xl ">Danh sách bệnh nhân đã khám</h2>
                </div>

                <div className="flex flex-col md:flex-row gap-16 my-6 mx-1 ">
                    <input
                        type="text"
                        placeholder="Tìm mã bệnh nhân "
                        value={filter}
                        onChange={(e) => setFilter(e.target.value)}
                        className="bg-[#618FCA] w-1/3  rounded-xl p-2 text-center"
                    />
                    <div className="bg-[#618FCA] w-fit rounded-xl py-2 px-4 text-center" >
                        Bác sĩ phụ trách
                    </div>
                    <input
                        type="date"
                        value={dateFilter}
                        onChange={(e) => setDateFilter(e.target.value)}
                        className="bg-[#618FCA] w-auto rounded-xl py-2 px-8  text-center"
                    />
                    <input
                        type="time"
                        value={timeFilter}
                        onChange={(e) => setDateFilter(e.target.value)}
                        className="bg-[#618FCA] w-fit rounded-xl py-2 px-8  text-center"
                    />
                    <div className="bg-[#618FCA] w-fit rounded-xl py-2 px-8 text-center" >
                        Xem chi tiết
                    </div>

                </div>
                <div className="grid grid-cols-6 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
                    <div>Mã bệnh nhân</div>
                    <div>Mã bệnh án</div>
                    <div>Họ tên</div>
                    <div>Giới tính</div>
                    <div>Thời gian vào</div>
                    <div>Bác sĩ phụ trách</div>
                </div>
                <div className="space-y-2 bg-[#D3E2F9] p-2 ">
                    {dataPatient.map((patientExamined, index) => (
                        <div
                            key={index}
                            className="grid grid-cols-6 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm"
                        >
                            <div>{patientExamined.patient_id}</div>
                            <div>{patientExamined.medicalRecord_id}</div>
                            <div>{patientExamined.name}</div>
                            <div>{patientExamined.gender}</div>
                            <div>{patientExamined.timeIn}</div>
                            <div>{patientExamined.attendDoctor}</div>
                        </div>
                    ))}
                </div>
                <div className=" bg-[#A7C5EB] px-4 py-4 rounded-b-xl shadow-md">
                </div>


            </div>
        </div>

    )

}
export default DoctorMain;