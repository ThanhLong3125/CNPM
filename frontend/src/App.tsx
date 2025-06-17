import React from 'react'
import './index.css'
import LoginPage from './components/Page/LoginPage'
import HomePage from './components/Page/HomePage'
import DoctorMain from './components/Doctor/DoctorMain'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from './components/layout/Navbar'
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail'
import MainStaff from "./components/ReceptionStaff/MainStaff"
import CreatedRecord from "./components/ReceptionStaff/CreatedRecordList"
import NewRecord from "../src/components/ReceptionStaff/NewRecord"
import PatientRecordView from './components/ReceptionStaff/PatientRecord'
import History_records from "./components/ReceptionStaff/History_records"
import CreateMedicalRecord from "./components/ReceptionStaff/CreateMedicalRecord"
import UpdatePatientRecord from './components/ReceptionStaff/UpdatePatientRecord'
import ReceptionProfile from './components/ReceptionStaff/ReceptionProfile'
import { Navigate } from 'react-router-dom';

const App = () => {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        {/* < Route path='/' element={<DoctorMain/>}/>
        <Route path='/detail/:patient_id' element={< PatientAwaitDetail/>}/> */}
          <Route path='/' element={<Navigate to="/mainstaff" />} />
        <Route path='/mainstaff' element={<MainStaff />} />
        <Route path='/CreatedRecordList' element={<CreatedRecord />} />
        <Route path='/create-profile' element={< NewRecord />} />
        <Route path='/patientRecord/:patientId' element={< PatientRecordView />} />
        <Route path='/medical-history/:patientId' element={<History_records />} />
        <Route path='/create-medical-record/:patientId' element={<CreateMedicalRecord />} />
        <Route path='/update-patient/:patientId' element={<UpdatePatientRecord />} />
        <Route path='/user-profile' element={< ReceptionProfile/>} />



      </Routes>
    </BrowserRouter>

  )
}
export default App
