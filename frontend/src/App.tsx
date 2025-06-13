import React from 'react'
import './index.css'
import LoginPage from './components/Page/LoginPage'
import HomePage from './components/Page/HomePage'
import DoctorMain from './components/Doctor/DoctorMain'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from './components/layout/Navbar'
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail'
import MainStaff from "./components/ReceptionStaff/MainStaff"
import CreatedRecord from "../src/components/ReceptionStaff/CreatedRecord"
import NewRecord from "../src/components/ReceptionStaff/NewRecord"
const App = () => {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        {/* < Route path='/' element={<DoctorMain/>}/>
        <Route path='/detail/:patient_id' element={< PatientAwaitDetail/>}/> */}
        <Route path='/' element={<MainStaff />} />
        <Route path='/CreatedRecord/:patient_id' element={<  CreatedRecord />} />
        <Route path='/create-profile' element={<  NewRecord />} />




      </Routes>
    </BrowserRouter>

  )
}
export default App
