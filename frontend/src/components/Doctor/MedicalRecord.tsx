import React from "react"
import { FaUser } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";

const MedicalRecord: React.FC = () => {
    
  const navigate = useNavigate();
  const handleClick= () => navigate('/OldMedicalRecord');
  
  
  
  
  return (
    <div>
      <nav className='flex items-center justify-between bg-[#133574] p-4 shadow-md'>
                        <div className='ml-10 text-white justify-start text-3xl font-bold shadow-md'>AIDIMS</div>
                        <div className="mr-10">
                            <FaUser className=" w-8 h-8 text-white" />
                        </div>
      </nav >
      
      <div className="bg-[#e0e7f9] p-9">
        <div className="grid grid-cols-6 grid-rows-6 gap-6 ">
          
          {/* 1 */}
          <div className="col-span-3 row-span-3">

            <div className="space-y-4 bg-[#a8c0f0] p-5 pt-0 rounded-md size-full shadow-md">
              
              <div className="flex justify-center">
                <h2 className="text-lg font-bold text-white bg-[#264796] px-10 py-1 rounded-md inline-block shadow-md">Thông tin bệnh nhân</h2>
              </div>

              <div className="grid grid-cols-8 grid-rows-5 gap-3">
                  <div className="col-span-4"><div className="text-[#133574] font-semibold">Mã bệnh nhân</div></div>
                  <div className="col-span-4 row-start-2"><input className="w-full bg-white rounded-md pl-2" disabled defaultValue="BN0001"/></div>
                  
                  <div className="col-span-4 col-start-5"><div className="text-[#133574] font-semibold">Mã bệnh án</div></div>
                  <div className="col-span-4 col-start-5 row-start-2"><input className="w-full bg-white rounded-md pl-2" disabled defaultValue="BA0001"/></div>

                  
                  <div className="col-span-4 row-start-3"><input className="w-full bg-white rounded-md pl-2" disabled defaultValue="Nguyễn Văn A"/></div>
                  <div className="col-span-2 col-start-5 row-start-3"><input className="w-full bg-white rounded-md pl-2" disabled type="date"/></div>
                  <div className="col-span-2 col-start-7 row-start-3"><select className="w-full bg-white rounded-md pl-2" disabled defaultValue="Nam"><option value="Nam">Nam</option><option value="Nữ">Nữ</option></select></div>
                  
                  <div className="col-span-4 row-start-4"><div className="text-[#133574] font-semibold">Triệu chứng</div></div>
                  <div className="col-span-4 row-start-5"><input className="w-full bg-white rounded-md pl-2" disabled defaultValue="Ho, sốt"/></div>
                  
                  <div className="col-span-4 col-start-5 row-start-4"><div className="text-[#133574] font-semibold" >Thời gian vào</div></div>
                  <div className="col-span-4 col-start-5 row-start-5"><input className="w-full bg-white rounded-md pl-2" disabled type="datetime-local"/></div>
              </div>

            </div>

          </div>
          
          {/* 2 */}
          <div className="col-span-3 row-span-6 col-start-4">
            
            <div className="space-y-4 bg-[#a8c0f0] p-5 pt-0 rounded-md size-full shadow-md">
              
              <div className="flex justify-center">
                <h2 className="text-lg font-bold text-white bg-[#264796] px-10 py-1 rounded-md inline-block shadow-md">Thông tin ảnh chụp</h2>
              </div>

              <div className="flex items-center justify-center relative">             
                
                <div className="grid grid-cols-3 grid-rows-3 gap-4">
                    <div className="col-span-2 row-span-3"><div className="w-65 h-65 bg-white rounded-md flex items-center justify-center relative"></div></div>
                    <div className="col-start-3"><div className="w-19 h-19 bg-white rounded-md flex items-center justify-center relative"></div></div>
                    <div className="col-start-3 row-start-2"><div className="w-19 h-19 bg-white rounded-md flex items-center justify-center relative"></div></div>
                    <div className="col-start-3 row-start-3"><div className="w-19 h-19 bg-white rounded-md flex items-center justify-center relative"></div></div>
                </div>

              </div>

              <div className="flex justify-start space-x-2 mt-2">
                <button className="text-white font-bold bg-[#264796] hover:bg-blue-900 focus:outline-2 focus:outline-offset-2 focus:outline-blue-500 active:bg-blue-700 px-7 py-2 rounded-xl shadow-md">Thêm</button>
                <button className="text-white font-bold bg-[#264796] hover:bg-blue-900 focus:outline-2 focus:outline-offset-2 focus:outline-blue-500 active:bg-blue-700 px-7 py-2 rounded-xl shadow-md">Xóa</button>
                <button className="text-white font-bold bg-[#264796] hover:bg-blue-900 focus:outline-2 focus:outline-offset-2 focus:outline-blue-500 active:bg-blue-700 px-7 py-2 rounded-xl shadow-md">AI</button>
              </div>

              <div className="grid grid-cols-2 grid-rows-1 gap-4">
                  <div ><div className="text-[#133574] font-semibold mb-1">Mã phiếu chụp</div><input className="w-full bg-white rounded-md pl-2" disabled defaultValue="A0001"/></div>
                  <div ><div className="text-[#133574] font-semibold mb-1">Thời gian chụp</div><input className="w-full bg-white rounded-md pl-2" disabled type="datetime-local"/></div>
              </div>

              <input className="w-full h-30 bg-white rounded-md pl-2" placeholder ="Chuẩn đoán của AI"/>
      
              <div className="flex justify-end space-x-2 mt-2">
                <button className="text-white font-bold bg-[#264796] hover:bg-blue-900 focus:outline-2 focus:outline-offset-2 focus:outline-blue-500 active:bg-blue-700 px-7 py-2 rounded-xl shadow-md" onClick={handleClick}>Xem lịch sử</button>

                <button className="text-white font-bold bg-[#264796] hover:bg-blue-900 focus:outline-2 focus:outline-offset-2 focus:outline-blue-500 active:bg-blue-700 px-7 py-2 rounded-xl shadow-md">Tạm lưu</button>
                <button className="text-white font-bold bg-[#264796] hover:bg-blue-900 focus:outline-2 focus:outline-offset-2 focus:outline-blue-500 active:bg-blue-700 px-7 py-2 rounded-xl shadow-md">Lưu</button>
              </div>

            </div>

          </div>

          {/* 3 */}
          <div className="col-span-3 row-span-3 row-start-4">
            
            <div className="space-y-4 bg-[#a8c0f0] p-5 pt-0 rounded-md size-full shadow-md">
              
              <div className="flex justify-center">
                <h2 className="text-lg font-bold text-white bg-[#264796] px-10 py-1 rounded-md inline-block shadow-md">Thông tin chẩn đoán</h2>
              </div>
    
              <div className="grid grid-cols-2 grid-rows-3 gap-2">
                  <div ><div className="text-[#133574] font-semibold mb-1">Tên bác sĩ</div></div>
                  <div ><div className="text-[#133574] font-semibold mb-1">Mã bác sĩ</div></div>
                  <div ><input className="w-full bg-white rounded-md pl-2" disabled defaultValue="Nguyễn Văn B"/></div>
                  <div ><input className="w-full bg-white rounded-md pl-2" disabled defaultValue="BS0001"/></div>
                  <div className="col-span-2"><div className="text-[#133574] font-semibold">Chẩn đoán</div></div>
              </div>

              <input className="w-full h-20 bg-white rounded-md pl-2" defaultValue=""/>
    
            </div>
          
          </div>

        </div>
      </div> 
    </div>
    );
}   
export default MedicalRecord;


