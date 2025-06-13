import React from 'react'
import './index.css'
import LoginPage from './components/Page/LoginPage'
import HomePage from './components/Page/HomePage'
import DoctorMain from './components/Doctor/DoctorMain'
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from './components/layout/Navbar'
import History_records from "./components/Staff/History_records"
import ReceptionProfile from "./components/Staff/ReceptionProfile"
const App = () => {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        {/* 
        <Route path='/detail/:patient_id' element={< PatientAwaitDetail/>}/> 
        < Route path='/' element={<History_records />} />
         < Route path='/' element={<ReceptionProfile />} />*/}
        < Route path='/' element={<DoctorMain />} />

      </Routes>
    </BrowserRouter>

  )
}
export default App


