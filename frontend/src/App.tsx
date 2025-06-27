import React from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './index.css'

import Navbar from './components/layout/Navbar'
import LoginPage from './components/Page/LoginPage'
import HomePage from './components/Page/HomePage'
import DoctorMain from './components/Doctor/DoctorMain'
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail'
import History_records from "./components/Staff/History_records"
import ReceptionProfile from "./components/Staff/ReceptionProfile"
import MedicalRecord from './components/Doctor/MedicalRecord';

const App = () => {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>

        {/* 
      < route path='/MedicalRecord/:patient_id' element {< MedicalRecord />} />
      < Route path='/History_records' element={<History_records />} />
      < Route path='/detail/:patient_id' element={< PatientAwaitDetail />} />
      < Route path='/' element={<DoctorMain />} />
      < Route path='/' element={<ReceptionProfile />} />
      < Route path='/' element={<ReceptionProfile />} />
      < Route path='/HomePage' element={<HomePage />} />
      
       */}
        < Route path='/doctor' element={<DoctorMain />} />
        < Route path='/MedicalRecord/:patient_id' element={< MedicalRecord />} />

      </Routes>
    </BrowserRouter>

  )
}
export default App


