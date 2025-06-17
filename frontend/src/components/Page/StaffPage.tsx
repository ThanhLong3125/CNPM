import React from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "../layout/Navbar"
import MainStaff from "../ReceptionStaff/MainStaff"
import CreatedRecordList from "../ReceptionStaff/CreatedRecordList"
import NewRecord from "../ReceptionStaff/NewRecord"
import PatientRecordView from '../ReceptionStaff/PatientRecord'
import History_records from "../ReceptionStaff/History_records"
import CreateMedicalRecord from "../ReceptionStaff/CreateMedicalRecord"
import UpdatePatientRecord from '../ReceptionStaff/UpdatePatientRecord'
import ReceptionProfile from '../ReceptionStaff/ReceptionProfile'
import { Navigate } from 'react-router-dom';

const StaffPage= () => {
    return (
        <BrowserRouter>
            <Navbar />
            <Routes>
                <Route path='/' element={<Navigate to="/mainstaff" />} />
                <Route path='/mainstaff' element={<MainStaff />} />
                <Route path='/CreatedRecordList' element={<CreatedRecordList />} />
                <Route path='/create-profile' element={< NewRecord />} />
                <Route path='/patientRecord/:patientId' element={< PatientRecordView />} />
                <Route path='/medical-history/:patientId' element={<History_records />} />
                <Route path='/create-medical-record/:patientId' element={<CreateMedicalRecord />} />
                <Route path='/update-patient/:patientId' element={<UpdatePatientRecord />} />
                <Route path='/user-profile' element={< ReceptionProfile />} />



            </Routes>
        </BrowserRouter>

    )
}
export default StaffPage
