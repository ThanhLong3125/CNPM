import React, { useState } from "react"
import type { patientAwait, patientExamined, } from "../../types/doctor.type";
import { useNavigate } from "react-router-dom";
import { IoIosSearch } from "react-icons/io";

const mockPatient: patientAwait[] = [
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/07/2025',
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
        timeIn: '10:30 01/07/2025',
    },
    {
        patient_id: 'B0001',
        medicalRecord_id: 'R0001',
        name: 'Nguyen Van A',
        gender: 'Male',
        timeIn: '10:30 01/07/2025',
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
            <div className="bg-[#D3E2F9] p-4 rounded-xl mx-10 text-center relative shadow-md ">
                <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md ">
                    <h2 className="text-white text-center font-semibold text-lg">Danh sách bệnh nhân chờ </h2>
                </div>



                <div className=" text-center relative  rounded-t-xl mt-10  ">

                    <div className="grid grid-cols-5 sticky top-24 z-10 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
                        <div>Mã bệnh nhân</div>
                        <div>Mã bệnh án</div>
                        <div>Họ tên</div>
                        <div>Giới tính</div>
                        <div>Thời gian vào</div>
                    </div>

                    <div className="max-h-[200px] overflow-y-auto space-y-2 bg-[#D3E2F9] p-2 ">
                        {mockPatient.map((patientAwait, index) => (
                            <div
                                key={index}
                                onClick={() => handleClick(patientAwait.patient_id)}
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
                    <div className=" bg-[#A7C5EB] sticky bottom-0 px-4 py-4 rounded-b-xl shadow-md">
                    </div>
                </div>
            </div>
            <div className="bg-[#D3E2F9] p-4 rounded-xl m-10 text-center relative shadow-md ">
                <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md ">
                    <h2 className="text-white text-center font-semibold text-lg">Danh sách bệnh nhân đã khám </h2>
                </div>
                <div className="grid grid-cols-1 md:grid-cols-13 gap-4 md:gap-6 my-6 mx-4 items-center">
                    <div className="relative w-full md:col-span-5">
                        < IoIosSearch className="absolute left-6 top-1/2 transform -translate-y-1/2 w-6 h-6 text-white" />
                        <input
                            type="text"
                            placeholder="Tìm kiếm mã bệnh nhân"
                            value={filter}
                            onChange={(e) => setFilter(e.target.value)}
                            className="bg-[#618FCA] w-full max-w-2xl pl-14 pr-3 rounded-4xl p-2 text-white placeholder-white "
                        />
                    </div>


                    <div className="bg-[#618FCA] md:col-span-2 w-full text-white rounded-xl py-2 px-4 text-center">
                        Bác sĩ phụ trách
                    </div>
                    <div className="md:col-span-2">
                        <input
                            type="date"
                            value={dateFilter}
                            onChange={(e) => setDateFilter(e.target.value)}
                            className="bg-[#618FCA] w-full rounded-xl py-2 px-4 text-white text-center"
                        />
                    </div>
                    <div className="md:col-span-2">
                        <input
                            type="time"
                            value={timeFilter}
                            onChange={(e) => setTimeFilter(e.target.value)}
                            className="bg-[#618FCA] w-full rounded-xl py-2 px-4 text-white text-center"
                        />
                    </div>
                    <div className="md:col-span-2">
                        <button
                            onClick={() => {/* handle xem chi tiết */ }}
                            className="bg-[#618FCA] w-full rounded-xl py-2 px-4 text-white text-center hover:bg-[#4b7bb3] transition-colors"
                        >
                            Xem chi tiết
                        </button>
                    </div>
                </div>
                <div>
                    <div className="grid grid-cols-6 sticky top-24 z-10 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
                        <div>Mã bệnh nhân</div>
                        <div>Mã bệnh án</div>
                        <div>Họ tên</div>
                        <div>Giới tính</div>
                        <div>Thời gian vào</div>
                        <div>Bác sĩ phụ trách</div>
                    </div>
                    <div className="max-h-[200px] overflow-y-auto space-y-2 bg-[#D3E2F9] p-2 ">
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
                    <div className=" bg-[#A7C5EB] sticky bottom-0 px-4 py-4 rounded-b-xl shadow-md">
                    </div>
                </div>

            </div>
        </div>

    )

}
export default DoctorMain;