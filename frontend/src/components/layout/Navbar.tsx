import React from "react"
import { FaUser } from "react-icons/fa6"
import { useNavigate } from "react-router-dom"

const Navbar: React.FC = () => {
  const navigate = useNavigate();

  const handleUserClick = () => {
    navigate('/staff/user-profile');
  };

  const handleHomeClick = () => {
    const role = sessionStorage.getItem("role");
    if (role === "staff") {
      navigate('/staff/mainstaff');
    } else if (role === "doctor") {
      navigate('/doctor/doctormain');
    } else {
      navigate('/'); // fallback về trang đăng nhập nếu không rõ role
    }
  };

  return (
    <nav className='flex items-center justify-between bg-[#133574] p-4 mb-10 shadow-md'>
      <div
        className='ml-10 text-white justify-start text-3xl font-bold shadow-md cursor-pointer'
        onClick={handleHomeClick}
      >
        AIDIMS
      </div>
      <div className="mr-10">
        <FaUser className="w-8 h-8 text-white cursor-pointer" onClick={handleUserClick} />
      </div>
    </nav>
  );
};

export default Navbar;
