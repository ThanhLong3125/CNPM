
import React, {useState} from "react"
import { FaChevronDown } from "react-icons/fa";

interface PatientFormData {
  fullName: string
  gender: string
  birthDate: string
  phoneOrEmail: string
  medicalHistory: string
}

const  PatientRecordForm: React.FC = () => {
  const [formData, setFormData] = useState<PatientFormData>({
    fullName: "",
    gender: "",
    birthDate: "",
    phoneOrEmail: "",
    medicalHistory: "",
  })

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }))
  }

  const handleCancel = () => {
    setFormData({
      fullName: "",
      gender: "",
      birthDate: "",
      phoneOrEmail: "",
      medicalHistory: "",
    })
  }

  const handleConfirm = () => {
    console.log("Form data:", formData)
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-200 via-blue-300 to-blue-400 flex items-center justify-center p-4">
      <div className="bg-blue-400/80 backdrop-blur-sm rounded-3xl p-8 w-full max-w-lg shadow-2xl">
=        <div className="bg-blue-500/60 rounded-xl px-6 py-3 mb-8 text-center">
          <h1 className="text-white font-semibold text-lg">Tạo hồ sơ bệnh nhân</h1>
        </div>

        <div className="space-y-6">
          <div>
            <label className="block text-white font-medium mb-2">Họ Tên</label>
            <input
              type="text"
              name="fullName"
              value={formData.fullName}
              onChange={handleInputChange}
              className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all"
              placeholder="Nhập họ và tên"
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-white font-medium mb-2">Giới tính</label>
              <div className="relative">
                <select
                  name="gender"
                  value={formData.gender}
                  onChange={handleInputChange}
                  className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 appearance-none cursor-pointer transition-all"
                >
                  <option value="">Chọn giới tính</option>
                  <option value="male">Nam</option>
                  <option value="female">Nữ</option>
                  <option value="other">Khác</option>
                </select>
                <FaChevronDown className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 w-5 h-5 pointer-events-none" />
              </div>
            </div>

            <div>
              <label className="block text-white font-medium mb-2">Ngày sinh</label>
              <div className="relative">
                <input
                  type="date"
                  name="birthDate"
                  value={formData.birthDate}
                  onChange={handleInputChange}
                  className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 cursor-pointer transition-all"
                />
              </div>
            </div>
          </div>

          <div>
            <label className="block text-white font-medium mb-2">SĐT hoặc Email</label>
            <input
              type="text"
              name="phoneOrEmail"
              value={formData.phoneOrEmail}
              onChange={handleInputChange}
              className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all"
              placeholder="Nhập số điện thoại hoặc email"
            />
          </div>

          <div>
            <label className="block text-white font-medium mb-2">Bệnh sử</label>
            <textarea
              name="medicalHistory"
              value={formData.medicalHistory}
              onChange={handleInputChange}
              rows={6}
              className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 resize-none transition-all"
              placeholder="Nhập thông tin bệnh sử của bệnh nhân..."
            />
          </div>

          <div className="flex gap-4 pt-4">
            <button
              type="button"
              onClick={handleCancel}
              className="flex-1 py-3 px-6 rounded-lg bg-white/20 text-white font-medium hover:bg-white/30 focus:outline-none focus:ring-2 focus:ring-white/50 transition-all duration-200 border border-white/30"
            >
              Hủy
            </button>
            <button
              type="button"
              onClick={handleConfirm}
              className="flex-1 py-3 px-6 rounded-lg bg-blue-600 text-white font-medium hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
            >
              Xác nhận
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}
export default PatientRecordForm;
