
import React, { useState } from "react"
import { FaChevronDown } from "react-icons/fa";
import type { PatientForm } from "../../types/staff.types";
import { useNavigate } from "react-router-dom";



const PatientRecordForm: React.FC = () => {
    const [formData, setFormData] = useState<PatientForm>({
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


    const navigate = useNavigate();

    const handleCancel = () => {
        navigate(-1); 
    }

    const handleConfirm = () => {
        console.log("Form data:", formData)
    }

    return (
        <div className="max-h-max flex items-center justify-center p-4">
            <div className="bg-[#89AFE0] backdrop-blur-sm rounded-3xl relative  py-8 px-20 w-full max-w-5xl shadow-2xl">
                <div className="absolute -top-5 left-1/2 transform -translate-x-1/2 bg-[#618FCA] mx-auto w-full max-w-xl px-6 py-2 rounded-xl shadow-md">
                    <h2 className="text-white text-center font-semibold text-lg">Tạo hồ sơ bệnh nhân</h2>
                </div>

                <div className="space-y-6">
                    <div>
                        <label className="block text-[#133574] font-medium mb-2">Họ Tên</label>
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
                            <label className="block text-[#133574] font-medium mb-2">Giới tính</label>
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
                            <label className="block text-[#133574] font-medium mb-2">Ngày sinh</label>
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
                        <label className="block text-[#133574] font-medium mb-2">SĐT hoặc Email</label>
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
                        <label className="block text-[#133574] font-medium mb-2">Bệnh sử</label>
                        <textarea
                            name="medicalHistory"
                            value={formData.medicalHistory}
                            onChange={handleInputChange}
                            rows={6}
                            className="w-full px-4 py-3 rounded-lg border-0 bg-white/90 focus:bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 resize-none transition-all"
                            placeholder="Nhập thông tin bệnh sử của bệnh nhân..."
                        />
                    </div>
                    <div className="flex w-full">
                        <div className="w-1/2">
                        </div>
                        <div className="flex w-1/2 gap-4 pt-4">
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
                                className="flex-1 py-3 px-6 rounded-lg bg-[#618FCA] text-white font-medium hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-300 transition-all duration-200 shadow-lg"
                            >
                                Xác nhận
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    )
}
export default PatientRecordForm;
