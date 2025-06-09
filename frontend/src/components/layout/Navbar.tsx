import React from "react"
import { FaUser } from "react-icons/fa6"


const Navbar: React.FC = () =>{
    return (
<nav className='flex items-center justify-between bg-[#133574] p-4 mb-10 shadow-md'>
      <div className='ml-10 text-white justify-start text-3xl font-bold shadow-md'>AIDIMS</div>
      <div className="mr-10">
        <FaUser className=" w-8 h-8 text-white" />
      </div>
    </nav >
    )
}
export default Navbar