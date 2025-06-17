import React from "react";
import { Outlet } from "react-router-dom";
import Navbar from "./Navbar"

const StaffLayout = () => {
  return (
    <>
      <Navbar />
      <div className="p-6">
        <Outlet />
      </div>
    </>
  );
};

export default StaffLayout;
