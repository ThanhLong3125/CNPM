import React from 'react'
import './index.css'
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import DetailCreatedRecord from './components/ReceptionStaff/MedicalRecordForm';
import LoginPage from './components/Page/LoginPage'
import DoctorMain from './components/Doctor/DoctorMain'
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail'
import StaffLayout from './components/layout/StaffLayout'
import MainStaff from "./components/ReceptionStaff/MainStaff"
import CreatedRecord from "./components/ReceptionStaff/CreatedRecordList"
import NewRecord from "./components/ReceptionStaff/NewRecord"
import PatientRecordView from './components/ReceptionStaff/PatientRecord'
import HistoryRecords from "./components/ReceptionStaff/History_records"
import CreateMedicalRecord from "./components/ReceptionStaff/CreateMedicalRecord"
import UpdatePatientRecord from './components/ReceptionStaff/UpdatePatientRecord'
import ReceptionProfile from './components/ReceptionStaff/ReceptionProfile'
import DoctorLayout from './components/layout/DoctorLayout';
import EditMedicalRecord from "../src/components/ReceptionStaff/EditMedicalRecord"

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />

        <Route path="/staff" element={<StaffLayout />}>
          <Route index element={<MainStaff />} />
          <Route path="mainstaff" element={<MainStaff />} />
          <Route path="CreatedRecordList" element={<CreatedRecord />} />
          <Route path="create-profile" element={<NewRecord />} />
          <Route path="patientRecord/:patientId" element={<PatientRecordView />} />
          <Route path="medical-history/:patientId" element={<HistoryRecords />} />
          <Route path="create-medical-record/:patientId" element={<CreateMedicalRecord />} />
          <Route path="update-patient/:patientId" element={<UpdatePatientRecord />} />
          <Route path="user-profile" element={<ReceptionProfile />} />
          <Route path="DetailCreatedRecord/:patientId" element={<DetailCreatedRecord/>} />
          <Route path="EditMedicalRecord" element={<EditMedicalRecord/>} />
        </Route>

        <Route path="/doctor" element={<DoctorLayout />}>
          <Route index element={<DoctorMain />} />
          <Route path="doctormain" element={<DoctorMain/>} />

          <Route path="detail/:patient_id" element={<PatientAwaitDetail />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
