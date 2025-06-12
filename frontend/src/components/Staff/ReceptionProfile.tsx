import React from "react";

const ReceptionProfile: React.FC = () => {
    return (
        <div className="min-h-screen bg-[#d8e0ef] flex flex-col">
            <header className="bg-[#3a5a89] px-6 py-3 text-white flex items-center justify-between">
                <h1 className="text-2xl font-bold">AIDIMS</h1>
                <div className="text-2xl">
                    <i className="fas fa-user-circle"></i>
                </div>
            </header>

            <main className="flex justify-center items-center flex-grow">
                <div className="bg-[#83a8dd] rounded-2xl p-10 w-[600px] shadow-md">
                    <h2 className="text-white text-xl font-semibold text-center mb-8 bg-[#5e8fc5] py-2 rounded-xl">
                        Hồ sơ lễ tân
                    </h2>

                    <form className="space-y-5">
                        <div>
                            <label className="block text-white mb-1">Mã lễ tân</label>
                            <input
                                type="text"
                                value="BN0001"
                                className="w-full px-3 py-2 rounded bg-white text-black focus:outline-none"
                                disabled
                            />
                        </div>

                        <div>
                            <label className="block text-white mb-1">Họ Tên</label>
                            <input
                                type="text"
                                value="Nguyễn Văn A"
                                className="w-full px-3 py-2 rounded bg-white text-black focus:outline-none"
                            />
                        </div>

                        <div className="flex space-x-4">
                            <div className="w-1/2">
                                <label className="block text-white mb-1">Giới tính</label>
                                <select className="w-full px-3 py-2 rounded bg-white text-black">
                                    <option>Nam</option>
                                    <option>Nữ</option>
                                </select>
                            </div>
                            <div className="w-1/2">
                                <label className="block text-white mb-1">Ngày sinh</label>
                                <input
                                    type="date"
                                    value="1970-03-15"
                                    className="w-full px-3 py-2 rounded bg-white text-black"
                                />
                            </div>
                        </div>

                        <div>
                            <label className="block text-white mb-1">SDT</label>
                            <input
                                type="text"
                                value="0908439908"
                                className="w-full px-3 py-2 rounded bg-white text-black focus:outline-none"
                            />
                        </div>

                        <div className="flex justify-end pt-4">
                            <button
                                type="button"
                                className="bg-[#5e8fc5] text-white px-6 py-2 rounded-xl hover:bg-[#4b7bb3]"
                            >
                                Đăng xuất
                            </button>
                        </div>
                    </form>
                </div>
            </main>
        </div>
    )

}

export default ReceptionProfile;
