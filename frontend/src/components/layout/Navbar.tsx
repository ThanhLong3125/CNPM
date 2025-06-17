import React from "react"
import { FaUser } from "react-icons/fa6"
import { useNavigate } from "react-router-dom"


const Navbar: React.FC = () => {
  const navigate = useNavigate();
  const handleClick = () => navigate('/user-profile')
  const Home = () => navigate('/mainstaff')

  return (
    <nav className='flex items-center justify-between bg-[#133574] p-4 mb-10 shadow-md'>
      <div className='ml-10 text-white justify-start text-3xl font-bold shadow-md'
      onClick = {Home}
      >
        AIDIMS
        </div>
      <div className="mr-10">
        <FaUser className=" w-8 h-8 text-white"
        onClick={handleClick}
         />
      </div>
    </nav >
  )
}
export default Navbar