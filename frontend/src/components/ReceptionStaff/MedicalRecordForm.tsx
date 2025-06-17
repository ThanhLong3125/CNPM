import { useNavigate } from "react-router-dom";

const DetailCreatedRecord = () => {
  const navigate = useNavigate();

  const handleCancel = () => {
    navigate(-1);
  }
  const handleEdit = () => navigate('/staff/EditMedicalRecord');
  return (
    <div className="min-h-screen">

      <div className="p-4 flex justify-center">
        <div className="w-full max-w-screen relative bg-[#89AFE0] rounded-lg p-6">
          <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
            <h2 className="text-white text-center font-semibold text-lg">Hồ sơ bệnh án</h2>
          </div>

          <div className="grid grid-cols-2 mt-6  gap-x-12 gap-y-4">

            <div>

              <div className="flex flex-col sm:flex-row gap-5">
                <div>
                  <label className="block mb-1 text-sm">Mã bệnh nhân</label>
                  <input type="text" defaultValue="BN0001" className="w-full px-6 py-1.5 bg-[#f0f0f0] rounded" />
                </div>

                <div>
                  <label className="block mb-1 text-sm">Mã bệnh án</label>
                  <input type="text" defaultValue="BN0001" className="w-full px-6 py-1.5 bg-[#f0f0f0] rounded" />
                </div>
              </div>
              <div className="mb-4">
                <label className="block mb-1 text-sm">Họ Tên</label>
                <input type="text" defaultValue="Nguyễn Văn A" className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
              </div>

              <div className="flex flex-col sm:flex-row gap-5">
                <div>
                  <label className="block font-medium text-sm text-gray-700 mb-1">Giới tính</label>
                  <select
                    defaultValue="Nam"
                    className="w-full px-10 py-2 bg-gray-200 rounded-lg shadow-sm focus:ring-2 focus:ring-blue-300"
                  >
                    <option value="Nam">Nam</option>
                    <option value="Nữ">Nữ</option>
                  </select>
                </div>

                <div>
                  <label className="block font-medium text-sm text-gray-700 mb-1">Ngày sinh</label>
                  <input
                    type="date"
                    defaultValue="1980-05-09"
                    className="w-full px-10 py-2 bg-gray-200 rounded-lg shadow-sm focus:ring-2 focus:ring-blue-300"
                  />
                </div>
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">SDT hoặc Email</label>
                <input type="text" defaultValue="0931490350" className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
              </div>

              <div>
                <label className="block mb-1 text-sm">Bệnh sử</label>
                <textarea
                  rows={5}
                  defaultValue="Tiểu đường"
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded resize-none"
                />
              </div>
            </div>


            <div>
              <div className="mb-4">
                <label className="block mb-1 text-sm">Thời gian tạo</label>
                <input type="text" defaultValue="16/04/2025 - 15:30" className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">Mã bác sĩ</label>
                <input type="text" defaultValue="BS0001" className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded" />
              </div>

              <div className="mb-4">
                <label className="block mb-1 text-sm">Triệu chứng</label>
                <textarea
                  rows={7}
                  defaultValue="Ho, đau họng"
                  className="w-full px-3 py-1.5 bg-[#f0f0f0] rounded resize-none"
                />
              </div>

              <div className="flex justify-end gap-4 mt-6">
                <button className="border border-gray-500 hover:bg-blue-400 text-black-900 px-10 py-2 rounded"
                onClick={handleCancel}
                >Hủy</button>
                <button className="bg-[#4977b8] hover:bg-blue-700 text-white px-6 py-2 rounded"
                onClick={handleCancel}
                >Xóa bệnh án</button>
                <button className="bg-[#4977b8] hover:bg-blue-700 text-white px-6 py-2 rounded"
                onClick={handleEdit}
                >Sửa bệnh án</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default DetailCreatedRecord
