import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IoIosSearch } from "react-icons/io";
import {
  fetchWaitingPatients,
  fetchTreatedPatients,
} from "../../service/doctorService";
import type { patient } from "../../service/doctorService";

const DoctorMain: React.FC = () => {
  const [filter, setFilter] = useState<string>("");
  const [dateFilter, setDateFilter] = useState<string>("");
  const [timeFilter, setTimeFilter] = useState<string>("");
  const navigate = useNavigate();
  const [waitingPatients, setWaitingPatients] = useState<patient[]>([]);
  const [treatedPatients, setTreatedPatients] = useState<patient[]>([]);

  const [clearAllLoading, setClearAllLoading] = useState(false);

  // Lấy danh sách bệnh nhân chờ
  useEffect(() => {
    const loadPatients = async () => {
      try {
        const data = await fetchWaitingPatients();
        setWaitingPatients(data);
      } catch (err) {
        console.error("Không thể tải danh sách bệnh nhân:", err);
      } finally {
        // Loading complete
      }
    };
    loadPatients();
  }, []);

  // Lấy danh sách bệnh nhận đã khám
  useEffect(() => {
    const loadData = async () => {
      try {
        const patients = await fetchTreatedPatients();
        setTreatedPatients(patients);
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu bệnh nhân:", error);
      }
    };

    loadData();
  }, []);

  const handleClick = (patient_id: string) => {
    navigate(`/doctor/MedicalRecord/${patient_id}`);
  };

  const handleClickOld = (patientId: string) => {
    navigate(`/doctor/OldMedicalRecord/${patientId}`);
  };


  const filteredTreatedPatients = treatedPatients.filter((p) => {
    const matchPatientId = p.patientId
      .toLowerCase()
      .includes(filter.toLowerCase());

    const matchDate =
      dateFilter === "" ||
      new Date(p.createdAt).toISOString().slice(0, 10) === dateFilter;

    const matchTime =
      timeFilter === "" ||
      new Date(p.createdAt).toTimeString().slice(0, 5) === timeFilter;

    return matchPatientId && matchDate && matchTime;
  });

  // Hàm định dạng ngày và giờ
  const formatDateTime = (isoDate: string): string => {
    const date = new Date(isoDate);
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0"); // Tháng bắt đầu từ 0
    const year = date.getFullYear();
    const hours = String(date.getHours()).padStart(2, "0");
    const minutes = String(date.getMinutes()).padStart(2, "0");
    return `${day}/${month}/${year} ${hours}:${minutes}`;
  };

  return (
    <div>
      <div className="bg-[#D3E2F9] p-4 rounded-xl mx-10 text-center relative shadow-md ">
        <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md ">
          <h2 className="text-white text-center font-semibold text-lg">
            Danh sách bệnh nhân chờ
          </h2>
        </div>

        <div className=" text-center relative  rounded-t-xl mt-10  ">
          <div className="grid grid-cols-5 top-24 z-10 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
            <div>Mã bệnh nhân</div>
            <div>Mã bệnh án</div>
            <div>Họ tên</div>
            <div>Giới tính</div>
            <div>Thời gian vào</div>
          </div>

          <div className="max-h-[200px] overflow-y-auto space-y-2 bg-[#D3E2F9] p-2 ">
            {waitingPatients.map((patientAwait, index) => (
              <div
                key={index}
                onClick={() => handleClick(patientAwait.patientId)}
                className="grid grid-cols-5 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm cursor-pointer hover:bg-[#c9dcf2]"
              >
                <div>{patientAwait.patientId}</div>
                <div>{patientAwait.medicalRecordId}</div>
                <div>{patientAwait.fullName}</div>
                <div>{patientAwait.gender}</div>
                <div>{formatDateTime(patientAwait.createdAt)}</div>
              </div>
            ))}
          </div>
          <div className=" bg-[#A7C5EB] sticky bottom-0 px-4 py-4 rounded-b-xl shadow-md"></div>
        </div>
      </div>
      <div className="bg-[#D3E2F9] p-4 rounded-xl m-10 text-center relative shadow-md ">
        <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md flex items-center justify-between">
          <h2 className="text-white text-center font-semibold text-lg flex-1">
            Danh sách bệnh nhân đã khám
          </h2>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-13 gap-4 md:gap-6 my-6 mx-4 items-center">
          <div className="relative w-full md:col-span-5">
            <IoIosSearch className="absolute left-6 top-1/2 transform -translate-y-1/2 w-6 h-6 text-white" />
            <input
              type="text"
              placeholder="Tìm kiếm mã bệnh nhân"
              value={filter}
              onChange={(e) => setFilter(e.target.value)}
              className="bg-[#618FCA] w-full max-w-2xl pl-14 pr-3 rounded-4xl p-2 text-white placeholder-white "
            />
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
          <div className="max-h-[200px] overflow-y-auto space-y-2 bg-[#D3E2F9] p-2">
            {filteredTreatedPatients.map((patient, index) => (
              <div
                key={index}
                onClick={() => handleClickOld(patient.patientId)}
                className="grid grid-cols-6 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm cursor-pointer hover:bg-[#c9dcf2]"
              >
                <div>{patient.patientId}</div>
                <div>{patient.medicalRecordId}</div>
                <div>{patient.fullName}</div>
                <div>{patient.gender}</div>
                <div>{formatDateTime(patient.createdAt)}</div>
                <div>{patient.physicicanId}</div>
              </div>
            ))}
          </div>
          <div className=" bg-[#A7C5EB] bottom-0 px-4 py-4 rounded-b-xl shadow-md"></div>
        </div>
      </div>
    </div>
  );
};
export default DoctorMain;