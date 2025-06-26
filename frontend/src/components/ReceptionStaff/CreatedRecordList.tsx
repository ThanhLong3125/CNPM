import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import type { CreatedRecordDeclare } from "../../types/staff.types";
import { fetchAllHistoryRecord, fetchMedicalRecordById } from "../../service/staffService";
import { IoIosSearch } from "react-icons/io";

const CreatedRecordList: React.FC = () => {
  const [searchInput, setSearchInput] = useState('');
  const [filter, setFilter] = useState('');

  const [history, setHistory] = useState<CreatedRecordDeclare[]>([]);
  const navigate = useNavigate();

  const handleSearch = async () => {
    if (!searchInput.trim()) return;

    const result = await fetchMedicalRecordById(searchInput.trim());
    if (result && result.medicalRecordId) {
      navigate(`/staff/DetailCreatedRecord/${result.medicalRecordId}`);
    } else {
      alert("Không tìm thấy bệnh án!");
    }
  };


  const handleClick = (medicalRecordId: string) => {
    navigate(`/staff/DetailCreatedRecord/${medicalRecordId}`);
  };

  useEffect(() => {
    const load = async () => {
      const data = await fetchAllHistoryRecord();
      console.log("Fetched medical record history:", data);
      setHistory(data);
    };
    load();
  }, []);

  const filtered = history.filter(record =>
    record.medicalRecordId?.toLowerCase().includes(filter.toLowerCase())
  );


  return (
    <div className="bg-[#D5DEEF] m-6 rounded-xl relative shadow-md">
      <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
        <h2 className="text-white text-center font-semibold text-lg">Bệnh án đã tạo</h2>
      </div>

      <div className="flex flex-col-4 md:flex-row gap-16 pt-14 my-6 mx-1 p-2">
        <div className="relative w-full">
          < IoIosSearch className="absolute left-6 top-1/2 transform -translate-y-1/2 w-6 h-6 text-white" />
          <input
            type="text"
            placeholder="Tìm kiếm mã bệnh án"
            value={searchInput}
            onChange={(e) => {
              setSearchInput(e.target.value);
              setFilter(e.target.value);
            }}
            onKeyDown={(e) => {
              if (e.key === "Enter") handleSearch();
            }}
            className="bg-[#618FCA] w-full max-w-2xl pl-14 pr-3 rounded-4xl p-2 text-white placeholder-white"
          />


        </div>
      </div>

      <div className="text-center rounded-t-xl px-2">
        <div className="grid grid-cols-6 font-semibold bg-[#A7C5EB] text-[#1F3C88] px-4 py-2 rounded-t-xl mt-4 shadow-md">
          <div>Mã bệnh nhân</div>
          <div>Mã bệnh án</div>
          <div>Họ tên</div>
          <div>Giới tính</div>
          <div>Số điện thoại</div>
          <div>Thời gian</div>
        </div>

        <div className="space-y-2 bg-[#D3E2F9] p-2">
          {filtered.map((record, index) => (
            <div
              key={index}
              onClick={() => handleClick(record.medicalRecordId)}
              className="grid grid-cols-6 bg-[#E3ECFA] text-sm px-4 py-2 rounded-xl shadow-sm cursor-pointer hover:bg-[#dbe7fc]"
            >
              <div>{record.patientID}</div>
              <div>{record.medicalRecordId}</div>
              <div>{record.fullName}</div>
              <div>{record.gender === "male" ? "Nam" : "Nữ"}</div>
              <div>{record.phone}</div>
              <div>{new Date(record.createdDate).toLocaleString("vi-VN")}</div>
            </div>
          ))}
        </div>

        <div className="bg-[#A7C5EB] px-4 py-4 mb-2 rounded-b-xl shadow-md" />
      </div>
    </div>
  );
};

export default CreatedRecordList;
