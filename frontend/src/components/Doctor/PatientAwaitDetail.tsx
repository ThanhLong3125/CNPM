import React from "react";
import Input from "../ui/Input";
import TextArea from "../ui/TextArea";
const PatientAwaitDetail: React.FC = () => {
  return (
    <div className="bg-[#D5DEEF] m-6 rounded-b shadow-md">
      <div className="grid grid-cols-2 gap-6">
        <div className=" bg-[#89AFE0] p-4 rounded">
          <div className="flex justify-center items-center ">
            <h2 className="bg-[#618FCA] w-fit p-2 rounded-xl ">Danh sách bệnh nhân chờ</h2>
          </div>
          <div className="grid grid-cols-2 gap-4">
            <Input label="Mã bệnh nhân" />
            <Input label="Mã bệnh án" />
            <Input label="Họ và tên" />
            <Input label="Ngày sinh" />
            <Input label="Địa chỉ" className="col-span-2" />
            <Input label="SĐT" />
            <Input label="Giới tính" />
            <Input label="Thời gian vào" />
            <Input label="Triệu chứng" />
            <TextArea label="Ghi chú" />
          </div>

          {/* Lower Section */}
          <div className="mt-4 grid grid-cols-2 gap-4">
            <Input label="Chẩn đoán sơ bộ" className="col-span-2" />
            <Input label="Chẩn đoán cuối cùng" className="col-span-2" />
            <Input label="Hướng điều trị" className="col-span-2" />
          </div>
        </div>

        {/* Right Column: Thông tin khám */}
        <div className="bg-[#8ab1db] p-4 rounded">
          <h2 className="text-lg font-semibold text-center mb-4">Thông tin khám</h2>
          <div className="grid grid-cols-3 gap-4">
            <Input label="Cân nặng" />
            <Input label="Chiều cao" />
            <Input label="Nhiệt độ" />
            <Input label="Mạch" />
            <Input label="Huyết áp" />
            <Input label="Nhịp thở" />
          </div>
          <div className="mt-4 grid grid-cols-2 gap-4">
            <TextArea label="Lý do khám" />
            <TextArea label="Tình trạng hiện tại" />
            <TextArea label="Bệnh sử" />
            <Input label="Tên bác sĩ" />
            <Input label="Mã bác sĩ" />
            <Input label="Khoa khám" />
            <Input label="Phòng khám" />
          </div>
          <div className="mt-6 flex gap-3 justify-end">
            <button className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded">Xem lịch sử</button>
            <button className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded">Lưu</button>
            <button className="bg-yellow-500 hover:bg-yellow-600 text-white px-4 py-2 rounded">Yêu cầu thêm</button>
            <button className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded">Tạm lưu</button>
          </div>
        </div>

      </div>
    </div>
  );
};

export default PatientAwaitDetail;
